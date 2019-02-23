//-----------------------------------------------------------------------
// <copyright file= "IDataAccess.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/10 11:16:26 
// Modified by:
// Description: 数据层访问接口
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Grapefruit.Infrastructure.Dapper
{
    public interface IDataAccess
    {
        #region Initialize

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns></returns>
        bool CloseConnection(IDbConnection connection);

        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns></returns>
        IDbConnection DbConnection();

        #endregion

        #region APIs

        /// <summary>
        /// 执行SQL语句或存储过程返回对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        T Execute<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句返回对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        T Execute<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程返回对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<T> ExecuteAsync<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句返回对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<T> ExecuteAsync<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程，返回IList<T>对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        IList<T> ExecuteIList<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

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
        IList<T> ExecuteIList<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程，返回IList<T>对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<IList<T>> ExecuteIListAsync<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

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
        Task<IList<T>> ExecuteIListAsync<T>(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        int ExecuteNonQuery(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL语句或存储过程返回受影响行数
        /// </summary>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="transaction">外部事务</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<int> ExecuteNonQueryAsync(string sql, object param, IDbTransaction transaction, IDbConnection connection, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行语句返回单个值对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        T ExecuteScalar<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行语句返回单个值对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="sql">SQL语句 or 存储过程名</param>
        /// <param name="param">参数</param>
        /// <param name="hasTransaction">是否使用事务</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        Task<T> ExecuteScalarAsync<T>(string sql, object param, bool hasTransaction = false, CommandType commandType = CommandType.Text);

        #endregion
    }
}
