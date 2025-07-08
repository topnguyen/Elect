namespace Elect.Job.Hangfire
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectHangfire(this IServiceCollection services, IConfiguration configuration, string sectionName = "ElectHangfire")
        {
            var electHangfireOptions = GetOptions(configuration, sectionName);
            return services.AddElectHangfire(electHangfireOptions);
        }
        public static ElectHangfireOptions GetOptions(IConfiguration configuration, string sectionName = "ElectHangfire")
        {
            var electHangfireOptions = new ElectHangfireOptions();
            electHangfireOptions.IsEnable = configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electHangfireOptions.IsEnable)}");
            electHangfireOptions.Url = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.Url)}");
            electHangfireOptions.AccessKey = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.AccessKey)}");
            electHangfireOptions.BackToUrl = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.BackToUrl)}");
            electHangfireOptions.DbConnectionString = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.DbConnectionString)}");
            electHangfireOptions.IsDisableJobDashboard = configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electHangfireOptions.IsDisableJobDashboard)}");
            electHangfireOptions.Provider = configuration.GetValueByEnv<HangfireProvider>($"{sectionName}:{nameof(electHangfireOptions.Provider)}");
            electHangfireOptions.StatsPollingInterval = configuration.GetValueByEnv<int>($"{sectionName}:{nameof(electHangfireOptions.StatsPollingInterval)}");
            electHangfireOptions.UnAuthorizeMessage = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.UnAuthorizeMessage)}");
            return electHangfireOptions;
        }
        public static IServiceCollection AddElectHangfire(this IServiceCollection services)
        {
            return services.AddElectHangfire(_ => { });
        }
        public static IServiceCollection AddElectHangfire(this IServiceCollection services, [Core.Attributes.NotNull] ElectHangfireOptions options)
        {
            return services.AddElectHangfire(_ =>
            {
                _.IsEnable = options.IsEnable;
                _.Url = options.Url;
                _.AccessKey = options.AccessKey;
                _.BackToUrl = options.BackToUrl;
                _.DbConnectionString = options.DbConnectionString;
                _.IsDisableJobDashboard = options.IsDisableJobDashboard;
                _.Provider = options.Provider;
                _.StatsPollingInterval = options.StatsPollingInterval;
                _.UnAuthorizeMessage = options.UnAuthorizeMessage;
                _.ExtendOptions = options.ExtendOptions;
            });
        }
        public static IServiceCollection AddElectHangfire(this IServiceCollection services, [Core.Attributes.NotNull] Action<ElectHangfireOptions> configuration)
        {
            services.Configure(configuration);
            var options = configuration.GetValue();
            if (!options.IsEnable)
            {
                return services;
            }
            switch (options.Provider)
            {
                // Registers and configures Hangfire services and storage (e.g., memory, SQL Server) in the DI container. It sets up how jobs are stored and managed.
                case HangfireProvider.Memory:
                    {
                        services.AddHangfire(config =>
                        {
                            config.UseMemoryStorage();
                            options.ExtendOptions?.Invoke(config, options);
                        });
                        GlobalConfiguration.Configuration.UseMemoryStorage();
                        options.ExtendOptions?.Invoke(GlobalConfiguration.Configuration, options);
                        break;
                    }
                case HangfireProvider.SqlServer:
                    {
                        services.AddHangfire(config =>
                        {
                            config.UseSqlServerStorage(options.DbConnectionString);
                            options.ExtendOptions?.Invoke(config, options);
                        });
                        GlobalConfiguration.Configuration.UseSqlServerStorage(options.DbConnectionString);
                        options.ExtendOptions?.Invoke(GlobalConfiguration.Configuration, options);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            // Add Hangfire Server
            // Registers the background processing server that executes jobs. Without this, jobs will not be processed, even if Hangfire is configured.
            services.AddHangfireServer();
            return services;
        }
    }
}
