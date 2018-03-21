#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalNotificationsResource.cs </Name>
//         <Created> 19/03/2018 9:49:08 PM </Created>
//         <Key> b76023b6-a7f1-4634-ae7d-39276be1c13d </Key>
//     </File>
//     <Summary>
//         ElectOneSignalNotificationsResource.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Models.Notifications;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalNotificationsResource : IElectOneSignalNotificationsResource
    {
        public ElectOneSignalOptions Options { get; }

        public ElectOneSignalNotificationsResource([NotNull]ElectOneSignalOptions configuration)
        {
            Options = configuration;
        }

        public ElectOneSignalNotificationsResource([NotNull]Action<ElectOneSignalOptions> configuration) : this(configuration.GetValue())
        {
        }

        public ElectOneSignalNotificationsResource([NotNull]IOptions<ElectOneSignalOptions> configuration) : this(configuration.Value)
        {
        }

        public async Task<NotificationCreateResult> CreateAsync([NotNull]NotificationCreateOptions options, [NotNull]string appName = ElectOneSignalConstants.DefaultAppName)
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
                        .AppendPathSegment("notifications")
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .PostJsonAsync(options)
                        .ReceiveJson<NotificationCreateResult>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                throw new HttpRequestException(e.GetResponseString());
            }
        }

        public async Task<NotificationViewResult> GetAsync([NotNull]string id, [NotNull]string appName = ElectOneSignalConstants.DefaultAppName)
        {
            var appInfo = Options.Apps.Single(x => x.AppName == appName);

            try
            {
                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment($"notifications/{id}?app_id={appInfo.AppId}")
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .GetAsync()
                        .ReceiveJson<NotificationViewResult>()
                        .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                throw new HttpRequestException(e.GetResponseString());
            }
        }

        public async Task<NotificationCancelResult> CancelAsync([NotNull]string id, [NotNull]string appName = ElectOneSignalConstants.DefaultAppName)
        {
            var appInfo = Options.Apps.Single(x => x.AppName == appName);

            try
            {
                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment($"notifications/{id}?app_id={appInfo.AppId}")
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .DeleteAsync()
                        .ReceiveJson<NotificationCancelResult>()
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