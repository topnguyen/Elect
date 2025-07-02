namespace Elect.Web.Middlewares.MinResponseMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectMinResponse(this IApplicationBuilder app)
        {
            app.UseWebMarkupMin();
            return app;
        }
    }
}
