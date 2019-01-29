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
using System.Threading.Tasks;

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

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns></returns>
        public bool CloseConnection(IDbConnection connection)
        {
            return _dataAccess.CloseConnection(connection);
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection DbConnection()
        {
            return _dataAccess.DbConnection();
        }

        /// <summary>
        /// 执行SQL语句或存储过程返回对象
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public object Execute(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.Execute(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行SQL语句返回对象
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public object Execute(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.Execute(sql, param, transaction, connection, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程返回对象
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<object> ExecuteAsync(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteAsync(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行SQL语句返回对象
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<object> ExecuteAsync(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteAsync(sql, param, transaction, connection, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程，返回IList<T>对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public IList<T> ExecuteIList<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteIList<T>(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程，返回IList<T>对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public IList<T> ExecuteIList<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteIList<T>(sql, param, transaction, connection, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程，返回IList<T>对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<IList<T>> ExecuteIListAsync<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteIListAsync<T>(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程，返回IList<T>对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<IList<T>> ExecuteIListAsync<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteIListAsync<T>(sql, param, transaction, connection, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteNonQuery(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteNonQuery(sql, param, transaction, connection, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<int> ExecuteNonQueryAsync(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteNonQueryAsync(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<int> ExecuteNonQueryAsync(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteNonQueryAsync(sql, param, transaction, connection, commandType);
        }

        /// <summary>
        /// 执行语句返回T对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteScalar<T>(sql, param, hasTransaction, commandType);
        }

        /// <summary>
        /// 执行语句返回T对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        public Task<T> ExecuteScalarAsync<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text)
        {
            return _dataAccess.ExecuteScalarAsync<T>(sql, param, hasTransaction, commandType);
        }

        #endregion
    }
}
