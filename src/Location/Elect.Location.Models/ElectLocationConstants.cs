namespace Elect.Location.Models
{
    public class ElectLocationConstants
    {
        public const double MileToKilometer = 1.609344;
        public const double MileToMeter = 1609.344;
        public const double NauticalMileToMile = 0.8684;
        public const double DegreesToRadians = Math.PI / 180.0;
        public const double RadiansToDegrees = 180.0 / Math.PI;
        public const double EarthRadiusMile = RadiansToDegrees * 60 * 1.1515;
        public const double EarthRadiusKilometer = EarthRadiusMile * MileToKilometer;
    }
}
