//-----------------------------------------------------------------------
// <copyright file= "SecretController.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/12/19 14:58:39 
// Modified by:
// Description: Jwt 授权
//-----------------------------------------------------------------------
using Grapefruit.Application.Authorization.Jwt;
using Grapefruit.Application.Authorization.Jwt.Dto;
using Grapefruit.Application.Authorization.Secret;
using Grapefruit.Application.Authorization.Secret.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        private readonly IJwtAppService _jwtApp;

        private readonly ILogger _logger;

        private readonly ISecretAppService _secretApp;

        public IConfiguration _configuration { get; }

        public SecretController(ILogger<SecretController> logger, IConfiguration configuration,
            IJwtAppService jwtApp, ISecretAppService secretApp)
        {
            _configuration = configuration;
            _jwtApp = jwtApp;
            _secretApp = secretApp;
            _logger = logger;
        }

        #endregion

        #region APIs

        /// <summary>
        /// 禁止访问
        /// </summary>
        /// <returns></returns>
        [HttpGet("denied")]
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return new JsonResult(new
            {
                msg = "denied access"
            });
        }

        /// <summary>
        /// 停用 Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("token/cancel")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _jwtApp.DeactivateCurrentAsync();

            return NoContent();
        }

        /// <summary>
        /// 刷新 Jwt Token 数据
        /// </summary>
        /// <param name="token">Token 值</param>
        /// <returns></returns>
        [HttpPost("token/refresh")]
        public IActionResult RefreshAccessToken(string token)
        {
            return null;
        }

        /// <summary>
        /// 取消刷新 Jwt Token 数据
        /// </summary>
        /// <param name="token">Token 值</param>
        /// <returns></returns>
        [HttpPost("token/revoke")]
        public IActionResult RevokeRefreshToken(string token)
        {
            //Todo：判断获取当前登录用户信息
            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "yuiter",
                Role = Guid.Empty,
                Email = "yuiter@yuiter.com",
                Phone = "13912345678",
            };

            if (user == null)
                return BadRequest();

            var flag = _jwtApp.Refresh(token, user, out JsonWebTokenDto jwt, out string msg);

            return Ok(new
            {
                access_token = flag ? jwt.Token : msg,
                token_type = "Bearer",
                profile = new
                {
                    name = user.UserName,
                    auth_time = flag ? jwt.Auths : 0,
                    expires_at = flag ? jwt.Expires : 0
                }
            });
        }

        /// <summary>
        /// 获取 Jwt Token 数据
        /// </summary>
        /// <param name="dto">获取 Token 值数据传输对象</param>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] SecretDto dto)
        {
            //Todo：判断获取当前登录用户信息
            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "yuiter",
                Role = Guid.Empty,
                Email = "yuiter@yuiter.com",
                Phone = "13912345678",
            };

            if (user == null)
                return BadRequest();

            var jwt = _jwtApp.Create(user);

            return Ok(new
            {
                access_token = jwt.Token,
                token_type = "Bearer",
                profile = new
                {
                    name = user.UserName,
                    auth_time = jwt.Auths,
                    expires_at = jwt.Expires
                }
            });
        }

        #endregion
    }
}