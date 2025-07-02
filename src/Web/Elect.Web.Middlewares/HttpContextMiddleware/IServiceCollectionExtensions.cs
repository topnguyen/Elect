namespace Elect.Web.Middlewares.HttpContextMiddleware
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectHttpContext(this IServiceCollection services)
        {
            if (services.All(x => x.ServiceType != typeof(IHttpContextAccessor)))
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }
            return services;
        }
    }
}
