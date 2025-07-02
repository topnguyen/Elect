namespace Elect.Web.DataTable.Models.Menu
{
    public class PaginateModel
    {
        [JsonProperty(PropertyName = PropertyConstants.First)]
        public string First { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Previous)]
        public string Previous { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Next)]
        public string Next { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Last)]
        public string Last { get; set; }
    }
}
