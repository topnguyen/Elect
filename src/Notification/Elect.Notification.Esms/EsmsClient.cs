#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EsmsClient.cs </Name>
//         <Created> 17/03/2018 9:23:31 AM </Created>
//         <Key> bca98369-5d50-4a87-8b0d-df793698685e </Key>
//     </File>
//     <Summary>
//         EsmsClient.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Notification.Esms.Models;
using Elect.Notification.Esms.Options;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Elect.Notification.Esms
{
    public class EsmsClient
    {
        public ElectEsmsOptions Options { get; }

        public EsmsClient(Action<ElectEsmsOptions> configuration)
        {
            Options = configuration.GetValue();

            FlurlHttp.Configure(config =>
            {
                config.JsonSerializer = new NewtonsoftJsonSerializer(
                    new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Include
                    }
                );
            });
        }

        public async Task<SendSmsResponseModel> SendAsync(SendSmsModel model)
        {
            var url = $"{Options.ApiUri}/MainService.svc/json/SendMultipleMessage_V4_get";

            url = url.SetQueryParam("ApiKey", Options.ApiKey)
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

            var result = await url.GetJsonAsync<SendSmsResponseModel>().ConfigureAwait(true);

            return result;
        }

        public async Task<BalanceModel> GetBalanceAsync()
        {
            var url = $"{Options.ApiUri}/MainService.svc/json/GetBalance/{Options.ApiKey}/{Options.ApiSecret}";

            var result = await url.GetJsonAsync<BalanceModel>().ConfigureAwait(true);

            return result;
        }
    }
}