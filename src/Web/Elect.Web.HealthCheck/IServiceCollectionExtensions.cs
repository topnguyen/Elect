namespace Elect.Web.HealthCheck
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectHealthCheck(this IServiceCollection services, IConfiguration configuration, string sectionName = "ElectHealthCheck")
        {
            var electHealthCheckOptions = GetOptions(configuration, sectionName);
            return services.AddElectHealthCheck(electHealthCheckOptions);
        }
        public static ElectHealthCheckOptions GetOptions(IConfiguration configuration, string sectionName = "ElectHealthCheck")
        {
            var electHealthCheckOptions = new ElectHealthCheckOptions();
            electHealthCheckOptions.IsEnable = configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electHealthCheckOptions.IsEnable)}");
            electHealthCheckOptions.Endpoint = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHealthCheckOptions.Endpoint)}");
            electHealthCheckOptions.DbConnectionString = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHealthCheckOptions.DbConnectionString)}");
            return electHealthCheckOptions;
        }
        public static IServiceCollection AddElectHealthCheck(this IServiceCollection services)
        {
            return services.AddElectHealthCheck(_ => { });
        }
        public static IServiceCollection AddElectHealthCheck(this IServiceCollection services, [NotNull] ElectHealthCheckOptions configure)
        {
            return services.AddElectHealthCheck(_ =>
            {
                _.IsEnable = configure.IsEnable;
                _.Endpoint = configure.Endpoint;
                _.DbConnectionString = configure.DbConnectionString;
                _.Builder = configure.Builder;
                _.Options = configure.Options;
            });
        }
        public static IServiceCollection AddElectHealthCheck(this IServiceCollection services, [NotNull] Action<ElectHealthCheckOptions> configure)
        {
            services.Configure(configure);
            var option = configure.GetValue();
            if (!option.IsEnable)
            {
                return services;
            }
            IHealthChecksBuilder builder;
            if (string.IsNullOrWhiteSpace(option.DbConnectionString))
            {
                builder = services.AddHealthChecks();
            }
            else
            {
                builder = services
                    .AddHealthChecks()
                    .AddCheck("database", new DbConnectionHealthCheck(option.DbConnectionString));
            }
            option.Builder?.Invoke(builder);
            return services;
        }
    }
}
