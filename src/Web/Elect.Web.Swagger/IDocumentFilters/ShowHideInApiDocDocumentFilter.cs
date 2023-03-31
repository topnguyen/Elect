#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ShowHideInApiDocDocumentFilter.cs </Name>
//         <Created> 24/03/2018 11:49:15 PM </Created>
//         <Key> a88793f4-dc72-4bf2-bbdd-b2d0591fea8d </Key>
//     </File>
//     <Summary>
//         ShowHideInApiDocDocumentFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Elect.Web.Swagger.IDocumentFilters
{
    public class ShowHideInApiDocDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var apiDescription in context.ApiDescriptions)
            {
                if (!(apiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor))
                {
                    continue;
                }

                bool isHideInApiDoc = true;

                // Method / Action level
                var isHideInApiDocAttributeInMethod =  controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<HideInApiDocAttribute>(true).Any() || controllerActionDescriptor.MethodInfo.GetCustomAttributes<HideInApiDocAttribute>(true).Any();

                if (!isHideInApiDocAttributeInMethod)
                {
                    var isShowInApiDocAttributeInMethod = controllerActionDescriptor.MethodInfo.GetCustomAttributes<ShowInApiDocAttribute>(true).Any();

                    if (isShowInApiDocAttributeInMethod)
                    {
                        isHideInApiDoc = false;
                    }
                    else
                    {
                        // Type / Controller level

                        var isHideInApiDocAttributeInType = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<HideInApiDocAttribute>(true).Any();

                        if (!isHideInApiDocAttributeInType)
                        {
                            var isShowInApiDocAttributeInType = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<ShowInApiDocAttribute>(true).Any();

                            if (isShowInApiDocAttributeInType)
                            {
                                isHideInApiDoc = false;
                            }
                        }
                    }
                }

                if (!isHideInApiDoc)
                {
                    continue;
                }

                var route = "/" + controllerActionDescriptor.AttributeRouteInfo?.Template?.Trim('/')
                    .Replace(":guid", string.Empty)
                    .Replace(":int", string.Empty)
                    .Replace(":double", string.Empty)
                    .Replace(":long", string.Empty)
                    ;
                    
                if (swaggerDoc.Paths.ContainsKey(route))
                {
                    swaggerDoc.Paths.Remove(route);
                }
            }
        }
    }
}