namespace Elect.Web.Middlewares.HttpContextMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            HttpContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
