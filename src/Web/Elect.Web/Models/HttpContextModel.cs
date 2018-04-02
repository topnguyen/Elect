#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HttpContextModel.cs </Name>
//         <Created> 02/04/2018 1:04:10 AM </Created>
//         <Key> f2da2be6-86e1-4dd3-8ce9-be6a469db3f0 </Key>
//     </File>
//     <Summary>
//         HttpContextModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.HttpUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Web.Models
{
    [Serializable]
    public sealed class HttpContextModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public Dictionary<string, List<string>> Headers { get; } = new Dictionary<string, List<string>>();

        public string DisplayUrl { get; }

        public string Protocol { get; }

        public string Method { get; }

        public string Endpoint { get; }

        public Dictionary<string, List<string>> QueryStrings { get; } = new Dictionary<string, List<string>>();

        /// <summary>
        ///     Need to <c> EnableRewind </c> for Request in middleware to get Request Body. 
        /// </summary>
        public object RequestBody { get; }

        public HttpContextModel(HttpContext context)
        {
            if (context == null)
            {
                return;
            }

            Headers = context.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToList());

            DisplayUrl = context.Request.GetDisplayUrl();

            Protocol = context.Request.Protocol;

            Method = context.Request.Method;

            QueryStrings = context.Request.Query.ToDictionary(x => x.Key, x => x.Value.ToList());

            Endpoint = context.Request.GetEndpoint();

            RequestBody = context.Request.GetBody();
        }
    }
}