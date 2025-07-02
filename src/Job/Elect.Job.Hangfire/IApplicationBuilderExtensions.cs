namespace Elect.Job.Hangfire
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectHangfire(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectHangfireOptions>>().Value;
            if (!options.IsEnable)
            {
                return app;
            }
            if (!options.IsDisableJobDashboard)
            {
                CheckHelper.CheckNullOrWhiteSpace(options.Url, nameof(options.Url));
                app.UseMiddleware<ElectHangfireMiddleware>();
                app.UseHangfireDashboard(options.Url, new DashboardOptions
                {
                    Authorization = new[] { new ElectDashboardAuthorizationFilter() },
                    AppPath = options.BackToUrl,
                    StatsPollingInterval = options.StatsPollingInterval
                });
            }
            app.UseHangfireServer();
            return app;
        }
        public class ElectHangfireMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ElectHangfireOptions _options;
            private readonly RouteCollection _routeCollection;
            public ElectHangfireMiddleware(RequestDelegate next, IOptions<ElectHangfireOptions> configuration, RouteCollection routeCollection)
            {
                _next = next;
                _options = configuration.Value;
                _routeCollection = routeCollection;
            }
            public async Task Invoke(HttpContext context)
            {
                // Check is request to Job Dashboard
                var route = _routeCollection.FindDispatcher(context.Request.Path.Value.Replace(_options.Url, string.Empty));
                var dashboardRequestUrl = route == null ? _options.Url : $@"{_options.Url}/{route.Item2.Value.Trim('/')}";
                var isRequestToHangfireDashboard = context.Request.IsRequestFor(dashboardRequestUrl);
                if (route == null || !isRequestToHangfireDashboard)
                {
                    await _next.Invoke(context).ConfigureAwait(true);
                    return;
                }
                // Set cookie if need
                string requestAccessKey = context.Request.Query[HangfireHelper.AccessKeyName];
                if (!string.IsNullOrWhiteSpace(requestAccessKey) && context.Request.Cookies[HangfireHelper.AccessKeyName] != requestAccessKey)
                {
                    SetCookie(context, HangfireHelper.CookieAccessKeyName, requestAccessKey);
                }
                // Check Permission
                bool isCanAccess = HangfireHelper.IsCanAccessHangfireDashboard(context, _options);
                if (!isCanAccess)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(_options.UnAuthorizeMessage).ConfigureAwait(true);
                    return;
                }
                await _next.Invoke(context).ConfigureAwait(true);
            }
            private static void SetCookie(HttpContext context, string key, string value)
            {
                context.Response.Cookies.Append(key, value, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false // allow transmit via http and https
                });
            }
        }
    }
}
