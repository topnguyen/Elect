#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ApiDocGroupOperationFilter.cs </Name>
//         <Created> 10/04/2018 4:10:27 PM </Created>
//         <Key> 1e25bcf7-26c3-43ab-8936-019614e62f20 </Key>
//     </File>
//     <Summary>
//         ApiDocGroupOperationFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.Swagger.Attributes;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elect.Web.Swagger.IOperationFilter
{
    public class ApiDocGroupOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
            {
                return;
            }

            // Get Api Doc Groups from Action
            var listApiDocGroup = controllerActionDescriptor.MethodInfo.GetCustomAttributes<ApiDocGroupAttribute>(true).ToList();

            // If action not have any group => get from Controller
            if (!listApiDocGroup.Any())
            {
                listApiDocGroup = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<ApiDocGroupAttribute>(true).ToList();
            }

            if (listApiDocGroup.Any())
            {
                operation.Tags = listApiDocGroup.Where(x => !string.IsNullOrWhiteSpace(x.GroupName)).Select(x => x.GroupName).ToArray();
            }
            else
            {
                // Standalize Group Name auto generate by Controller

                var listGroupNames = new List<string>();

                if (operation.Tags?.Any() == true)
                {
                    foreach (var operationTag in operation.Tags)
                    {
                        var groupName = operationTag.Humanize(LetterCasing.Title);

                        listGroupNames.Add(groupName);
                    }

                    operation.Tags = listGroupNames;
                }
            }
        }
    }
}