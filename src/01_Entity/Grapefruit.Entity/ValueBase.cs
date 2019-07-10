//-----------------------------------------------------------------------
// <copyright file= "ValueBase.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/2/12 18:28:27 
// Modified by:
// Description: 泛型值对象基类
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Grapefruit.Entity
{
    /// <summary>
    /// 泛型值对象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueBase<T> where T : ValueBase<T>
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
