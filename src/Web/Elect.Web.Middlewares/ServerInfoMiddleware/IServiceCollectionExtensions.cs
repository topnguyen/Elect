namespace Elect.Web.Middlewares.ServerInfoMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectServerInfo(this IServiceCollection services)
        {
            return services.AddElectServerInfo(_ => { });
        }
        public static IServiceCollection AddElectServerInfo(this IServiceCollection services, [NotNull] ElectServerInfoOptions configuration)
        {
            return services.AddElectServerInfo(_ =>
            {
                _.ServerName = configuration.ServerName;
                _.PoweredBy = configuration.PoweredBy;
                _.AuthorName = configuration.AuthorName;
                _.AuthorWebsite = configuration.AuthorWebsite;
                _.AuthorEmail = configuration.AuthorEmail;
            });
        }
        public static IServiceCollection AddElectServerInfo(this IServiceCollection services, [NotNull]Action<ElectServerInfoOptions> configuration)
        {
            services.Configure(configuration);
            return services;
        }
    }
}
