#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DistanceMatrixRowElementModel.cs </Name>
//         <Created> 20/03/2018 2:25:53 PM </Created>
//         <Key> 55f5ae51-5ad9-4046-b922-f79927ecd2bc </Key>
//     </File>
//     <Summary>
//         DistanceMatrixRowElementModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Location.Google.Models
{
    public class DistanceMatrixRowElementModel
    {
        [JsonProperty(PropertyName = "distance")]
        public DistanceMatrixElementDistanceDataModel Distance { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public DistanceMatrixElementDurationDataModel Duration { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}