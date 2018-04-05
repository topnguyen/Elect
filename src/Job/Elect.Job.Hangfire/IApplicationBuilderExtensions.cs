#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 02/04/2018 6:47:36 PM </Created>
//         <Key> 86834024-5ce1-49ce-ac7e-1d00e279c850 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Job.Hangfire.IDashboardAuthorizationFilters;
using Elect.Job.Hangfire.Models;
using Elect.Job.Hangfire.Utils;
using Elect.Web.HttpUtils;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Elect.Job.Hangfire
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectHangfire(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectHangfireOptions>>().Value;

            if (!string.IsNullOrWhiteSpace(options.Url))
            {
                app.UseMiddleware<ElectHangfireMiddleware>();

                app.UseHangfireDashboard(options.Url, new DashboardOptions
                {
                    Authorization = new[] { new ElectDashboardAuthorizationFilter() },

                    AppPath = options.BackToUrl,

                    StatsPollingInterval = 3000
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
                var route = _routeCollection.FindDispatcher(context.Request.Path.Value.Replace(_options.Url, string.Empty));

                var dashboardRequestUrl = route == null ? _options.Url : $@"{_options.Url}/{route.Item2.Value.Trim('/')}";

                var isRequestToHangfireDashboard = context.Request.IsRequestFor(dashboardRequestUrl);

                if (route == null || !isRequestToHangfireDashboard)
                {
                    await _next.Invoke(context).ConfigureAwait(true);

                    return;
                }

                bool isCanAccess = HangfireHelper.IsCanAccessHangfireDashboard(context, _options);

                // Set cookie if need serve for check access in ElectDashboardAuthorizationFilter.cs
                string accessKey = context.Request.Query[HangfireHelper.AccessKeyName];

                if (!string.IsNullOrWhiteSpace(accessKey) && context.Request.Cookies[HangfireHelper.AccessKeyName] != accessKey)
                {
                    SetCookie(context, HangfireHelper.CookieAccessKeyName, accessKey);
                }

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