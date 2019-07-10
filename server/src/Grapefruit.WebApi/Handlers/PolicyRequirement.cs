//-----------------------------------------------------------------------
// <copyright file= "PolicyRequirement.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/15 19:22:05 
// Modified by:
// Description: 自定义策略要求
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Authorization;

namespace Grapefruit.WebApi.Handlers
{
    public class PolicyRequirement : IAuthorizationRequirement
    { }
}
