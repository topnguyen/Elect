namespace Elect.Web.Middlewares.ServerInfoMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectServerInfo(this IApplicationBuilder app)
        {
            app.UseMiddleware<ElectServerInfoMiddleware>();
            return app;
        }
        public class ElectServerInfoMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ElectServerInfoOptions _options;
            public ElectServerInfoMiddleware(RequestDelegate next, IOptions<ElectServerInfoOptions> configuration)
            {
                _next = next;
                _options = configuration.Value;
            }
            public Task Invoke(HttpContext context)
            {
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.Server, _options.ServerName);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XPoweredBy, _options.PoweredBy);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorName, _options.AuthorName);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorWebsite, _options.AuthorWebsite);
                    httpContext.Response.Headers.AddOrUpdate(HeaderKey.XAuthorEmail, _options.AuthorEmail);
                    return Task.CompletedTask;
                }, context);
                return _next(context);
            }
        }
    }
}
