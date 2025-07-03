namespace Elect.Web.Middlewares.MeasureProcessingTimeMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Response information about executed time 
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseElectMeasureProcessingTime(this IApplicationBuilder app)
        {
            app.UseMiddleware<ElectMeasureProcessingTimeMiddleware>();
            return app;
        }
        public class ElectMeasureProcessingTimeMiddleware
        {
            private readonly RequestDelegate _next;
            public ElectMeasureProcessingTimeMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public Task Invoke(HttpContext context)
            {
                var watch = new Stopwatch();
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;
                    watch.Stop();
                    var elapsedMilliseconds = watch.ElapsedMilliseconds.ToString("N");
                    httpContext.Response.Headers.Append(HeaderKey.XProcessingTimeMilliseconds, elapsedMilliseconds);
                    return Task.CompletedTask;
                }, context);
                watch.Start();
                return _next(context);
            }
        }
    }
}
