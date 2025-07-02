namespace Elect.Web.DataTable.Models.Menu
{
    public class LanguageModel
    {
        [JsonProperty(PropertyName = PropertyConstants.Processing)]
        public string Processing { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.LengthMenu)]
        public string LengthMenu { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.ZeroRecords)]
        public string ZeroRecord { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Info)]
        public string Info { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.InfoEmpty)]
        public string InfoEmpty { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.InfoFiltered)]
        public string InfoFiltered { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.InfoPostFix)]
        public string InfoPostFix { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Search)]
        public string Search { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Url)]
        public string Url { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Paginate)]
        public PaginateModel Paginate { get; set; }
    }
}
