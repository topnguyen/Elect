namespace Elect.Location.Google.Models
{
    public class TripModel
    {
        public List<CoordinateModel> CoordinateSequences { get; set; }
        public double TotalDistanceInMeter { get; set; }
        public double TotalDurationInSecond { get; set; }
        public DistanceDurationMatrixResultModel DistanceDurationMatrix { get; set; }
    }
}
