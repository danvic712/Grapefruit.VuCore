//-----------------------------------------------------------------------
// <copyright file= "SecretController.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/12/19 14:58:39 
// Modified by:
// Description: Jwt 授权
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Grapefruit.Application.Authorization.Secret.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Grapefruit.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SecretController : ControllerBase
    {
        #region Initialize

        //Todo：依赖注入服务接口
        //
        public IConfiguration Configuration { get; }

        public SecretController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region APIs

        /// <summary>
        /// 获取 Jwt Token 数据
        /// </summary>
        /// <param name="dto">获取 Token 值数据传输对象</param>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] SecretDto dto)
        {
            //Todo：判断当前获取用户是否存在,不存在直接返回 401
            var user = new
            {
                Id = Guid.NewGuid(),
                Name = "yuiter",
                Role = Guid.NewGuid(),
                Email = "yuiter@yuiter.com",
                Phone = "13912345678",
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]));

            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddMinutes(20);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.MobilePhone,user.Phone)
                }),//创建声明信息
                Issuer = "yuiter.com",//Jwt token 的签发者
                Audience = "yuiter.com",//Jwt token 的接收者
                Expires = expiresAt,//过期时间
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//创建 token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                access_token = tokenHandler.WriteToken(token),
                token_type = "Bearer",
                profile = new
                {
                    name = user.Name,
                    role = user.Role,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });
        }

        #endregion
    }
}