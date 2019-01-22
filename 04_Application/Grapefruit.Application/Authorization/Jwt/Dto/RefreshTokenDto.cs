//-----------------------------------------------------------------------
// <copyright file= "RefreshTokenDto.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/22 21:36:55 
// Modified by:
// Description: Json Web Token 数据刷新实体
//-----------------------------------------------------------------------

namespace Grapefruit.Application.Authorization.Jwt.Dto
{
    public class RefreshTokenDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 刷新后的 Token 信息
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 是否停用刷新后的 Token
        /// </summary>
        public bool Revoked { get; set; }
    }
}
