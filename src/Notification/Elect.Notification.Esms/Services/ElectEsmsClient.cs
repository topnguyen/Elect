#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectEsmsClient.cs </Name>
//         <Created> 17/03/2018 9:23:31 AM </Created>
//         <Key> bca98369-5d50-4a87-8b0d-df793698685e </Key>
//     </File>
//     <Summary>
//         ElectEsmsClient.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Notification.Esms.Interfaces;
using Elect.Notification.Esms.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elect.Notification.Esms.Services
{
    public class ElectEsmsClient : IElectEsmsClient
    {
        public ElectEsmsOptions Options { get; }

        public ElectEsmsClient([NotNull]ElectEsmsOptions configuration)
        {
            Options = configuration;
        }

        public ElectEsmsClient([NotNull]Action<ElectEsmsOptions> configuration) : this(configuration.GetValue())
        {
        }

        public ElectEsmsClient([NotNull]IOptions<ElectEsmsOptions> configuration) : this(configuration.Value)
        {
        }

        public async Task<SendSmsResponseModel> SendAsync([NotNull]SendSmsModel model)
        {
            var url = $"{ElectEsmsConstants.DefaultApiUrl}/MainService.svc/json/SendMultipleMessage_V4_get";

            url = url
                .SetQueryParam("ApiKey", Options.ApiKey)
                .SetQueryParam("SecretKey", Options.ApiSecret)
                .SetQueryParam("Phone", model.Phone)
                .SetQueryParam("Content", model.Content)
                .SetQueryParam("SmsType", (int)model.Type);

            switch (model.Type)
            {
                case EsmsSmsType.Random:
                case EsmsSmsType.Notify:
                case EsmsSmsType.OTP:
                    {
                        url = url.SetQueryParam("IsUnicode", model.IsUnicode ? "1" : "0");
                        break;
                    }
                case EsmsSmsType.Verify:
                    {
                        model.BrandName = "Verify";
                        break;
                    }
            }

            if (!string.IsNullOrWhiteSpace(model.BrandName) && (model.Type == EsmsSmsType.BrandName || model.Type == EsmsSmsType.Verify))
            {
                url = url.SetQueryParam("Brandname", model.BrandName);
            }

            try
            {
                var result = await url
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectEsmsConstants.NewtonsoftJsonSerializer;
                    })
                    .GetJsonAsync<SendSmsResponseModel>()
                    .ConfigureAwait(true);

                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);

                throw new HttpRequestException(response);
            }
        }

        public async Task<BalanceModel> GetBalanceAsync()
        {
            var url = $"{ElectEsmsConstants.DefaultApiUrl}/MainService.svc/json/GetBalance/{Options.ApiKey}/{Options.ApiSecret}";

            try
            {
                var result = await url
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectEsmsConstants.NewtonsoftJsonSerializer;
                    })
                    .GetJsonAsync<BalanceModel>()
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