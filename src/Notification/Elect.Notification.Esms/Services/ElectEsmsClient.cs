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
                .SetQueryParam("SmsType", model.Type)
                .SetQueryParam("Sandbox", model.Sandbox);
            if (!string.IsNullOrWhiteSpace(model.BrandName))
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
