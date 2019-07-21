//-----------------------------------------------------------------------
// <copyright file= "ValueBase.cs">
//     Copyright (c) Danvic.Wang All rights reserved.
// </copyright>
// Author: Danvic.Wang
// Created DateTime: 2019/7/21 11:46:55
// Modified by:
// Description: 泛型值对象基类
//-----------------------------------------------------------------------

namespace Grapefruit.Entities
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