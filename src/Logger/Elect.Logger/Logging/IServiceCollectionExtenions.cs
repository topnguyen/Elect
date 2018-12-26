using System;
using Elect.Core.Attributes;
using Elect.Logger.Logging.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Logger.Logging
{
    public static class IServiceCollectionExtenions
    {
        public static IServiceCollection AddElectLog(this IServiceCollection services)
        {
            return services.AddElectLog(_ => { });
        }

        public static IServiceCollection AddElectLog(this IServiceCollection services,
            [NotNull] ElectLogOptions configure)
        {
            return services.AddElectLog(_ =>
            {
                _.Url = configure.Url;
                _.JsonFilePath = configure.JsonFilePath;
                _.Threshold = configure.Threshold;
                _.AccessKey = configure.AccessKey;
                _.BatchSize = configure.BatchSize;
                _.UnAuthorizeMessage = configure.UnAuthorizeMessage;
                _.IsEnableLogToFile = configure.IsEnableLogToFile;
                _.IsEnableLogToConsole = configure.IsEnableLogToConsole;
            });
        }

        public static IServiceCollection AddElectLog(this IServiceCollection services,
            [NotNull] Action<ElectLogOptions> configure)
        {
            services.Configure(configure);

            services.AddSingleton<IElectLog, ElectLog>();

            return services;
        }
    }
}