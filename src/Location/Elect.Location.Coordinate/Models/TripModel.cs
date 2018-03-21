#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TripModel.cs </Name>
//         <Created> 21/03/2018 9:34:08 AM </Created>
//         <Key> 57cd4089-e649-4eb7-9a41-ea93d9818ee4 </Key>
//     </File>
//     <Summary>
//         TripModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Google.Models;
using Elect.Location.Models;
using System.Collections.Generic;

namespace Elect.Location.Coordinate.Models
{
    public class TripModel
    {
        public List<CoordinateModel> CoordinateSequences { get; set; }

        public double TotalDistanceInMeter { get; set; }

        public double TotalDurationInSecond { get; set; }

        public DistanceDurationMatrixResultModel DistanceDurationMatrix { get; set; }
    }
}