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

using Elect.Notification.Esms.Interfaces;
using Elect.Notification.Esms.Models;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using NullValueHandling = Newtonsoft.Json.NullValueHandling;

namespace Elect.Notification.Esms.Services
{
    public class ElectEsmsClient : IElectEsmsClient
    {
        public ElectEsmsOptions Options { get; }

        private readonly NewtonsoftJsonSerializer _newtonsoftJsonSerializer = new NewtonsoftJsonSerializer(
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            }
        );

        public ElectEsmsClient(IOptions<ElectEsmsOptions> configuration)
        {
            Options = configuration.Value;
        }

        public async Task<SendSmsResponseModel> SendAsync(SendSmsModel model)
        {
            var url = $"{Options.ApiUri}/MainService.svc/json/SendMultipleMessage_V4_get";

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

            var result = await url
                .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                .GetJsonAsync<SendSmsResponseModel>()
                .ConfigureAwait(true);

            return result;
        }

        public async Task<BalanceModel> GetBalanceAsync()
        {
            var url = $"{Options.ApiUri}/MainService.svc/json/GetBalance/{Options.ApiKey}/{Options.ApiSecret}";

            var result = await url
                .ConfigureRequest(config => { config.JsonSerializer = _newtonsoftJsonSerializer; })
                .GetJsonAsync<BalanceModel>()
                .ConfigureAwait(true);

            return result;
        }
    }
}