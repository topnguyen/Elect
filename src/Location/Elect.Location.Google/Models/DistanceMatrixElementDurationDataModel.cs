namespace Elect.Location.Google.Models
{
    public class DistanceMatrixElementDurationDataModel
    {
        /// <summary>
        ///     Displace text depend on "language" params 
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        /// <summary>
        ///     Value always in Second Unit 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
    }
}
