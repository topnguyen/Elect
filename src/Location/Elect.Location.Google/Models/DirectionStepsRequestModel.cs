#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DirectionStepsRequestModel.cs </Name>
//         <Created> 20/03/2018 3:05:11 PM </Created>
//         <Key> ed9624c2-6a72-416c-bb3d-101fafb8999e </Key>
//     </File>
//     <Summary>
//         DirectionStepsRequestModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Models;
using System.Collections.Generic;

namespace Elect.Location.Google.Models
{
    public class DirectionStepsRequestModel
    {
        public CoordinateModel OriginalCoordinate { get; set; }

        public CoordinateModel DestinationCoordinate { get; set; }

        public List<CoordinateModel> WaypointCoodinates { get; set; } = new List<CoordinateModel>();

        public bool IsAvoidHighway { get; set; }

        public bool IsAvoidToll { get; set; }

        public int UnitSystem { get; set; } = 1;

        public string TravelMode { get; set; } = "DRIVING";

        /// <summary>
        ///     Extra query params, ex: "mode=driving", "language=en-US" 
        /// </summary>
        public Dictionary<string, string> AdditionalValues { get; set; } = new Dictionary<string, string>();
    }
}