//-----------------------------------------------------------------------
// <copyright file= "DapperMiddleware.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/31 15:00:58 
// Modified by:
// Description: 加载 SQL 中间件
//-----------------------------------------------------------------------
using Grapefruit.Infrastructure.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Grapefruit.WebApi.Middlewares
{
    public class DapperMiddleware
    {
        private readonly ILogger _logger;

        private readonly IDataRepository _repository;

        private readonly RequestDelegate _request;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        /// <param name="request"></param>
        public DapperMiddleware(IDataRepository repository, ILogger<DapperMiddleware> logger, RequestDelegate request)
        {
            _repository = repository;
            _logger = logger;
            _request = request;
        }

        /// <summary>
        /// 注入中间件到HttpContext中
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"加载存储XML文件DLL，开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            //加载存储xml的dll
            _repository.LoadDataXmlStore();

            _logger.LogInformation($"加载完成存储XML文件DLL，结束时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");

            await _request(context);
        }
    }
}
