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
