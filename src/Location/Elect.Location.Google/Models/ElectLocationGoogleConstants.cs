#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectLocationGoogleConstants.cs </Name>
//         <Created> 20/03/2018 2:36:04 PM </Created>
//         <Key> 9c23ce6f-fe63-470f-873f-3dff1482b222 </Key>
//     </File>
//     <Summary>
//         ElectLocationGoogleConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Flurl.Http.Configuration;
using Newtonsoft.Json;

namespace Elect.Location.Google.Models
{
    public class ElectLocationGoogleConstants
    {
        public const string DefaultGoogleMatrixApiEndpoint = "https://maps.googleapis.com/maps/api/distancematrix/json";
        public const string DefaultGoogleDirectionApiEndpoint = "https://maps.googleapis.com/maps/api/directions/xml";

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