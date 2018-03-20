#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DistanceMatrixElementDistanceDataModel.cs </Name>
//         <Created> 20/03/2018 2:26:10 PM </Created>
//         <Key> 4becba92-2067-458d-acb7-69e2510e6ba5 </Key>
//     </File>
//     <Summary>
//         DistanceMatrixElementDistanceDataModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Location.Google.Models
{
    public class DistanceMatrixElementDistanceDataModel
    {
        /// <summary>
        ///     Displace text depend on "units" and "language" params 
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        ///     Value always in Meters Unit 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
    }
}