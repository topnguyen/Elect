namespace Elect.Web.Middlewares.CorsMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectCors(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectCorsOptions>>().Value;
            app.UseCors(options.PolicyName);
            return app;
        }
    }
}
