//-----------------------------------------------------------------------
// <copyright file= "ConfigurationManager.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/10 10:23:35 
// Modified by:
// Description: 获取配置信息 
//-----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grapefruit.Infrastructure.Configuration
{
    public class ConfigurationManager
    {
        #region Initialize

        /// <summary>
        /// 加锁防止并发操作
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// 配置实例
        /// </summary>
        private static ConfigurationManager _instance = null;

        /// <summary>
        /// 配置根节点
        /// </summary>
        private IConfigurationRoot Config { get; }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Config = builder.Build();
        }

        /// <summary>
        /// 获取配置实例
        /// </summary>
        /// <returns></returns>
        private static ConfigurationManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_locker)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigurationManager();
                    }
                }
            }

            return _instance;
        }

        #endregion

        #region APIs

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="name">配置节点名称</param>
        /// <returns></returns>
        public static string GetConfig(string name)
        {
            return GetInstance().Config.GetSection(name).Value;
        }

        #endregion
    }
}
