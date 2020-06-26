#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 02/04/2018 5:42:42 PM </Created>
//         <Key> ff199602-013b-4fc8-bb36-a2a430b4357c </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ActionUtils;
using Elect.Job.Hangfire.Models;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using System;
using Elect.Core.ConfigUtils;
using Microsoft.Extensions.Configuration;

namespace Elect.Job.Hangfire
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectHangfire(this IServiceCollection services, IConfiguration configuration, string sectionName = "ElectHangfire")
        {
            var electHangfireOptions = new ElectHangfireOptions();
            
            electHangfireOptions.IsEnable = configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electHangfireOptions.IsEnable)}");
            
            electHangfireOptions.Url = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.Url)}");
            
            electHangfireOptions.AccessKey = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.AccessKey)}");
            
            electHangfireOptions.BackToUrl = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.BackToUrl)}");
            
            electHangfireOptions.DbConnectionString = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.DbConnectionString)}");
            
            electHangfireOptions.IsDisableJobDashboard = configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electHangfireOptions.IsDisableJobDashboard)}");
            
            electHangfireOptions.Provider = configuration.GetValueByEnv<HangfireProvider>($"{sectionName}:{nameof(electHangfireOptions.Provider)}");
            
            electHangfireOptions.StatsPollingInterval = configuration.GetValueByEnv<int>($"{sectionName}:{nameof(electHangfireOptions.StatsPollingInterval)}");

            electHangfireOptions.UnAuthorizeMessage = configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electHangfireOptions.UnAuthorizeMessage)}");
            
            return services.AddElectHangfire(electHangfireOptions);
        }
        
        public static IServiceCollection AddElectHangfire(this IServiceCollection services)
        {
            return services.AddElectHangfire(_ => { });
        }

        public static IServiceCollection AddElectHangfire(this IServiceCollection services, [NotNull] ElectHangfireOptions options)
        {
            return services.AddElectHangfire(_ =>
            {
                _.IsEnable = options.IsEnable;
                _.Url = options.Url;
                _.AccessKey = options.AccessKey;
                _.BackToUrl = options.BackToUrl;
                _.DbConnectionString = options.DbConnectionString;
                _.IsDisableJobDashboard = options.IsDisableJobDashboard;
                _.Provider = options.Provider;
                _.StatsPollingInterval = options.StatsPollingInterval;
                _.UnAuthorizeMessage = options.UnAuthorizeMessage;
                _.ExtendOptions = options.ExtendOptions;
            });
        }

        public static IServiceCollection AddElectHangfire(this IServiceCollection services, [NotNull] Action<ElectHangfireOptions> configuration)
        {
            services.Configure(configuration);

            var options = configuration.GetValue();

            if (!options.IsEnable)
            {
                return services;
            }
            
            switch (options.Provider)
            {
                case HangfireProvider.Memory:
                    {
                        services.AddHangfire(config =>
                        {
                            config.UseMemoryStorage();
                            
                            options.ExtendOptions?.Invoke(config, options);
                        });
                        
                        GlobalConfiguration.Configuration.UseMemoryStorage();
                        
                        options.ExtendOptions?.Invoke(GlobalConfiguration.Configuration, options);

                        break;
                    }
                case HangfireProvider.SqlServer:
                    {
                        services.AddHangfire(config =>
                        {
                            config.UseSqlServerStorage(options.DbConnectionString);
                            
                            options.ExtendOptions?.Invoke(config, options);
                        });
                        
                        GlobalConfiguration.Configuration.UseSqlServerStorage(options.DbConnectionString);
                        
                        options.ExtendOptions?.Invoke(GlobalConfiguration.Configuration, options);

                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return services;
        }
    }
}