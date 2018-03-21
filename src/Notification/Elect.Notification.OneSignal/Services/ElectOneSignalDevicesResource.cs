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

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Models.Device;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalDevicesResource : IElectOneSignalDevicesResource
    {
        public ElectOneSignalOptions Options { get; }

        public ElectOneSignalDevicesResource([NotNull]ElectOneSignalOptions configuration)
        {
            Options = configuration;
        }

        public ElectOneSignalDevicesResource([NotNull]Action<ElectOneSignalOptions> configuration) : this(configuration.GetValue())
        {
        }

        public ElectOneSignalDevicesResource([NotNull]IOptions<ElectOneSignalOptions> configuration) : this(configuration.Value)
        {
        }

        public async Task<DeviceAddResult> AddAsync([NotNull]DeviceAddOptions options, [NotNull]string appName = ElectOneSignalConstants.DefaultAppName)
        {
            var appInfo = Options.Apps.Single(x => x.AppName == appName);

            options.AppId = appInfo.AppId;

            try
            {
                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment("players")
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .PostJsonAsync(options)
                        .ReceiveJson<DeviceAddResult>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                throw new HttpRequestException(e.GetResponseString());
            }
        }

        public async Task EditAsync([NotNull]string id, [NotNull]DeviceEditOptions options, [NotNull]string appName = ElectOneSignalConstants.DefaultAppName)
        {
            var appInfo = Options.Apps.Single(x => x.AppName == appName);

            options.AppId = appInfo.AppId;

            try
            {
                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment($"players/{id}")
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .PutJsonAsync(options)
                        .ConfigureAwait(true);
            }
            catch (FlurlHttpException e)
            {
                throw new HttpRequestException(e.GetResponseString());
            }
        }

        public async Task<DeviceInfo> GetAsync([NotNull]string playerId, [NotNull]string appName = ElectOneSignalConstants.DefaultAppName)
        {
            try
            {
                var appInfo = Options.Apps.Single(x => x.AppName == appName);

                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment($"players/{playerId}")
                        .SetQueryParam("app_id", appInfo.AppId)
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .GetJsonAsync<DeviceInfo>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                throw new HttpRequestException(e.GetResponseString());
            }
        }
    }
}