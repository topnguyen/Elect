namespace Elect.Location.Google
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectLocationGoogle(this IServiceCollection services)
        {
            return services.AddElectLocationGoogle(_ => { });
        }
        public static IServiceCollection AddElectLocationGoogle(this IServiceCollection services, [NotNull]ElectLocationGoogleOptions configuration)
        {
            return services.AddElectLocationGoogle(_ =>
            {
                _.GoogleApiKey = configuration.GoogleApiKey;
            });
        }
        public static IServiceCollection AddElectLocationGoogle(this IServiceCollection services, [NotNull] Action<ElectLocationGoogleOptions> configuration)
        {
            services.Configure(configuration);
            services.TryAddScoped<IElectGoogleClient, ElectGoogleClient>();
            return services;
        }
    }
}
