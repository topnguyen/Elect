#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DirectionHelper.cs </Name>
//         <Created> 21/03/2018 9:33:10 AM </Created>
//         <Key> 7e79ca3c-d5ca-419c-9f56-94b3ca2dce29 </Key>
//     </File>
//     <Summary>
//         DirectionHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Coordinate.Models;
using Elect.Location.Models;

namespace Elect.Location.Coordinate.DirectionUtils
{
    public class DirectionHelper
    {
        public static TripModel GetFastestAzTrip(params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.AZ, null, coordinates);
        }

        public static TripModel GetFastestAzTrip(string googleApiKey = null, params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.AZ, googleApiKey, coordinates);
        }

        public static TripModel GetFastestRoundTrip(params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.RoundTrip, null, coordinates);
        }

        public static TripModel GetFastestRoundTrip(string googleApiKey = null, params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.RoundTrip, googleApiKey, coordinates);
        }

        public static TripModel GetFastestTrip(TripType type, params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(type, null, coordinates);
        }

        public static TripModel GetFastestTrip(TripType type, string googleApiKey = null, params CoordinateModel[] coordinates)
        {
            FastestTrip fastestTrip = new FastestTrip(coordinates, type, googleApiKey);

            TripModel tripModel = new TripModel
            {
                CoordinateSequences = fastestTrip.GetTrip(),
                TotalDistanceInMeter = fastestTrip.GetTotalDistanceInMeter(),
                TotalDurationInSecond = fastestTrip.GetTotalDurationInSecond(),
                DistanceDurationMatrix = fastestTrip.DistanceDurationMatrix
            };

            return tripModel;
        }
    }
}