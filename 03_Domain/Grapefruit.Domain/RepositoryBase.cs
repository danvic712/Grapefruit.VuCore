//-----------------------------------------------------------------------
// <copyright file= "RepositoryBase.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/10 19:24:04 
// Modified by:
// Description: 泛型仓储接口实现基类
//-----------------------------------------------------------------------
using Grapefruit.Entity;
using System;

namespace Grapefruit.Domain
{
    /// <summary>
    /// 仓储抽象实现基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>
    {
        #region APIs

        public TEntity GetEntityById(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

    /// <summary>
    /// 主键为 Guid 类型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, Guid> where TEntity : EntityBase
    { }
}
