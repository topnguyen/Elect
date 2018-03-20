#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectLocationConstants.cs </Name>
//         <Created> 20/03/2018 2:15:17 PM </Created>
//         <Key> 0fcd895e-d9de-4602-b26d-633a661cd5c4 </Key>
//     </File>
//     <Summary>
//         ElectLocationConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

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