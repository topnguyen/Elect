namespace Elect.Location.Models
{
    public class CoordinateModel
    {
        public CoordinateModel()
        {
        }
        public CoordinateModel(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        /// <summary>
        ///     Cluster Purpose 
        /// </summary>
        public int GroupNo { get; set; } = -1;
        /// <summary>
        ///     Route Sequence Purpose 
        /// </summary>
        public int SequenceNo { get; set; } = -1;
    }
}
