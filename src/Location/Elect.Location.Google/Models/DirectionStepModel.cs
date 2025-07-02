namespace Elect.Location.Google.Models
{
    public class DirectionStepModel
    {
        public int Index { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public string DistanceText { get; set; }
        public double Duration { get; set; }
        public string DurationText { get; set; }
    }
}
