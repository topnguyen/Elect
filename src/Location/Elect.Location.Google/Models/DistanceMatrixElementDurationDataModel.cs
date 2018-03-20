#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DistanceMatrixElementDurationDataModel.cs </Name>
//         <Created> 20/03/2018 2:26:22 PM </Created>
//         <Key> 36acde36-de83-4d80-ad75-639244cd19c4 </Key>
//     </File>
//     <Summary>
//         DistanceMatrixElementDurationDataModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Location.Google.Models
{
    public class DistanceMatrixElementDurationDataModel
    {
        /// <summary>
        ///     Displace text depend on "language" params 
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        ///     Value always in Second Unit 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
    }
}