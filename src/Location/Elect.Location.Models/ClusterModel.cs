namespace Elect.Location.Models
{
    public class ClusterModel
    {
        public CoordinateModel CenterCoordinate { get; set; }
        public List<CoordinateModel> Coordinates { get; set; } = new List<CoordinateModel>();
    }
}
