#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
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

using Elect.Core.LinqUtils;
using Elect.Web.Swagger.Attributes;
using EnumsNET;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace Elect.Web.Swagger.IOperationFilter
{
    public class ParameterOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return;
            }

            var listParameters = controllerActionDescriptor.MethodInfo.GetCustomAttributes<ApiParameterAttribute>(true).ToList();

            if (!listParameters.Any())
            {
                return;
            }

            foreach (var parameter in listParameters)
            {
                var parameterInfo = new NonBodyParameter
                {
                    Name = parameter.Name,
                    Required = parameter.IsRequire,
                    In = parameter.In.AsString(EnumFormat.DisplayName),
                    Type = parameter.Type,
                    Description = parameter.Description
                };

                operation.Parameters = operation.Parameters.RemoveWhere(x => x.Name == parameterInfo.Name).ToList();

                operation.Parameters.Add(parameterInfo);
            }
        }
    }
}