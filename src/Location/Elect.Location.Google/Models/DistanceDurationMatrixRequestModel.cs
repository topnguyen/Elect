#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DistanceDurationMatrixRequestModel.cs </Name>
//         <Created> 20/03/2018 2:30:42 PM </Created>
//         <Key> f669731e-8486-4ae8-9526-714ae9de0700 </Key>
//     </File>
//     <Summary>
//         DistanceDurationMatrixRequestModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Models;
using System.Collections.Generic;

namespace Elect.Location.Google.Models
{
    public class DistanceDurationMatrixRequestModel
    {
        public List<CoordinateModel> OriginalCoordinates { get; set; } = new List<CoordinateModel>();

        public List<CoordinateModel> DestinationCoordinates { get; set; } = new List<CoordinateModel>();

        /// <summary>
        ///     Extra query params, ex: "mode=driving", "language=en-US" 
        /// </summary>
        public Dictionary<string, string> AdditionalValues { get; set; } = new Dictionary<string, string>();
    }
}