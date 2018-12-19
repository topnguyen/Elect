#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalApp.cs </Name>
//         <Created> 19/03/2018 9:22:18 PM </Created>
//         <Key> ea52c2a0-efe6-438f-8ff2-e455e58405b2 </Key>
//     </File>
//     <Summary>
//         ElectOneSignalApp.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Elect.Notification.OneSignal.Models.App;
using Flurl.Http;

namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalApp : IElectOneSignalApp
    {
        public ElectOneSignalOptions Options { get; }

        public ElectOneSignalApp([NotNull] ElectOneSignalOptions configuration)
        {
            Options = configuration;
        }

        public ElectOneSignalApp([NotNull] Action<ElectOneSignalOptions> configuration) : this(configuration.GetValue())
        {
        }

        public ElectOneSignalApp([NotNull] IOptions<ElectOneSignalOptions> configuration) : this(configuration.Value)
        {
        }

        public async Task<AppInfoModel> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await ElectOneSignalConstants.DefaultApiUrl
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                    })
                    .AppendPathSegments("apps", id)
                    .WithHeader("Authorization", $"Basic {Options.AuthKey}")
                    .GetJsonAsync<AppInfoModel>(cancellationToken)
                    .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        public async Task<List<AppInfoModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await ElectOneSignalConstants.DefaultApiUrl
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                    })
                    .AppendPathSegment("apps")
                    .WithHeader("Authorization", $"Basic {Options.AuthKey}")
                    .GetJsonAsync<List<AppInfoModel>>(cancellationToken)
                    .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        public async Task<AppInfoModel> CreateAsync(AppAddModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await ElectOneSignalConstants.DefaultApiUrl
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                    })
                    .AppendPathSegment("apps")
                    .WithHeader("Authorization", $"Basic {Options.AuthKey}")
                    .PostJsonAsync(model, cancellationToken)
                    .ReceiveJson<AppInfoModel>()
                    .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        public async Task<AppInfoModel> EditAsync(string id, AppEditModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await ElectOneSignalConstants.DefaultApiUrl
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                    })
                    .AppendPathSegments("apps", id)
                    .WithHeader("Authorization", $"Basic {Options.AuthKey}")
                    .PostJsonAsync(model, cancellationToken)
                    .ReceiveJson<AppInfoModel>()
                    .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }
    }
}