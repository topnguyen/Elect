namespace Elect.Web.Middlewares.RequestRewindMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Enable Rewind help to get Request Body content. 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElectRequestRewind(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ElectRequestRewindMiddleware>();
        }
        public class ElectRequestRewindMiddleware
        {
            private readonly RequestDelegate _next;
            public ElectRequestRewindMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public Task Invoke(HttpContext context)
            {
                // Allows using several time the stream in ASP.Net Core. Enable Rewind help to get
                // Request Body content.
                // https://github.com/aspnet/AspNetCore/issues/12505: Change EnableRewind to EnableBuffering
                context.Request.EnableBuffering();
                return _next(context);
            }
        }
    }
}
