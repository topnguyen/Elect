#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalDevicesResource.cs </Name>
//         <Created> 19/03/2018 9:22:18 PM </Created>
//         <Key> ea52c2a0-efe6-438f-8ff2-e455e58405b1 </Key>
//     </File>
//     <Summary>
//         ElectOneSignalDevicesResource.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Models.Device;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalDevicesResource : IElectOneSignalDevicesResource
    {
        public ElectOneSignalOptions Options { get; }

        private readonly NewtonsoftJsonSerializer _newtonsoftJsonSerializer = new NewtonsoftJsonSerializer(
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            }
        );

        public ElectOneSignalDevicesResource(IOptions<ElectOneSignalOptions> configuration)
        {
            Options = configuration.Value;
        }

        /// <summary>
        ///     Adds new device into OneSignal App. 
        /// </summary>
        /// <param name="options"> Here you can specify options used to add new device. </param>
        /// <returns> Result of device add operation. </returns>
        public async Task<DeviceAddResult> AddAsync(DeviceAddOptions options)
        {
            var result =
                await Options.ApiUri
                    .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                    .AppendPathSegment("players")
                    .WithHeader("Authorization", $"Basic {Options.ApiKey}")
                    .PostJsonAsync(options)
                    .ReceiveJson<DeviceAddResult>()
                    .ConfigureAwait(true);

            return result;
        }

        /// <summary>
        ///     Edits existing device defined in OneSignal App. 
        /// </summary>
        /// <param name="id">      Id of the device </param>
        /// <param name="options"> Options used to modify attributes of the device. </param>
        /// <exception cref="Exception"></exception>
        public async Task EditAsync(string id, DeviceEditOptions options)
        {
            var result =
                await Options.ApiUri
                    .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                    .AppendPathSegment($"players/{id}")
                    .WithHeader("Authorization", $"Basic {Options.ApiKey}")
                    .PutJsonAsync(options)
                    .ConfigureAwait(true);
        }

        public async Task<DeviceInfo> GetAsync(string playerId, string appId)
        {
            try
            {
                var result =
                    await Options.ApiUri
                        .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                        .AppendPathSegment($"players/{playerId}")
                        .SetQueryParam("app_id", appId)
                        .WithHeader("Authorization", $"Basic {Options.ApiKey}")
                        .GetJsonAsync<DeviceInfo>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}