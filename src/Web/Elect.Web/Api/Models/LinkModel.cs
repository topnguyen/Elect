namespace Elect.Web.Api.Models
{
    public class LinkModel : ElectDisposableModel
    {
        public string Url { get; set; }
        /// <summary>
        ///     Http method, default is "GET". 
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethod Method { get; set; } = HttpMethod.GET;
        public RouteValueDictionary Data { get; set; } = new RouteValueDictionary();
    }
}
