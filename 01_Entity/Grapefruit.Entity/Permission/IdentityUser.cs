//-----------------------------------------------------------------------
// <copyright file= "IdentityUser.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/14 21:35:00 
// Modified by:
// Description: 用户实体
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Grapefruit.Entity.Permission
{
    public class IdentityUser : EntityBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 加密参数
        /// </summary>
        public Guid Salt { get; set; }
    }
}
