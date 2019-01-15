//-----------------------------------------------------------------------
// <copyright file= "RoleHandler.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/15 19:23:35 
// Modified by:
// Description: 角色策略授权处理
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Grapefruit.WebApi.Core.Policy
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        //授权处理
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            //1、Todo：获取角色、Url 对应关系
            List<Menu> list = new List<Menu> { new Menu
            {
                Role = Guid.Empty.ToString(),
                Url = "/api/v1.0/Values"
            } };

            //验证用户角色是否拥有请求地址权限
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;

            //var url = httpContext.Request.Path.Value.ToLower();
            //var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;

            //var data = list.Where(i => i.Role.Equals(role)).ToList();

            //if (data.Count == 0)
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            //var flag = data.Find(i => i.Url == url);

            //if (flag == null)
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}
            //else
            //{
            //    context.Succeed(requirement);

            //}

            return Task.CompletedTask;
        }

        /// <summary>
        /// 测试类
        /// </summary>
        public class Menu
        {
            public string Role { get; set; }

            public string Url { get; set; }
        }
    }
}
