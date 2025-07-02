namespace Elect.Web.DataTable
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectDataTable(this IServiceCollection services)
        {
            return services.AddElectDataTable(_ => { });
        }
        public static IServiceCollection AddElectDataTable(this IServiceCollection services, [NotNull] ElectDataTableOptions configure)
        {
            return services.AddElectDataTable(_ =>
            {
                _.DateTimeTimeZone = configure.DateTimeTimeZone;
                _.DateFormat = configure.DateFormat;
                _.DateTimeFormat = configure.DateTimeFormat;
                _.RequestDateTimeFormatType = configure.RequestDateTimeFormatType;
                _.DefaultDisplayText = configure.DefaultDisplayText;
                _.SharedResourceType = configure.SharedResourceType;
            });
        }
        public static IServiceCollection AddElectDataTable(this IServiceCollection services, [NotNull] Action<ElectDataTableOptions> configure)
        {
            services.Configure(configure);
            if (ElectDataTableOptions.Instance == null)
            {
                ElectDataTableOptions.Instance = configure.GetValue();
            }
            // Add DataTable Model Binder
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddDataTableModelBinder();
            });
            return services;
        }
    }
}
