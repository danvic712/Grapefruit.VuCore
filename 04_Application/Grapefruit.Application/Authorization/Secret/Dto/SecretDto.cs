//-----------------------------------------------------------------------
// <copyright file= "SecretDto.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/12/19 15:42:20 
// Modified by:
// Description: 用户登录获取 Token 数据传输对象
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
