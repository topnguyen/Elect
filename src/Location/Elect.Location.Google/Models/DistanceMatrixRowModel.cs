#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DistanceMatrixRowModel.cs </Name>
//         <Created> 20/03/2018 2:24:54 PM </Created>
//         <Key> 4cd9d326-e63d-4ff7-ba2a-5c701e8136d8 </Key>
//     </File>
//     <Summary>
//         DistanceMatrixRowModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Location.Google.Models
{
    public class DistanceMatrixRowModel
    {
        [JsonProperty(PropertyName = "elements")]
        public DistanceMatrixRowElementModel[] Elements { get; set; }
    }
}