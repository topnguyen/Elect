#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ParameterOperationFilter.cs </Name>
//         <Created> 18/04/2018 9:10:07 AM </Created>
//         <Key> 0634d6fc-b061-46c0-8318-38e2344f58ec </Key>
//     </File>
//     <Summary>
//         ParameterOperationFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using Elect.Core.LinqUtils;
using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnumsNET;
using Microsoft.OpenApi.Models;

namespace Elect.Web.Swagger.IOperationFilter
{
    public class ParameterOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return;
            }

            var listParameters = controllerActionDescriptor.MethodInfo.GetCustomAttributes<ApiParameterAttribute>(true)
                .ToList();

            if (!listParameters.Any())
            {
                return;
            }

            foreach (var parameter in listParameters)
            {
                var parameterInfo = new OpenApiParameter
                {
                    Name = parameter.Name,
                    In = Enums.Parse<ParameterLocation>(parameter.In),
                    Description = parameter.Description,
                    Required = parameter.Required,
                    AllowEmptyValue = parameter.AllowEmptyValue,
                    Style = Enums.Parse<ParameterStyle>(parameter.Style)
                };

                if (operation.Parameters?.Any() != true)
                {
                    operation.Parameters = new List<OpenApiParameter>();
                }

                operation.Parameters = operation.Parameters.RemoveWhere(x => x.Name == parameterInfo.Name).ToList();

                operation.Parameters.Add(parameterInfo);
            }
        }
    }
}