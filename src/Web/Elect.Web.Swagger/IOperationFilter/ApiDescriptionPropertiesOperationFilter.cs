namespace Elect.Web.Swagger.IOperationFilter
{
    public class ApiDescriptionPropertiesOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!string.IsNullOrWhiteSpace(operation.OperationId))
            {
                context.ApiDescription.Properties.AddOrUpdate(nameof(operation.OperationId), operation.OperationId);
            }
            if (!string.IsNullOrWhiteSpace(operation.Summary))
            {
                context.ApiDescription.Properties.AddOrUpdate(nameof(operation.Summary), operation.Summary);
            }
            if (!string.IsNullOrWhiteSpace(operation.Description))
            {
                context.ApiDescription.Properties.AddOrUpdate(nameof(operation.Description), operation.Description);
            }
        }
    }
}
