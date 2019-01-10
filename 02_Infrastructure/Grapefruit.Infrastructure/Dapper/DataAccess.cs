//-----------------------------------------------------------------------
// <copyright file= "DataAccess.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/10 11:17:04 
// Modified by:
// Description: 数据层接口实现
//-----------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Grapefruit.Infrastructure.Dapper
{
    public class DataAccess : IDataAccess
    {
        #region Initialize

        private readonly string _connectionString;

        private readonly DataBaseTypeEnum _dataBaseType;

        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数注入日志记录
        /// </summary>
        /// <param name="logger"></param>
        public DataAccess(ILogger<DataAccess> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="dataBaseType">数据库类型</param>
        public DataAccess(string connectionString, DataBaseTypeEnum dataBaseType)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                _logger.LogError("创建数据库连接失败，错误信息：数据库连接字符串为空");
                throw new ArgumentNullException("Connection String is null or empty");
            }
            _connectionString = connectionString;
            _dataBaseType = dataBaseType;
        }

        #endregion

        #region APIs

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns></returns>
        public bool CloseConnection(IDbConnection connection)
        {
            bool result = false;
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                _logger.LogError($"数据库连接关闭失败，错误信息：{ex}");
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection DbConnection()
        {
            IDbConnection connection = null;
            switch (_dataBaseType)
            {
                case DataBaseTypeEnum.SqlServer:
                    connection = new SqlConnection(_connectionString);
                    break;
                case DataBaseTypeEnum.MySql:
                    connection = new MySqlConnection(_connectionString);
                    break;
            };
            return connection;
        }

        public object Execute(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public object Execute(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteIList<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteIList<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
