namespace Elect.Web.Swagger.IOperationFilter
{
    public class GlobalParameterOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public ElectSwaggerOptions Options { get; }
        public GlobalParameterOperationFilter(IOptions<ElectSwaggerOptions> configuration)
        {
            Options = configuration.Value;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (Options.GlobalParameters?.Any() != true)
            {
                return;
            }
            foreach (var globalParameter in Options.GlobalParameters)
            {
                operation.Parameters = operation.Parameters.RemoveWhere(x => x.Name == globalParameter.Name).ToList();
                operation.Parameters.Add(globalParameter);
            }
        }
    }
}
