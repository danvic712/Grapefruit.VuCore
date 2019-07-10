//-----------------------------------------------------------------------
// <copyright file= "SecretDto.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/12/19 15:42:20 
// Modified by:
// Description: 用户登录实体
//-----------------------------------------------------------------------


namespace Grapefruit.Application.Authorization.Secret.Dto
{
    public class SecretDto
    {
        /// <summary>
        /// 账号名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登录后授权的 Token
        /// </summary>
        public string Token { get; set; }

    }
}
