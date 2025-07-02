namespace Elect.Web.Models
{
    [Serializable]
    public sealed class HttpContextModel : ElectDisposableModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public Dictionary<string, List<string>> Headers { get; } = new Dictionary<string, List<string>>();
        public string DisplayUrl { get; }
        public string Protocol { get; }
        public string Method { get; }
        public string Endpoint { get; }
        public Dictionary<string, List<string>> QueryStrings { get; } = new Dictionary<string, List<string>>();
        /// <summary>
        ///     Need to <c> EnableRewind </c> for Request in middleware to get Request Body. 
        /// </summary>
        public object RequestBody { get; }
        public HttpContextModel(HttpContext context)
        {
            if (context == null)
            {
                return;
            }
            Headers = context.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToList());
            DisplayUrl = context.Request.GetDisplayUrl();
            Protocol = context.Request.Protocol;
            Method = context.Request.Method;
            QueryStrings = context.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToList());
            Endpoint = context.Request.GetEndpoint();
            RequestBody = context.Request.GetBody();
        }
    }
}
