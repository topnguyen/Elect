#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalConstants.cs </Name>
//         <Created> 20/03/2018 10:03:13 AM </Created>
//         <Key> 0bd9e578-ed39-439e-9140-98824ce59793 </Key>
//     </File>
//     <Summary>
//         ElectOneSignalConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Collections.Generic;
using Flurl.Http.Configuration;
using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models
{
    public class ElectOneSignalConstants
    {
        public List<string> IncludedAllSegments = new List<string>
        {
            "All"
        };

        public const string DefaultAppName = "Default";

        public const string DefaultApiUrl = "https://onesignal.com/api/v1";

        internal static readonly NewtonsoftJsonSerializer NewtonsoftJsonSerializer =
            new NewtonsoftJsonSerializer(
                new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include
                }
            );
    }
}