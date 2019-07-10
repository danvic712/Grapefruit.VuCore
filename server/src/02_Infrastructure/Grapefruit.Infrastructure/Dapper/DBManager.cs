//-----------------------------------------------------------------------
// <copyright file= "DBManager.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/3 20:11:45 
// Modified by:
// Description: 数据层访问
//-----------------------------------------------------------------------
using Grapefruit.Infrastructure.Configuration;
using System;

namespace Grapefruit.Infrastructure.Dapper
{
    public class DBManager
    {
        #region Initialize

        [ThreadStatic]
        private static IDataAccess _sMsSqlFactory;

        [ThreadStatic]
        private static IDataAccess _sMySqlFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        private static IDataAccess CreateDataAccess(ConnectionParameter cp)
        {
            return new DataAccessProxy(DataAccessProxyFactory.Create(cp));
        }

        #endregion

        #region APIs

        /// <summary>
        /// MsSQL 数据库连接字符串
        /// </summary>
        public static IDataAccess MsSQL
        {
            get
            {
                ConnectionParameter cp;
                if (_sMsSqlFactory == null)
                {
                    cp = new ConnectionParameter
                    {
                        ConnectionString = ConfigurationManager.GetConfig("ConnectionStrings:MsSQLConnection"),
                        DataBaseType = DataBaseTypeEnum.SqlServer
                    };
                    _sMsSqlFactory = CreateDataAccess(cp);
                }
                return _sMsSqlFactory;
            }
        }

        /// <summary>
        /// MySQL 数据库连接字符串
        /// </summary>
        public static IDataAccess MySQL
        {
            get
            {
                ConnectionParameter cp;
                if (_sMySqlFactory == null)
                {
                    cp = new ConnectionParameter
                    {
                        ConnectionString = ConfigurationManager.GetConfig("ConnectionStrings:MySQLConnection"),
                        DataBaseType = DataBaseTypeEnum.MySql
                    };
                    _sMySqlFactory = CreateDataAccess(cp);
                }
                return _sMySqlFactory;
            }
        }

        #endregion
    }
}
