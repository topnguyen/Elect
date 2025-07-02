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
