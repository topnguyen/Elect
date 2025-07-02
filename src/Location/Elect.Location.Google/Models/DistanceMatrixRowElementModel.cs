namespace Elect.Location.Google.Models
{
    public class DistanceMatrixRowElementModel
    {
        [JsonProperty(PropertyName = "distance")]
        public DistanceMatrixElementDistanceDataModel Distance { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public DistanceMatrixElementDurationDataModel Duration { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
