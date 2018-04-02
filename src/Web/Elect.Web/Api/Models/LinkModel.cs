#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> LinkModel.cs </Name>
//         <Created> 02/04/2018 1:25:54 AM </Created>
//         <Key> 58efcc21-9e03-4a4b-be0d-8d7ff22c5ac2 </Key>
//     </File>
//     <Summary>
//         LinkModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.Models;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Elect.Web.Api.Models
{
    public class LinkModel
    {
        public string Href { get; set; }

        /// <summary>
        ///     Http method, default is "GET". 
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethod Method { get; set; } = HttpMethod.GET;

        public RouteValueDictionary Values { get; set; } = new RouteValueDictionary();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonExtensionData]
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    }
}