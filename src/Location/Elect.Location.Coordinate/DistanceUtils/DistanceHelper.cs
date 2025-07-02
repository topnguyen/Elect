namespace Elect.Location.Coordinate.DistanceUtils
{
    public class DistanceHelper
    {
        /// <summary>
        ///     By Haversine https://en.wikipedia.org/wiki/Haversine_formula 
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(double originLongitude, double originLatitude, double destinationLongitude, double destinationLatitude)
        {
            var origin = new CoordinateModel(originLongitude, originLatitude);
            var destination = new CoordinateModel(destinationLongitude, destinationLatitude);
            return GetDistanceByHaversine(origin, destination, UnitOfLengthModel.Kilometer);
        }
        /// <summary>
        ///     By Haversine https://en.wikipedia.org/wiki/Haversine_formula 
        /// </summary>
        /// <returns></returns>
        public static double GetDistance(double originLongitude, double originLatitude, double destinationLongitude, double destinationLatitude, [NotNull]UnitOfLengthModel unitOfLength)
        {
            var origin = new CoordinateModel(originLongitude, originLatitude);
            var destination = new CoordinateModel(destinationLongitude, destinationLatitude);
            return GetDistanceByHaversine(origin, destination, unitOfLength);
        }
        /// <summary>
        ///     By Spherical law of cosines http://en.wikipedia.org/wiki/Spherical_law_of_cosines 
        /// </summary>
        public static double GetDistance([NotNull]CoordinateModel origin, [NotNull]CoordinateModel destination, [NotNull]UnitOfLengthModel unitOfLength)
        {
            var theta = origin.Longitude - destination.Longitude;
            var thetaRad = theta * ElectLocationConstants.DegreesToRadians;
            var targetRad = destination.Latitude * ElectLocationConstants.DegreesToRadians;
            var baseRad = origin.Latitude * ElectLocationConstants.DegreesToRadians;
            var distance =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            distance = Math.Acos(distance);
            // calculate to earth radius by miles
            distance = distance * ElectLocationConstants.EarthRadiusMile;
            return unitOfLength.ConvertFromMiles(distance);
        }
        /// <summary>
        ///     Distance to Destination Coordinate in Flat (2D) Map 
        /// </summary>
        /// <param name="origin">     </param>
        /// <param name="destination"></param>
        /// <returns> Miles </returns>
        public static double GetDistanceByFlat([NotNull]CoordinateModel origin, [NotNull]CoordinateModel destination)
        {
            return Math.Abs(origin.Latitude - destination.Latitude) + Math.Abs(origin.Longitude - destination.Longitude);
        }
        /// <summary>
        ///     By Haversine https://en.wikipedia.org/wiki/Haversine_formula 
        /// </summary>
        /// <returns></returns>
        public static double GetDistanceByHaversine([NotNull]CoordinateModel origin, [NotNull]CoordinateModel destination, [NotNull]UnitOfLengthModel unitOfLength)
        {
            var dLat = (destination.Latitude - origin.Latitude) * ElectLocationConstants.DegreesToRadians;
            var dLon = (destination.Longitude - origin.Longitude) * ElectLocationConstants.DegreesToRadians;
            var a = Math.Pow(Math.Sin(dLat / 2), 2) +
                    Math.Cos(origin.Latitude * ElectLocationConstants.DegreesToRadians) *
                    Math.Cos(destination.Latitude * ElectLocationConstants.DegreesToRadians) *
                    Math.Pow(Math.Sin(dLon / 2), 2);
            // central angle, aka arc segment angular distance
            var centralAngle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = ElectLocationConstants.EarthRadiusMile * centralAngle;
            return unitOfLength.ConvertFromMiles(distance);
        }
        /// <summary>
        ///     By Geographical distance http://en.wikipedia.org/wiki/Geographical_distance 
        /// </summary>
        public static double GetDistanceByGeo([NotNull]CoordinateModel origin, [NotNull]CoordinateModel destination, [NotNull]UnitOfLengthModel unitOfLength)
        {
            var radLatOrigin = origin.Latitude * ElectLocationConstants.DegreesToRadians;
            var radLatDestination = destination.Latitude * ElectLocationConstants.DegreesToRadians;
            var dLat = radLatDestination - radLatOrigin;
            var dLon = (destination.Longitude - origin.Longitude) * ElectLocationConstants.DegreesToRadians;
            var a = dLon * Math.Cos((radLatOrigin + radLatDestination) / 2);
            // central angle, aka arc segment angular distance
            var centralAngle = Math.Sqrt(a * a + dLat * dLat);
            // great-circle (orthodromic) distance on Earth between 2 points
            var distance = ElectLocationConstants.EarthRadiusMile * centralAngle;
            return unitOfLength.ConvertFromMiles(distance);
        }
    }
}
