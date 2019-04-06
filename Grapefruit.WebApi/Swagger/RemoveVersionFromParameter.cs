//-----------------------------------------------------------------------
// <copyright file= "RemoveVersionFromParameter.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/4/6 21:34:05 
// Modified by:
// Description: 删除参数中的 api 版本参数
//-----------------------------------------------------------------------
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Grapefruit.WebApi.Swagger
{
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
