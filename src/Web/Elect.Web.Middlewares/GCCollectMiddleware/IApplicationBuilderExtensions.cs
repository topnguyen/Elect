namespace Elect.Web.Middlewares.GCCollectMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Conditionally trigger garbage collection based on memory pressure. <br />
        ///     WARNING: This middleware can significantly impact performance. Use only in specific scenarios. <br />
        ///     [NOTE] Keep this Middleware at the top of the pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options">Options to configure GC behavior</param>
        /// <remarks>
        /// This middleware should only be used in very specific scenarios where memory pressure 
        /// is a critical concern. In most applications, the .NET GC handles memory management 
        /// efficiently without manual intervention.
        /// </remarks>
        public static IApplicationBuilder UseGCCollect(this IApplicationBuilder app, GCCollectOptions options = null)
        {
            options ??= new GCCollectOptions();
            app.UseMiddleware<GCCollectMiddleware>(Options.Create(options));
            return app;
        }

        public class GCCollectMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly GCCollectOptions _options;
            private long _requestCount;

            public GCCollectMiddleware(RequestDelegate next, IOptions<GCCollectOptions> options)
            {
                _next = next;
                _options = options?.Value ?? new GCCollectOptions();
            }

            public async Task Invoke(HttpContext context)
            {
                await _next(context).ConfigureAwait(false);

                // Only collect if enabled and conditions are met
                if (_options.Enabled)
                {
                    _requestCount++;

                    bool shouldCollect = false;

                    // Check if we should collect based on request interval
                    if (_options.RequestInterval > 0 && _requestCount % _options.RequestInterval == 0)
                    {
                        shouldCollect = true;
                    }

                    // Check memory threshold
                    if (_options.MemoryThresholdMB > 0)
                    {
                        var memoryUsedMB = GC.GetTotalMemory(false) / (1024 * 1024);
                        if (memoryUsedMB > _options.MemoryThresholdMB)
                        {
                            shouldCollect = true;
                        }
                    }

                    if (shouldCollect)
                    {
                        // Use background GC mode for better performance
                        GC.Collect(_options.Generation, GCCollectionMode.Optimized, false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Options for configuring GC collection behavior
    /// </summary>
    public class GCCollectOptions
    {
        /// <summary>
        /// Enable or disable GC collection. Default is false.
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Collect every N requests. 0 means don't use request-based collection. Default is 0.
        /// </summary>
        public int RequestInterval { get; set; } = 0;

        /// <summary>
        /// Memory threshold in MB. Collect when memory usage exceeds this value. 0 means don't use threshold. Default is 500.
        /// </summary>
        public long MemoryThresholdMB { get; set; } = 500;

        /// <summary>
        /// Generation to collect (0, 1, or 2). Default is 1.
        /// </summary>
        public int Generation { get; set; } = 1;
    }
}