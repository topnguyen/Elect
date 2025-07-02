namespace Elect.Location.Google.Models
{
    public class DirectionStepsResultModel
    {
        public CoordinateModel OriginPoint { get; set; }
        public CoordinateModel DestinationPoint { get; set; }
        public double TotalDuration { get; set; }
        public string TotalDurationText { get; set; }
        public double TotalDistance { get; set; }
        public string TotalDistanceText { get; set; }
        public string OriginAddress { get; set; }
        public string DestinationAddress { get; set; }
        public List<DirectionStepModel> Steps { get; set; }
    }
}
