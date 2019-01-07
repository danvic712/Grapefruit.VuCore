//-----------------------------------------------------------------------
// <copyright file= "IRepository.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/7 19:45:45 
// Modified by:
// Description: 泛型仓储接口
//-----------------------------------------------------------------------
using Grapefruit.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grapefruit.Domain
{
    /// <summary>
    /// 泛型仓储接口定义
    /// </summary>
    public interface IRepository
    { }

    /// <summary>
    /// 泛型仓储接口定义
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : BaseEntity<TPrimaryKey>
    {
        #region APIs

        /// <summary>
        /// 根据主键获取实体信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        TEntity GetEntityById(TPrimaryKey id);

        #endregion
    }

    #region Implements

    /// <summary>
    /// Guid 主键泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, Guid> where TEntity : BaseEntity
    { }

    #endregion
}
