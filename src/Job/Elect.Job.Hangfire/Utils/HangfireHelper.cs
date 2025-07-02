namespace Elect.Job.Hangfire.Utils
{
    public class HangfireHelper
    {
        internal const string AccessKeyName = "key";
        internal const string CookieAccessKeyName = "Elect_Hangfire_AccessKey";
        public static bool IsCanAccessHangfireDashboard(HttpContext httpContext, ElectHangfireOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AccessKey))
            {
                return true;
            }
            string requestKey = httpContext.Request.Query[AccessKeyName];
            requestKey = string.IsNullOrWhiteSpace(requestKey) ? httpContext.Request.Cookies[CookieAccessKeyName] : requestKey;
            var isCanAccess = options.AccessKey == requestKey;
            return isCanAccess;
        }
    }
}
