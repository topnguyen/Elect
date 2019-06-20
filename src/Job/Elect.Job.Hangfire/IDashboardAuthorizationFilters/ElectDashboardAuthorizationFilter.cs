#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectDashboardAuthorizationFilter.cs </Name>
//         <Created> 02/04/2018 7:01:53 PM </Created>
//         <Key> 965fc44a-01cf-4642-a433-3ffda6c97db0 </Key>
//     </File>
//     <Summary>
//         ElectDashboardAuthorizationFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Job.Hangfire.Models;
using Elect.Job.Hangfire.Utils;
using Hangfire.Dashboard;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Elect.Job.Hangfire.IDashboardAuthorizationFilters
{
    public class ElectDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var options = httpContext.RequestServices.GetService<IOptions<ElectHangfireOptions>>().Value;

            var isCanAccess = HangfireHelper.IsCanAccessHangfireDashboard(httpContext, options);

            return isCanAccess;
        }
    }
}