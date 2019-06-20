#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> GlobalParameterOperationFilter.cs </Name>
//         <Created> 18/04/2018 11:29:06 AM </Created>
//         <Key> d36a491e-4418-4ed6-b346-7dad04ff22fe </Key>
//     </File>
//     <Summary>
//         GlobalParameterOperationFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.LinqUtils;
using Elect.Web.Swagger.Models;
using EnumsNET;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Elect.Web.Swagger.IOperationFilter
{
    public class GlobalParameterOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public ElectSwaggerOptions Options { get; }

        public GlobalParameterOperationFilter(IOptions<ElectSwaggerOptions> configuration)
        {
            Options = configuration.Value;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (Options.GlobalParameters?.Any() != true)
            {
                return;
            }

            foreach (var globalParameter in Options.GlobalParameters)
            {
                var parameterInfo = new NonBodyParameter
                {
                    Name = globalParameter.Name,
                    Required = globalParameter.IsRequire,
                    In = globalParameter.In,
                    Type = globalParameter.Type,
                    Description = globalParameter.Description
                };

                operation.Parameters = operation.Parameters.RemoveWhere(x => x.Name == parameterInfo.Name).ToList();

                operation.Parameters.Add(parameterInfo);
            }
        }
    }
}