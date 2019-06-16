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
using Elect.Core.ObjUtils;

namespace Elect.Web.Api.Models
{
    public class PagedResponseModel<T>: ElectDisposableModel where T : class, new()
    {
        [JsonProperty(Order = 6)]
        public virtual int Total { get; set; }

        [JsonProperty(Order = 7)]
        public virtual IEnumerable<T> Items { get; set; }

        /// <summary>
        ///     Will be de-serialize as list property 
        /// </summary>
        [JsonProperty(Order = 8)]
        [JsonExtensionData]
        public virtual Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}