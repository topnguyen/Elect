﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> PositionHelper.cs </Name>
//         <Created> 20/03/2018 11:13:28 PM </Created>
//         <Key> 62ac07cf-0579-47f7-b66a-b7a39c140eb2 </Key>
//     </File>
//     <Summary>
//         PositionHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Models;
using System;

namespace Elect.Location.Coordinate.PositionUtils
{
    public class PositionHelper
    {
        /// <summary>
        ///     Get Top Left Coordinate of square (out bound of circle) corner 
        /// </summary>
        /// <param name="origin">         </param>
        /// <param name="radiusKilometer"></param>
        /// <returns></returns>
        public static CoordinateModel GetTopLeftOfSquare(CoordinateModel origin, double radiusKilometer)
        {
            var hypotenuseLength = GetHypotenuseLength(radiusKilometer);

            var topLeft = GetDerivedPosition(origin, hypotenuseLength, 315);

            return topLeft;
        }

        /// <summary>
        ///     Get Bot Right Coordinate of square (out bound of circle) corner 
        /// </summary>
        /// <param name="origin">         </param>
        /// <param name="radiusKilometer"></param>
        /// <returns></returns>
        public static CoordinateModel GetBotRightOfSquare(CoordinateModel origin, double radiusKilometer)
        {
            var hypotenuseLength = GetHypotenuseLength(radiusKilometer);

            var botRight = GetDerivedPosition(origin, hypotenuseLength, 135);

            return botRight;
        }

        /// <summary>
        ///     Calculates the end-point from a given source at a given range (kilometers) and
        ///     bearing (degrees). methods uses simple geometry equations to calculate the end-point.
        /// </summary>
        /// <param name="origin">   Point of origin </param>
        /// <param name="radiusKm"> Radius/Range in Kilometers </param>
        /// <param name="bearing">  Bearing in degrees from 0 to 360 </param>
        /// <returns> End-point from the source given the desired range and bearing. </returns>
        public static CoordinateModel GetDerivedPosition(CoordinateModel origin, double radiusKm, double bearing)
        {
            var latOrigin = origin.Latitude * ElectLocationConstants.DegreesToRadians;

            var lngOrigin = origin.Longitude * ElectLocationConstants.DegreesToRadians;

            var angularDistance = radiusKm / ElectLocationConstants.EarthRadiusKilometer;

            var trueCourse = bearing * ElectLocationConstants.DegreesToRadians;

            var lat = Math.Asin(
                Math.Sin(latOrigin) * Math.Cos(angularDistance) +
                Math.Cos(latOrigin) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

            var dlon = Math.Atan2(
                Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latOrigin),
                Math.Cos(angularDistance) - Math.Sin(latOrigin) * Math.Sin(lat));

            var lon = (lngOrigin + dlon + Math.PI) % (Math.PI * 2) - Math.PI;

            var result = new CoordinateModel(lon * ElectLocationConstants.RadiansToDegrees, lat * ElectLocationConstants.RadiansToDegrees);

            return result;
        }

        /// <summary>
        ///     Get Hypotenuse Edge of the right isosceles triangle 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static double GetHypotenuseLength(double length)
        {
            return Math.Sqrt(length * length * 2);
        }
    }
}