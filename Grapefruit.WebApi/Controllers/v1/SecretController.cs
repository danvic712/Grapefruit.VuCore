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

        /// <summary>
        /// Jwt 服务
        /// </summary>
        private readonly IJwtAppService _jwtApp;

        /// <summary>
        /// 日志记录服务
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly ISecretAppService _secretApp;

        /// <summary>
        /// 配置信息
        /// </summary>
        public IConfiguration _configuration { get; }

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="jwtApp"></param>
        /// <param name="secretApp"></param>
        public SecretController(ILogger<SecretController> logger, IConfiguration configuration,
            IJwtAppService jwtApp, ISecretAppService secretApp)
        {
            _configuration = configuration;
            _jwtApp = jwtApp;
            _secretApp = secretApp;
            _logger = logger;
        }

        #region APIs

        /// <summary>
        /// 停用 Jwt 授权数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("deactivate")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _jwtApp.DeactivateCurrentAsync();
            return Ok();
        }

        /// <summary>
        /// 获取 Jwt 授权数据
        /// </summary>
        /// <param name="dto">授权用户信息</param>
        [HttpPost("token")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] SecretDto dto)
        {
            //Todo：获取用户信息
            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "yuiter",
                Role = Guid.Empty,
                Email = "yuiter@yuiter.com",
                Phone = "13912345678",
            };

            if (user == null)
                return Ok(new JwtResponseDto
                {
                    Access = "无权访问",
                    Type = "Bearer",
                    Profile = new Profile
                    {
                        Name = dto.Account,
                        Auths = 0,
                        Expires = 0
                    }
                });

            var jwt = _jwtApp.Create(user);

            return Ok(new JwtResponseDto
            {
                Access = jwt.Token,
                Type = "Bearer",
                Profile = new Profile
                {
                    Name = user.UserName,
                    Auths = jwt.Auths,
                    Expires = jwt.Expires
                }
            });
        }

        /// <summary>
        /// 刷新 Jwt 授权数据
        /// </summary>
        /// <param name="dto">刷新授权用户信息</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] SecretDto dto)
        {
            //Todo：获取用户信息
            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                UserName = "yuiter",
                Role = Guid.Empty,
                Email = "yuiter@yuiter.com",
                Phone = "13912345678",
            };

            if (user == null)
                return Ok(new JwtResponseDto
                {
                    Access = "无权访问",
                    Type = "Bearer",
                    Profile = new Profile
                    {
                        Name = dto.Account,
                        Auths = 0,
                        Expires = 0
                    }
                });

            var jwt = await _jwtApp.RefreshAsync(dto.Token, user);

            return Ok(new JwtResponseDto
            {
                Access = jwt.Token,
                Type = "Bearer",
                Profile = new Profile
                {
                    Name = user.UserName,
                    Auths = jwt.Success ? jwt.Auths : 0,
                    Expires = jwt.Success ? jwt.Expires : 0
                }
            });
        }

        #endregion
    }
}