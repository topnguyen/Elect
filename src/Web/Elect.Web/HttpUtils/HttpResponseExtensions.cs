namespace Elect.Web.HttpUtils
{
    public static class HttpResponseExtensions
    {
        public static Task WriteAsync<T>(this HttpResponse response, T actionResult, CancellationToken cancellationToken = default) where T : IActionResult
        {
            return response.HttpContext.WriteAsync(actionResult, cancellationToken);
        }
    }
}
