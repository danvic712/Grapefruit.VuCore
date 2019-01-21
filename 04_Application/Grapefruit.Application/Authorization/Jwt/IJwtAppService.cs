//-----------------------------------------------------------------------
// <copyright file= "IJwtAppService.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/21 21:37:15 
// Modified by:
// Description: Jwt Token 操作功能接口
//-----------------------------------------------------------------------
using System.Threading.Tasks;

namespace Grapefruit.Application.Authorization.Jwt
{
    public interface IJwtAppService
    {
        #region APIs

        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCurrentActiveTokenAsync();

        /// <summary>
        /// 停用当前 Token
        /// </summary>
        /// <returns></returns>
        Task DeactivateCurrentAsync();

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        Task<bool> IsActiveAsync(string token);

        /// <summary>
        /// 停用 Token
        /// </summary>
        /// <returns></returns>
        Task DeactivateAsync(string token);

        #endregion
    }
}
