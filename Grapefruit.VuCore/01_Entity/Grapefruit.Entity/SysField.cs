//-----------------------------------------------------------------------
// <copyright file= "SysField.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2018/10/30 16:58:50 
// Modified by:
// Description: 系统字段
//-----------------------------------------------------------------------
using System;

namespace Grapefruit.Entity
{
    public class SysField
    {
        #region Attribute

        /// <summary>
        /// 创建人主键
        /// </summary>
        public Guid CreatedOID { get; set; }

        /// <summary>
        /// 创建人账户
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 最后修改人主键
        /// </summary>
        public Guid ModifiedOID { get; set; }

        /// <summary>
        /// 最后修改人账户
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        #endregion
    }
}
