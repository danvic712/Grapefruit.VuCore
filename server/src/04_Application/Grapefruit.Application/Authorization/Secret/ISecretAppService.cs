//-----------------------------------------------------------------------
// <copyright file= "ISecretAppService.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/12/19 15:02:39 
// Modified by:
// Description: 
//-----------------------------------------------------------------------
using Grapefruit.Application.Authorization.Secret.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grapefruit.Application.Authorization.Secret
{
    public interface ISecretAppService
    {
        #region APIs

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<UserDto> GetCurrentUserAsync(string account, string password);

        #endregion
    }
}
