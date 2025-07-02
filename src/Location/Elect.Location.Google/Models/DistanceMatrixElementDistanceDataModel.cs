namespace Elect.Location.Google.Models
{
    public class DistanceMatrixElementDistanceDataModel
    {
        /// <summary>
        ///     Displace text depend on "units" and "language" params 
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        ///     Value always in Meters Unit 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
    }
}
