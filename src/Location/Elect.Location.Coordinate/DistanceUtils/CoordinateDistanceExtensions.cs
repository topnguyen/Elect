#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CoordinateDistanceExtensions.cs </Name>
//         <Created> 20/03/2018 11:07:22 PM </Created>
//         <Key> 8033a21f-5266-4392-9653-664cf820af7c </Key>
//     </File>
//     <Summary>
//         CoordinateDistanceExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Location.Models;

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