//-----------------------------------------------------------------------
// <copyright file= "RoleHandler.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/15 19:23:35 
// Modified by:
// Description: 角色策略授权处理
//-----------------------------------------------------------------------
using Grapefruit.Application.Authorization.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Grapefruit.WebApi.Core
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private readonly IJwtAppService _jwtApp;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="jwtApp"></param>
        public RoleHandler(IAuthenticationSchemeProvider schemes, IJwtAppService jwtApp)
        {
            Schemes = schemes;
            _jwtApp = jwtApp;
        }

        //授权处理
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            //1、Todo：获取角色、Url 对应关系
            List<Menu> list = new List<Menu> {
                new Menu
                {
                    Role = Guid.Empty.ToString(),
                    Url = "/api/v1.0/Values"
                },
                new Menu
                {
                    Role=Guid.Empty.ToString(),
                    Url="/api/v1.0/secret/token/cancel"
                },
                new Menu
                {
                    Role=Guid.Empty.ToString(),
                    Url="/api/v1.0/secret/token/refresh"
                }
            };

            //验证用户角色是否拥有请求地址权限
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;

            var url = httpContext.Request.Path.Value.ToLower();

            //判断请求是否停止
            var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler
                    && await handler.HandleRequestAsync())
                {
                    context.Fail();
                    return;
                }
            }
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result.Succeeded)
                {
                    //判断是否为过期 Token 信息
                    if (!await _jwtApp.IsCurrentActiveTokenAsync())
                    {
                        context.Fail();
                        return;
                    }

                    httpContext.User = result.Principal;
                    //判断角色与 Url 是否对应
                    //
                    var role = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;
                    var menu = list.Where(x => x.Role.Equals(role) && x.Url.ToLower().Equals(url)).FirstOrDefault();

                    if (menu == null)
                    {
                        context.Fail();
                        return;
                    }

                    //判断过期时间
                    if (DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration).Value) >= DateTime.UtcNow)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    return;
                }
            }
            //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
            if (!url.Equals("") && (!httpContext.Request.Method.Equals("POST")
               || !httpContext.Request.HasFormContentType))
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
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
