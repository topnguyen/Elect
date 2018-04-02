#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> PagedResponseModel_T_.cs </Name>
//         <Created> 02/04/2018 9:43:16 AM </Created>
//         <Key> d309d0dd-933e-4c46-b4f7-9e5fce9495fd </Key>
//     </File>
//     <Summary>
//         PagedResponseModel_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Elect.Web.Api.Models
{
    public class PagedResponseModel<T> where T : class, new()
    {
        [JsonProperty(Order = 6)]
        public int Total { get; set; }

        [JsonProperty(Order = 7)]
        public IEnumerable<T> Items { get; set; }

        [JsonProperty(Order = 8)]
        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}