namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalNotification : IElectOneSignalNotification
    {
        public ElectOneSignalOptions Options { get; }
        public ElectOneSignalNotification([NotNull] ElectOneSignalOptions configuration)
        {
            Options = configuration;
        }
        public ElectOneSignalNotification([NotNull] Action<ElectOneSignalOptions> configuration) : this(configuration.GetValue())
        {
        }
        public ElectOneSignalNotification([NotNull] IOptions<ElectOneSignalOptions> configuration) : this(configuration.Value)
        {
        }
        #region Create
        public async Task<NotificationCreateResultModel> CreateAsync([NotNull] NotificationCreateModel model, [NotNull] string appId)
        {
            var appInfo = Options.Apps.Single(x => x.AppId == appId);
            return await CreateAsync(model, appInfo);
        }
        public async Task<NotificationCreateResultModel> CreateAsync([NotNull] NotificationCreateModel model, [NotNull] string appId, [NotNull] string appKey)
        {
            var appInfo = new ElectOneSignalAppOption(appId, appKey);
            return await CreateAsync(model, appInfo);
        }
        public async Task<NotificationCreateResultModel> CreateAsync([NotNull] NotificationCreateModel model, [NotNull] ElectOneSignalAppOption appInfo)
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
                        .AppendPathSegment("notifications")
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .PostJsonAsync(model)
                        .ReceiveJson<NotificationCreateResultModel>()
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
        #region Cancel
        public async Task<NotificationCancelResultModel> CancelAsync([NotNull] string id, [NotNull] string appId)
        {
            var appInfo = Options.Apps.Single(x => x.AppId == appId);
            return await CancelAsync(id, appInfo);
        }
        public async Task<NotificationCancelResultModel> CancelAsync([NotNull] string id, [NotNull] string appId, [NotNull] string appKey)
        {
            var appInfo = new ElectOneSignalAppOption(appId, appKey);
            return await CancelAsync(id, appInfo);
        }
        public async Task<NotificationCancelResultModel> CancelAsync([NotNull] string id, [NotNull] ElectOneSignalAppOption appInfo)
        {
            try
            {
                var result =
                    await ElectOneSignalConstants.DefaultApiUrl
                        .ConfigureRequest(config =>
                        {
                            config.JsonSerializer = ElectOneSignalConstants.NewtonsoftJsonSerializer;
                        })
                        .AppendPathSegment($"notifications/{id}")
                        .SetQueryParam("app_id", appInfo.AppId)
                        .WithHeader("Authorization", $"Basic {appInfo.ApiKey}")
                        .DeleteAsync()
                        .ReceiveJson<NotificationCancelResultModel>()
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
        #region Get
        public async Task<NotificationViewResultModel> GetAsync([NotNull] string id, [NotNull] string appId)
        {
            var appInfo = Options.Apps.Single(x => x.AppId == appId);
            return await GetAsync(id, appInfo);
        }
        public async Task<NotificationViewResultModel> GetAsync([NotNull] string id, [NotNull] string appId,
            [NotNull] string appKey)
        {
            var appInfo = new ElectOneSignalAppOption(appId, appKey);
            return await GetAsync(id, appInfo);
        }
        public async Task<NotificationViewResultModel> GetAsync([NotNull] string id,
            [NotNull] ElectOneSignalAppOption appInfo)
        {
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
                        .ReceiveJson<NotificationViewResultModel>()
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
