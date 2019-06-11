using System;
using Elect.Core.ActionUtils;
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

        public static IServiceCollection AddElectLog(this IServiceCollection services, [NotNull] ElectLogOptions configuration)
        {
            return services.AddElectLog(_ =>
            {
                _.Url = configuration.Url;
                _.JsonFilePath = configuration.JsonFilePath;
                _.Threshold = configuration.Threshold;
                _.AccessKey = configuration.AccessKey;
                _.BatchSize = configuration.BatchSize;
                _.UnAuthorizeMessage = configuration.UnAuthorizeMessage;
                _.IsEnableLogToFile = configuration.IsEnableLogToFile;
                _.IsEnableLogToConsole = configuration.IsEnableLogToConsole;
            });
        }

        public static IServiceCollection AddElectLog(this IServiceCollection services, [NotNull] Action<ElectLogOptions> configuration)
        {
            services.Configure(configuration);

            var options = configuration.GetValue();

            if (!options.IsEnableLogToFile && !options.IsEnableLogToFile)
            {
                return services;
            }
            
            services.AddSingleton<IElectLog, ElectLog>();

            return services;
        }
    }
}