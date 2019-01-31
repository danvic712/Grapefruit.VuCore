//-----------------------------------------------------------------------
// <copyright file= "IDataRepository.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/31 14:45:58 
// Modified by:
// Description: SQL仓储接口
//-----------------------------------------------------------------------

namespace Grapefruit.Infrastructure.Dapper
{
    public interface IDataRepository
    {
        /// <summary>
        /// 获取 SQL 语句
        /// </summary>
        /// <param name="commandName"></param>
        /// <returns></returns>
        string GetCommandSQL(string commandName);

        /// <summary>
        /// 批量写入 SQL 语句
        /// </summary>
        void LoadDataXmlStore();
    }
}
