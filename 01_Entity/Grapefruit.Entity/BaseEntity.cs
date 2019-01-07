//-----------------------------------------------------------------------
// <copyright file= "BaseEntity.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/7 20:05:46 
// Modified by:
// Description: 泛型实体基类
//-----------------------------------------------------------------------
using System;

namespace Grapefruit.Entity
{
    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键</typeparam>
    public abstract class BaseEntity<TPrimaryKey>
    {
        #region Attributes

        /// <summary>
        /// 主键
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual TPrimaryKey Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后修改者
        /// </summary>
        public virtual TPrimaryKey Editor { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

        #endregion
    }

    #region Implements

    /// <summary>
    /// Guid 类型主键实体基类
    /// </summary>
    public abstract class BaseEntity : BaseEntity<Guid>
    { }

    #endregion
}
