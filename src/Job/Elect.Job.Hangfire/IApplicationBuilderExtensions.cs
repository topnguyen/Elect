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
                app.UseMiddleware<HangfireDashboardAccessMiddleware>();

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

        public class HangfireDashboardAccessMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ElectHangfireOptions _options;

            public HangfireDashboardAccessMiddleware(RequestDelegate next, IOptions<ElectHangfireOptions> configuration)
            {
                _next = next;
                _options = configuration.Value;
            }

            public async Task Invoke(HttpContext context)
            {
                if (context.Request.IsRequestFor(_options.Url) && !HangfireHelper.IsCanAccessHangfireDashboard(context, _options))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;

                    context.Response.Headers.Clear();

                    await context.Response.WriteAsync(_options.UnAuthorizeMessage).ConfigureAwait(true);

                    return;
                }

                await _next.Invoke(context).ConfigureAwait(true);
            }
        }
    }
}