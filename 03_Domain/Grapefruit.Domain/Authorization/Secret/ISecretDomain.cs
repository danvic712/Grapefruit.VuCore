//-----------------------------------------------------------------------
// <copyright file= "ISecretDomain.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/2/17 13:50:47 
// Modified by:
// Description: 
//-----------------------------------------------------------------------
using Grapefruit.Entity.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grapefruit.Domain.Authorization.Secret
{
    public interface ISecretDomain
    {
        #region APIs

        /// <summary>
        /// 根据帐户名、密码获取用户实体信息
        /// </summary>
        /// <param name="account">账户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<IdentityUser> GetUserForLoginAsync(string account, string password);

        #endregion
    }
}
