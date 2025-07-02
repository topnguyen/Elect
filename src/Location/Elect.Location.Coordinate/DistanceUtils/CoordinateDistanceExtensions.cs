namespace Elect.Location.Coordinate.DistanceUtils
{
    public static class CoordinateDistanceExtensions
    {
        /// <summary>
        ///     By Spherical law of cosines http://en.wikipedia.org/wiki/Spherical_law_of_cosines 
        /// </summary>
        public static double DistanceTo(this CoordinateModel origin, [NotNull]CoordinateModel destination, [NotNull]UnitOfLengthModel unitOfLength)
        {
            return DistanceHelper.GetDistance(origin, destination, unitOfLength);
        }
        /// <summary>
        ///     Distance to Destination Coordinate in Flat (2D) Map 
        /// </summary>
        /// <param name="origin">     </param>
        /// <param name="destination"></param>
        /// <returns> Miles </returns>
        public static double FlatDistanceTo(this CoordinateModel origin, [NotNull]CoordinateModel destination)
        {
            return DistanceHelper.GetDistanceByFlat(origin, destination);
        }
        /// <summary>
        ///     By Haversine https://en.wikipedia.org/wiki/Haversine_formula 
        /// </summary>
        /// <returns></returns>
        public static double HaversineDistanceTo(this CoordinateModel origin, [NotNull]CoordinateModel destination, [NotNull]UnitOfLengthModel unitOfLength)
        {
            return DistanceHelper.GetDistanceByHaversine(origin, destination, unitOfLength);
        }
        /// <summary>
        ///     By Geographical distance http://en.wikipedia.org/wiki/Geographical_distance 
        /// </summary>
        public static double GeoDistanceTo(this CoordinateModel origin, [NotNull]CoordinateModel destination, [NotNull]UnitOfLengthModel unitOfLength)
        {
            return DistanceHelper.GetDistanceByGeo(origin, destination, unitOfLength);
        }
    }
}
