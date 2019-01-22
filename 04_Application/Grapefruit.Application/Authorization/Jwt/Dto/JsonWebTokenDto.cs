//-----------------------------------------------------------------------
// <copyright file= "JsonWebTokenDto.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/22 13:29:14 
// Modified by:
// Description: Json Web Token 数据传输实体
//-----------------------------------------------------------------------

namespace Grapefruit.Application.Authorization.Jwt.Dto
{
    public class JsonWebTokenDto
    {
        /// <summary>
        /// 访问 Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 刷新 Token
        /// </summary>
        public string Refresh { get; set; }

        /// <summary>
        /// 授权时间
        /// </summary>
        public long Auths { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long Expires { get; set; }
    }
}
