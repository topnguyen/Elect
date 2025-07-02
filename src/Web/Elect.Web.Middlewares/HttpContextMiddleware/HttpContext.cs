namespace Elect.Web.Middlewares.HttpContextMiddleware
{
    public static class HttpContext
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _contextAccessor;
        /// <summary>
        ///     Get current request HttpContext 
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor?.HttpContext;
        internal static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}
