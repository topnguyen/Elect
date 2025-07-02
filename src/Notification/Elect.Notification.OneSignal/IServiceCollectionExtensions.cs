namespace Elect.Notification.OneSignal
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectOneSignal(this IServiceCollection services, [NotNull]ElectOneSignalOptions configuration)
        {
            return services.AddElectOneSignal(_ =>
            {
                _.AuthKey = configuration.AuthKey;
                _.Apps = configuration.Apps;
            });
        }
        public static IServiceCollection AddElectOneSignal(this IServiceCollection services, [NotNull]Action<ElectOneSignalOptions> configuration)
        {
            services.Configure(configuration);
            services.TryAddScoped<IElectOneSignalApp, ElectOneSignalApp>();
            services.TryAddScoped<IElectOneSignalDevice, ElectOneSignalDevice>();
            services.TryAddScoped<IElectOneSignalNotification, ElectOneSignalNotification>();
            services.TryAddScoped<IElectOneSignalClient, ElectOneSignalClient>();
            return services;
        }
    }
}
