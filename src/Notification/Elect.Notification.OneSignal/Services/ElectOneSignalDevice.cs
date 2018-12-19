#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalDevice.cs </Name>
//         <Created> 19/03/2018 9:22:18 PM </Created>
//         <Key> ea52c2a0-efe6-438f-8ff2-e455e58405b1 </Key>
//     </File>
//     <Summary>
//         ElectOneSignalDevice.cs is a part of Elect
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
    public class ElectOneSignalDevice : IElectOneSignalDevice
    {
        public ElectOneSignalOptions Options { get; }

        public ElectOneSignalDevice([NotNull] ElectOneSignalOptions configuration)
        {
            Options = configuration;
        }

        public ElectOneSignalDevice([NotNull] Action<ElectOneSignalOptions> configuration) : this(
            configuration.GetValue())
        {
        }

        public ElectOneSignalDevice([NotNull] IOptions<ElectOneSignalOptions> configuration) : this(configuration.Value)
        {
        }

        #region Add

        public async Task<DeviceAddResultModel> AddAsync([NotNull] DeviceAddModel model, [NotNull] string appId)
        {
            var appInfo = Options.Apps.Single(x => x.AppId == appId);

            return await AddAsync(model, appInfo);
        }

        public async Task<DeviceAddResultModel> AddAsync([NotNull] DeviceAddModel model, [NotNull] string appId,
            [NotNull] string appKey)
        {
            var appInfo = new ElectOneSignalAppOption(appId, appKey);

            return await AddAsync(model, appInfo);
        }

        public async Task<DeviceAddResultModel> AddAsync([NotNull] DeviceAddModel model,
            [NotNull] ElectOneSignalAppOption appInfo)
        {
            model.AppId = appInfo.AppId;

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
                        .PostJsonAsync(model)
                        .ReceiveJson<DeviceAddResultModel>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        #endregion

        #region Edit

        public async Task EditAsync([NotNull] string id, [NotNull] DeviceEditModel model, [NotNull] string appId)
        {
            var appInfo = Options.Apps.Single(x => x.AppId == appId);

            await EditAsync(id, model, appInfo);
        }

        public async Task EditAsync([NotNull] string id, [NotNull] DeviceEditModel model, [NotNull] string appId,
            [NotNull] string appKey)
        {
            var appInfo = new ElectOneSignalAppOption(appId, appKey);

            await EditAsync(id, model, appInfo);
        }

        public async Task EditAsync([NotNull] string id, [NotNull] DeviceEditModel model,
            [NotNull] ElectOneSignalAppOption appInfo)
        {
            model.AppId = appInfo.AppId;

            try
            {
                await ElectOneSignalConstants.DefaultApiUrl
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                    })
                    .AppendPathSegment($"players/{id}")
                    .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                    .PutJsonAsync(model)
                    .ConfigureAwait(true);
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        #endregion

        #region Get

        public async Task<DeviceInfoModel> GetAsync([NotNull] string id, [NotNull] string appId)
        {
            var appInfo = Options.Apps.Single(x => x.AppId == appId);

            return await GetAsync(id, appInfo);
        }

        public async Task<DeviceInfoModel> GetAsync([NotNull] string id, [NotNull] string appId,
            [NotNull] string appKey)
        {
            var appInfo = new ElectOneSignalAppOption(appId, appKey);

            return await GetAsync(id, appInfo);
        }

        public async Task<DeviceInfoModel> GetAsync([NotNull] string id, [NotNull] ElectOneSignalAppOption appInfo)
        {
            try
            {
                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment($"players/{id}")
                        .SetQueryParam("app_id", appInfo.AppId)
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .GetJsonAsync<DeviceInfoModel>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        #endregion
    }
}