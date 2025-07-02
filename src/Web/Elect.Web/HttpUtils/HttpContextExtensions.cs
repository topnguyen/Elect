namespace Elect.Web.HttpUtils
{
    public static class HttpContextExtensions
    {
        private static readonly RouteData EmptyRouteData = new RouteData();
        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();
        public static Task WriteAsync<T>(this HttpContext context, T actionResult, CancellationToken cancellationToken = default) where T : IActionResult
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var executor = context.RequestServices.GetService<IActionResultExecutor<T>>();
            if (executor == null)
            {
                throw new InvalidOperationException($"No result executor for '{typeof(T).FullName}' has been registered.");
            }
            var routeData = context.GetRouteData() ?? EmptyRouteData;
            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);
            return cancellationToken.IsCancellationRequested
                ? Task.CompletedTask
                : executor.ExecuteAsync(actionContext, actionResult);
        }
    }
}
