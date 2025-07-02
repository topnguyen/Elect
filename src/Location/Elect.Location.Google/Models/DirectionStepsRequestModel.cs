namespace Elect.Location.Google.Models
{
    public class DirectionStepsRequestModel
    {
        public CoordinateModel OriginalCoordinate { get; set; }
        public CoordinateModel DestinationCoordinate { get; set; }
        public List<CoordinateModel> WaypointCoodinates { get; set; } = new List<CoordinateModel>();
        public bool IsAvoidHighway { get; set; }
        public bool IsAvoidToll { get; set; }
        public int UnitSystem { get; set; } = 1;
        public string TravelMode { get; set; } = "DRIVING";
        /// <summary>
        ///     Extra query params, ex: "mode=driving", "language=en-US" 
        /// </summary>
        public Dictionary<string, string> AdditionalValues { get; set; } = new Dictionary<string, string>();
    }
}
