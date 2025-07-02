namespace Elect.Web.Swagger.IOperationFilter
{
    public class ApiDocGroupOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
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
                operation.Tags = listApiDocGroup.Where(x => !string.IsNullOrWhiteSpace(x.GroupName)).Select(x => new OpenApiTag
                {
                    Name = x.GroupName
                }).ToArray();
            }
            else
            {
                // Standalize Group Name auto generate by Controller
                var listGroupNames = new List<string>();
                if (operation.Tags?.Any() == true)
                {
                    foreach (var operationTag in operation.Tags)
                    {
                        var groupName = operationTag.Name;
                        listGroupNames.Add(groupName);
                    }
                    operation.Tags = listGroupNames.Select(x => new OpenApiTag
                    {
                        Name = x
                    }).ToArray();
                }
            }
        }
    }
}
