namespace Elect.Location.Google.Models
{
    public class DistanceDurationMatrixRequestModel
    {
        public List<CoordinateModel> OriginalCoordinates { get; set; } = new List<CoordinateModel>();
        public List<CoordinateModel> DestinationCoordinates { get; set; } = new List<CoordinateModel>();
        /// <summary>
        ///     Extra query params, ex: "mode=driving", "language=en-US" 
        /// </summary>
        public Dictionary<string, string> AdditionalValues { get; set; } = new Dictionary<string, string>();
    }
}
