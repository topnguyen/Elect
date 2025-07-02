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
