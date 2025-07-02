namespace Elect.Web.Middlewares.GCCollectMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Release memory if possible by GC Collection <br />
        ///     [NOTE] Keep this Middleware at the top of the pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <remarks>Keep this Middleware at the top of the pipeline</remarks>
        public static IApplicationBuilder UseGCCollect(this IApplicationBuilder app)
        {
            app.UseMiddleware<GCCollectMiddleware>();
            return app;
        }
        public class GCCollectMiddleware
        {
            private readonly RequestDelegate _next;
            public GCCollectMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public async Task Invoke(HttpContext context)
            {
                await _next(context);
                GC.Collect(2, GCCollectionMode.Forced, true);
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
