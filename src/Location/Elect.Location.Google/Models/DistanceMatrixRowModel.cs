namespace Elect.Location.Google.Models
{
    public class DistanceMatrixRowModel
    {
        [JsonProperty(PropertyName = "elements")]
        public DistanceMatrixRowElementModel[] Elements { get; set; }
    }
}
