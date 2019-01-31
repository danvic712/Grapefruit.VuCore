//-----------------------------------------------------------------------
// <copyright file= "SqlCommand.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/31 14:33:19 
// Modified by:
// Description: XML 中的 SQL 对应类
//-----------------------------------------------------------------------

namespace Grapefruit.Infrastructure.Dapper
{
    public class SqlCommand
    {
        /// <summary>
        /// SQL语句名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// SQL语句创建者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// SQL语句版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// SQL语句或存储过程内容
        /// </summary>
        public string Sql { get; set; }
    }
}
