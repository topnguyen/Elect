namespace Elect.Web.Api.Models
{
    public class PagedResponseModel<T>: ElectDisposableModel where T : class, new()
    {
        [JsonProperty(Order = 6)]
        public virtual int Total { get; set; }
        [JsonProperty(Order = 7)]
        public virtual IEnumerable<T> Items { get; set; }
        /// <summary>
        ///     Will be de-serialize as list property 
        /// </summary>
        [JsonProperty(Order = 8)]
        [JsonExtensionData]
        public virtual Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}
