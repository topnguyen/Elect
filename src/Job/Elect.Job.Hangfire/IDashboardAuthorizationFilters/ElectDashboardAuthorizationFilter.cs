namespace Elect.Job.Hangfire.IDashboardAuthorizationFilters
{
    public class ElectDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([Core.Attributes.NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var options = httpContext.RequestServices.GetService<IOptions<ElectHangfireOptions>>().Value;
            var isCanAccess = HangfireHelper.IsCanAccessHangfireDashboard(httpContext, options);
            return isCanAccess;
        }
    }
}
