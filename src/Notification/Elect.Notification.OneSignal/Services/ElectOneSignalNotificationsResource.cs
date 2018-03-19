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

using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Models.Notifications;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalNotificationsResource : IElectOneSignalNotificationsResource
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

        public ElectOneSignalNotificationsResource(IOptions<ElectOneSignalOptions> configuration)
        {
            Options = configuration.Value;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Creates new notification to be sent by OneSignal system. 
        /// </summary>
        /// <param name="options"> Options used for notification create operation. </param>
        /// <returns></returns>
        public async Task<NotificationCreateResult> CreateAsync(NotificationCreateOptions options)
        {
            var result =
                await Options.ApiUri
                    .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                    .AppendPathSegment("notifications")
                    .WithHeader("Authorization", $"Basic {Options.ApiKey}")
                    .PostJsonAsync(options)
                    .ReceiveJson<NotificationCreateResult>()
                    .ConfigureAwait(true);

            return result;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Get delivery and convert report about single notification. 
        /// </summary>
        /// <param name="options">
        ///     Options used for getting delivery and convert report about single notification.
        /// </param>
        /// <returns></returns>
        public async Task<NotificationViewResult> ViewAsync(NotificationViewOptions options)
        {
            var result =
                await Options.ApiUri
                    .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                    .AppendPathSegment($"notifications/{options.Id}?app_id={options.AppId}")
                    .WithHeader("Authorization", $"Basic {Options.ApiKey}")
                    .GetAsync()
                    .ReceiveJson<NotificationViewResult>()
                    .ConfigureAwait(true);

            return result;
        }

        /// <summary>
        ///     Cancel a notification scheduled by the OneSignal system 
        /// </summary>
        /// <param name="options"> Options used for notification cancel operation. </param>
        /// <returns></returns>
        public async Task<NotificationCancelResult> CancelAsync(NotificationCancelOptions options)
        {
            var result =
                await Options.ApiUri
                    .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                    .AppendPathSegment($"notifications/{options.Id}?app_id={options.AppId}")
                    .WithHeader("Authorization", $"Basic {Options.ApiKey}")
                    .DeleteAsync()
                    .ReceiveJson<NotificationCancelResult>()
                    .ConfigureAwait(true);

            return result;
        }
    }
}