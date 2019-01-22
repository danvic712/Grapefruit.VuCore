//-----------------------------------------------------------------------
// <copyright file= "RoleRequirement.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/15 19:22:05 
// Modified by:
// Description: 角色策略要求
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Grapefruit.WebApi.Core
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public SecurityKey Key { get; set; }

        public RoleRequirement() { }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="expiration"></param>
        /// <param name="key"></param>
        public RoleRequirement(string issuer, string audience, TimeSpan expiration, SecurityKey key)
        {
            Issuer = issuer;
            Audience = audience;
            Expiration = expiration;
            Key = key;
        }
    }
}
