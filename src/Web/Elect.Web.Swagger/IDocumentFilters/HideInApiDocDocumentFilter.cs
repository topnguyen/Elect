#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HideInApiDocDocumentFilter.cs </Name>
//         <Created> 24/03/2018 11:49:15 PM </Created>
//         <Key> a88793f4-dc72-4bf2-bbdd-b2d0591fea8d </Key>
//     </File>
//     <Summary>
//         HideInApiDocDocumentFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace Elect.Web.Swagger.IDocumentFilters
{
    public class HideInApiDocDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescriptionGroup in context.ApiDescriptionsGroups.Items)
            {
                foreach (var apiDescription in apiDescriptionGroup.Items)
                {
                    var controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;

                    if (controllerActionDescriptor == null)
                    {
                        continue;
                    }

                    var isHideInDocAttributeInType =
                        controllerActionDescriptor
                            .ControllerTypeInfo
                            .GetCustomAttributes<HideInApiDocAttribute>(true)
                            .Any();

                    var isHideInDocAttributeInMethod =
                        controllerActionDescriptor
                            .MethodInfo
                            .GetCustomAttributes<HideInApiDocAttribute>(true)
                            .Any();

                    if (!isHideInDocAttributeInType && !isHideInDocAttributeInMethod)
                    {
                        continue;
                    }

                    var route = "/" + controllerActionDescriptor.AttributeRouteInfo.Template.TrimEnd('/');

                    swaggerDoc.Paths.Remove(route);
                }
            }
        }
    }
}