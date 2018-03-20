#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DirectionStepsResultModel.cs </Name>
//         <Created> 20/03/2018 11:57:47 AM </Created>
//         <Key> 233aeec7-cf65-4474-9007-d6fe338a5ce9 </Key>
//     </File>
//     <Summary>
//         DirectionStepsResultModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Models;
using System.Collections.Generic;

namespace Elect.Location.Google.Models
{
    public class DirectionStepsResultModel
    {
        public CoordinateModel OriginPoint { get; set; }

        public CoordinateModel DestinationPoint { get; set; }

        public double TotalDuration { get; set; }

        public string TotalDurationText { get; set; }

        public double TotalDistance { get; set; }

        public string TotalDistanceText { get; set; }

        public string OriginAddress { get; set; }

        public string DestinationAddress { get; set; }

        public List<DirectionStepModel> Steps { get; set; }
    }
}