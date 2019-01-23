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
using Grapefruit.Application.Authorization.Secret;
using Grapefruit.Application.Authorization.Secret.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Grapefruit.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Permission")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SecretController : ControllerBase
    {
        #region Initialize

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
        /// 停用 Token
        /// </summary>
        /// <returns></returns>
        [HttpPost("token/cancel")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _jwtApp.DeactivateCurrentAsync();
            return Ok();
        }

        /// <summary>
        /// 刷新 Jwt Token 数据
        /// </summary>
        /// <param name="refreshToken">刷新 Token 值</param>
        /// <returns></returns>
        [HttpPost("token/refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync(string refreshToken)
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

            var jwt = await _jwtApp.RefreshAsync(refreshToken, user);

            bool flag = string.IsNullOrEmpty(jwt.Refresh);

            return Ok(new
            {
                access_token = jwt.Token,
                refresh_token = flag ? jwt.Refresh : jwt.Token,
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
                refresh_token = jwt.Refresh,
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