//-----------------------------------------------------------------------
// <copyright file= "DataAccessProxy.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/10 11:17:18 
// Modified by:
// Description: 数据层访问接口代理
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Grapefruit.Infrastructure.Dapper
{
    public class DataAccessProxy : IDataAccess
    {
        #region Initialize

        private readonly IDataAccess _dataAccess;

        public DataAccessProxy(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException("dataAccess is null");
        }

        #endregion

        #region APIs

        public bool CloseConnection(IDbConnection connection)
        {
            return _dataAccess.CloseConnection(connection);
        }

        public IDbConnection DbConnection()
        {
            return _dataAccess.DbConnection();
        }

        public object Execute(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.Execute(sql, param, hasTransaction, commandType);
        }

        public object Execute(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.Execute(sql, param, transaction, connection, commandType);
        }

        public IList<T> ExecuteIList<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteIList<T>(sql, param, hasTransaction, commandType);
        }

        public IList<T> ExecuteIList<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteIList<T>(sql, param, transaction, connection, commandType);
        }

        public int ExecuteNonQuery(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteNonQuery(sql, param, hasTransaction, commandType);
        }

        public int ExecuteNonQuery(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteNonQuery(sql, param, transaction, connection, commandType);
        }

        public T ExecuteScalar<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteScalar<T>(sql, param, hasTransaction, commandType);
        }

        #endregion
    }
}
