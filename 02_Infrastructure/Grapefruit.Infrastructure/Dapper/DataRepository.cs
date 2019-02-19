//-----------------------------------------------------------------------
// <copyright file= "DataRepository.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/31 13:51:45 
// Modified by:
// Description: SQL仓储
//-----------------------------------------------------------------------
using Grapefruit.Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Grapefruit.Infrastructure.Dapper
{
    public class DataRepository : IDataRepository
    {
        #region Initialize

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IDistributedCache _cache;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="cache"></param>
        public DataRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        #endregion

        #region APIs

        /// <summary>
        /// 获取SQL语句内容
        /// </summary>
        /// <param name="commandName">命令名称</param>
        /// <returns></returns>
        public string GetCommandSQL(string commandName)
        {
            return GetFromCache(commandName);
        }

        /// <summary>
        /// 根据 RepositoryAssembly 加载 dll（XXX.Domain.dll）中的xml资源文件
        /// </summary>
        public void LoadDataXmlStore()
        {
            string repositoryPrefix = ConfigurationManager.GetConfig("Assembly:RepositoryAssembly");
            if (string.IsNullOrEmpty(repositoryPrefix))
            {
                return;
            }
            foreach (var item in repositoryPrefix.Split('|'))
            {
                string fullPath = AppDomain.CurrentDomain.BaseDirectory + item + ".Domain.dll";
                if (!File.Exists(fullPath))
                {
                    continue;
                }
                LoadCommandXml(fullPath);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 从缓存中获取SQL语句内容
        /// </summary>
        /// <param name="commandName">SQL语句名称</param>
        /// <returns></returns>
        private string GetFromCache(string commandName)
        {
            lock (locker)
            {
                var bytes = _cache.Get(commandName.ToLower());

                if (bytes == null || bytes.Length == 0)
                    return string.Empty;

                return Encoding.Default.GetString(bytes);
            }
        }

        /// <summary>
        /// 载入dll中包含的SQL语句
        /// </summary>
        /// <param name="fullPath">命令名称</param>
        private void LoadCommandXml(string fullPath)
        {
            SqlCommand command = null;
            Assembly dll = Assembly.LoadFile(fullPath);
            string[] xmlFiles = dll.GetManifestResourceNames();
            for (int i = 0; i < xmlFiles.Length; i++)
            {
                Stream stream = dll.GetManifestResourceStream(xmlFiles[i]);
                XElement rootNode = XElement.Load(stream);
                var targetNodes = from n in rootNode.Descendants("Command")
                                  select n;
                foreach (var item in targetNodes)
                {
                    command = new SqlCommand
                    {
                        Name = item.Attribute("Name").Value.ToString(),
                        Sql = item.Value.ToString().Replace("<![CDATA[", "").Replace("]]>", "")
                    };
                    command.Sql = command.Sql.Replace("\r\n", "").Replace("\n", "").Trim();
                    LoadSQL(command.Name, command.Sql);
                }
            }
        }

        /// <summary>
        /// 载入SQL语句
        /// </summary>
        /// <param name="commandName">SQL语句名称</param>
        /// <param name="commandSQL">SQL语句内容</param>
        private string LoadSQL(string commandName, string commandSQL)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentNullException("CommandName is null or empty!");
            }

            string result = GetCommandSQL(commandName);

            if (string.IsNullOrEmpty(result))
            {
                StoreToCache(commandName, commandSQL);
            }

            return result;
        }

        /// <summary>
        /// 将SQL语句存储到缓存中
        /// </summary>
        /// <param name="commandName">SQL语句名称</param>
        /// <param name="commandSQL">SQL语句内容</param>
        private void StoreToCache(string commandName, string commandSQL)
        {
            lock (locker)
            {
                _cache.Set(commandName.ToLower(), Encoding.Default.GetBytes(commandSQL));
            }
        }

        #endregion
    }
}
