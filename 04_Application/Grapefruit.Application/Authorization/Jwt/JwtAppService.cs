//-----------------------------------------------------------------------
// <copyright file= "JwtAppService.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/21 21:59:09 
// Modified by:
// Description: Jwt Token 操作功能接口实现
//-----------------------------------------------------------------------
using Grapefruit.Application.Authorization.Jwt.Dto;
using Grapefruit.Application.Authorization.Secret.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Grapefruit.Application.Authorization.Jwt
{
    public class JwtAppService : IJwtAppService
    {
        #region Initialize

        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IDistributedCache _cache;

        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 获取 HTTP 请求上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// hash
        /// </summary>
        private readonly IPasswordHasher<UserDto> _passwordHasher;

        /// <summary>
        /// 刷新后的 Token 信息集合
        /// </summary>
        private readonly ISet<RefreshTokenDto> _refreshTokens = new HashSet<RefreshTokenDto>();

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="configuration"></param>
        /// <param name="passwordHasher"></param>
        public JwtAppService(IDistributedCache cache, IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration, IPasswordHasher<UserDto> passwordHasher)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        #endregion

        #region API Implements

        /// <summary>
        /// 新增 Token
        /// </summary>
        /// <param name="dto">用户信息数据传输对象</param>
        /// <returns></returns>
        public JsonWebTokenDto Create(UserDto dto)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));

            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));

            //将用户信息添加到 Claim 中
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            IEnumerable<Claim> claims = new Claim[] {
                new Claim(ClaimTypes.Name,dto.UserName),
                new Claim(ClaimTypes.Role,dto.Role.ToString()),
                new Claim(ClaimTypes.Email,dto.Email),
                new Claim(ClaimTypes.Expiration,expiresAt.ToString())
            };
            identity.AddClaims(claims);

            _httpContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//创建声明信息
                Issuer = _configuration["Jwt:Issuer"],//Jwt token 的签发者
                Audience = _configuration["Jwt:Audience"],//Jwt token 的接收者
                Expires = expiresAt,//过期时间
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//创建 token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refresh = _passwordHasher.HashPassword(dto, Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);

            //存储刷新后的 Token 信息
            _refreshTokens.Add(new RefreshTokenDto
            {
                Name = dto.UserName,
                Token = refresh
            });

            return new JsonWebTokenDto
            {
                Token = tokenHandler.WriteToken(token),
                Refresh = refresh,
                Auths = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                Expires = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 停用 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task DeactivateAsync(string token)
        => await _cache.SetStringAsync(GetKey(token),
                " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromSeconds(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]))
                });

        /// <summary>
        /// 停用当前 Token
        /// </summary>
        /// <returns></returns>
        public async Task DeactivateCurrentAsync()
        => await DeactivateAsync(GetCurrentAsync());

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task<bool> IsActiveAsync(string token)
        => await _cache.GetStringAsync(GetKey(token)) == null;

        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCurrentActiveTokenAsync()
        => await IsActiveAsync(GetCurrentAsync());

        /// <summary>
        /// 刷新 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="dto"></param>
        /// <param name="jwt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool Refresh(string token, UserDto dto, out JsonWebTokenDto jwt, out string msg)
        {
            var refreshToken = GetRefreshToken(token);
            if (refreshToken == null)
            {
                msg = "未获取到刷新后的 Token 信息";
                jwt = null;
                return false;
            }

            if (refreshToken.Revoked)
            {
                msg = "刷新后的 Token 信息已被停用";
                jwt = null;
                return false;
            }

            msg = "";
            jwt = Create(dto);
            return true;
        }

        /// <summary>
        /// 撤销刷新 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="msg">错误信息</param>
        public bool Revoke(string token, out string msg)
        {
            var refreshToken = GetRefreshToken(token);
            if (refreshToken == null)
            {
                msg = "未获取到刷新后的 Token 信息";
                return false;
            }

            if (refreshToken.Revoked)
            {
                msg = "刷新后的 Token 信息已被停用";
                return false;
            }

            refreshToken.Revoked = true;
            msg = "";
            return true;
        }

        #endregion

        #region Method

        /// <summary>
        /// 缓存中过期 Token 值的 key
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        private static string GetKey(string token)
            => $"deactivated token:{token}";

        /// <summary>
        /// 获取 HTTP 请求的 Token 值
        /// </summary>
        /// <returns></returns>
        private string GetCurrentAsync()
        {
            //http header
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            //authorization
            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();// bearer tokenvalue
        }

        /// <summary>
        /// 获取刷新后的 Token 信息
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        private RefreshTokenDto GetRefreshToken(string token)
            => _refreshTokens.SingleOrDefault(x => x.Token == token);

        #endregion
    }
}
