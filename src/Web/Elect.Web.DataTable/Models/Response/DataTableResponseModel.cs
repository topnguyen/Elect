namespace Elect.Web.DataTable.Models.Response
{
    public class DataTableResponseModel<T>: ElectDisposableModel where T : class, new()
    {
        [JsonProperty(PropertyName = PropertyConstants.TotalRecords)]
        public int TotalRecord { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.TotalDisplayRecords)]
        public int TotalDisplayRecord { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Echo)]
        public int Echo { get; set; }
        [JsonProperty(PropertyName = PropertyConstants.Data)]
        public object[] Data { get; set; }
        [JsonIgnore] public Type DataType { get; } = typeof(T);
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
        public DataTableResponseModel<T> Transform<TData, TTransform>(Func<TData, TTransform> transformRow)
        {
            var data = new DataTableResponseModel<T>
            {
                Data = Data.Cast<TData>().Select(transformRow).Cast<object>().ToArray(),
                TotalDisplayRecord = TotalDisplayRecord,
                TotalRecord = TotalRecord,
                Echo = Echo
            };
            return data;
        }
    }
}
