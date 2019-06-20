#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectEsmsConstants.cs </Name>
//         <Created> 20/03/2018 4:19:29 PM </Created>
//         <Key> 1fa24093-513e-4a52-9c71-639fc08c6b0d </Key>
//     </File>
//     <Summary>
//         ElectEsmsConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Flurl.Http.Configuration;
using Newtonsoft.Json;

namespace Elect.Notification.Esms.Models
{
    public class ElectEsmsConstants
    {
        public const string DefaultApiUrl = "https://restapi.esms.vn";

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