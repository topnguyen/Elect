#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace Elect.Web.Swagger.IDocumentFilters
{
    public class ShowHideInApiDocDocumentFilter : IDocumentFilter
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

                    bool isHideInApiDoc = true;

                    // Method / Action level
                    var isHideInApiDocAttributeInMethod = controllerActionDescriptor.MethodInfo.GetCustomAttributes<HideInApiDocAttribute>(true).Any();

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

                    var route = "/" + controllerActionDescriptor.AttributeRouteInfo.Template.Trim('/');

                    var pathItem = swaggerDoc.Paths[route];

                    switch (apiDescription.HttpMethod.ToUpperInvariant())
                    {
                        case "GET":
                            {
                                pathItem.Get = null;
                                break;
                            }
                        case "POST":
                            {
                                pathItem.Post = null;
                                break;
                            }
                        case "PUT":
                            {
                                pathItem.Put = null;
                                break;
                            }
                        case "PATCH":
                            {
                                pathItem.Patch = null;
                                break;
                            }
                        case "DELETE":
                            {
                                pathItem.Delete = null;
                                break;
                            }
                        case "HEAD":
                            {
                                pathItem.Head = null;
                                break;
                            }
                        case "OPTIONS":
                            {
                                pathItem.Options = null;
                                break;
                            }
                    }

                    if (pathItem.Get == null && pathItem.Post == null && pathItem.Put == null &&
                        pathItem.Patch == null && pathItem.Delete == null && pathItem.Head == null &&
                        pathItem.Options == null)
                    {
                        swaggerDoc.Paths.Remove(route);
                    }
                }
            }
        }
    }
}