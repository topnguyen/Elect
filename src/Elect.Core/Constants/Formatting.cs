#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> Formatting.cs </Name>
//         <Created> 15/03/2018 5:03:15 PM </Created>
//         <Key> dab7a521-ee23-4444-8393-3da854959c0a </Key>
//     </File>
//     <Summary>
//         Formatting.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Elect.Core.Constants
{
    public static class Formatting
    {
        /// <summary>
        ///     Isolate Datetime Format 
        /// </summary>
        public const string DateTimeOffSetFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateFormatString = DateTimeOffSetFormat,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
    }
}