namespace Elect.Notification.Esms
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectNotificationEsms(this IServiceCollection services, [NotNull]ElectEsmsOptions configuration)
        {
            return services.AddElectNotificationEsms(_ =>
            {
                _.ApiKey = configuration.ApiKey;
                _.ApiSecret = configuration.ApiSecret;
            });
        }
        public static IServiceCollection AddElectNotificationEsms(this IServiceCollection services, [NotNull]Action<ElectEsmsOptions> configuration)
        {
            services.Configure(configuration);
            services.TryAddScoped<IElectEsmsClient, ElectEsmsClient>();
            return services;
        }
    }
}
