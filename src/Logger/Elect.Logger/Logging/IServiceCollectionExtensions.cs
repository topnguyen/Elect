using System;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Core.ConfigUtils;
using Elect.Logger.Logging.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Logger.Logging
{
    public static class IServiceCollectionExtensions
    {
         public static IServiceCollection AddElectLog(this IServiceCollection services, IConfiguration configuration, string sectionName = "ElectLog")
         {
             var electLogOptions = GetOptions(configuration, sectionName);

             return services.AddElectLog(electLogOptions);
         }

         public static ElectLogOptions GetOptions(IConfiguration configuration,
             string sectionName = "ElectLog")
         {
             var electLogOptions = new ElectLogOptions();

             electLogOptions.Url = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electLogOptions.Url)}");

             electLogOptions.JsonFilePath =
                 configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electLogOptions.JsonFilePath)}");

             electLogOptions.Threshold =
                 configuration.GetValueByEnv<TimeSpan>($"{sectionName}:{nameof(electLogOptions.Threshold)}");

             electLogOptions.AccessKey =
                 configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electLogOptions.AccessKey)}");

             electLogOptions.BatchSize =
                 configuration.GetValueByEnv<uint>($"{sectionName}:{nameof(electLogOptions.BatchSize)}");

             electLogOptions.UnAuthorizeMessage =
                 configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electLogOptions.UnAuthorizeMessage)}");

             electLogOptions.IsEnableLogToFile =
                 configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electLogOptions.IsEnableLogToFile)}");

             electLogOptions.IsEnableLogToConsole =
                 configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electLogOptions.IsEnableLogToConsole)}");

             return electLogOptions;
         }
         
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

            if (!options.IsEnableLogToConsole && !options.IsEnableLogToFile)
            {
                return services;
            }
            
            services.AddSingleton<IElectLog, ElectLog>();

            return services;
        }
    }
}