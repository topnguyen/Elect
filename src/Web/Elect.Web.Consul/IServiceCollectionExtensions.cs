using System;
using System.Collections.Generic;
using Consul;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Core.ConfigUtils;
using Elect.Web.Consul.FabioClient;
using Elect.Web.Consul.HostedServices;
using Elect.Web.Consul.Models;
using Elect.Web.HealthCheck.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Elect.Web.Consul
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectConsul(this IServiceCollection services, IConfiguration configuration,
            string sectionName = "ElectConsul")
        {
            var electConsulOptions = new ElectConsulOptions();

            electConsulOptions.IsEnable =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electConsulOptions.IsEnable)}");

            electConsulOptions.ConsulEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.ConsulEndpoint)}");

            electConsulOptions.ConsulAccessToken =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.ConsulAccessToken)}");

            electConsulOptions.ServiceEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.ServiceEndpoint)}");

            electConsulOptions.ServiceId =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.ServiceId)}");

            electConsulOptions.ServiceName =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.ServiceName)}");

            electConsulOptions.Tags =
                configuration.GetListValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.Tags)}");

            electConsulOptions.CheckTimeOut =
                configuration.GetValueByEnv<TimeSpan>($"{sectionName}:{nameof(electConsulOptions.CheckTimeOut)}");

            electConsulOptions.CheckInternal =
                configuration.GetValueByEnv<TimeSpan>($"{sectionName}:{nameof(electConsulOptions.CheckInternal)}");

            electConsulOptions.DeregisterDeadServiceAfter =
                configuration.GetValueByEnv<TimeSpan>(
                    $"{sectionName}:{nameof(electConsulOptions.DeregisterDeadServiceAfter)}");

            electConsulOptions.IsFabioEnable =
                configuration.GetValueByEnv<bool>($"{sectionName}:{nameof(electConsulOptions.IsFabioEnable)}");

            electConsulOptions.FabioEndpoint =
                configuration.GetValueByEnv<string>($"{sectionName}:{nameof(electConsulOptions.FabioEndpoint)}");

            return services.AddElectConsul(electConsulOptions);
        }
        
        public static IServiceCollection AddElectConsul(this IServiceCollection services)
        {
            return services.AddElectConsul(_ => { });
        }

        public static IServiceCollection AddElectConsul(this IServiceCollection services, [NotNull] ElectConsulOptions configure)
        {
            return services.AddElectConsul(_ =>
            {
                _.IsEnable = configure.IsEnable;
                _.ConsulEndpoint = configure.ConsulEndpoint;
                _.ConsulAccessToken = configure.ConsulAccessToken;
                _.ServiceEndpoint = configure.ServiceEndpoint;
                _.ServiceId = configure.ServiceId;
                _.ServiceName = configure.ServiceName;
                _.Tags = configure.Tags;
                _.CheckTimeOut = configure.CheckTimeOut;
                _.CheckInternal = configure.CheckInternal;
                _.DeregisterDeadServiceAfter = configure.DeregisterDeadServiceAfter;
                _.IsFabioEnable= configure.IsFabioEnable;
                _.FabioEndpoint = configure.FabioEndpoint;
            });
        }

        public static IServiceCollection AddElectConsul(this IServiceCollection services, [NotNull] Action<ElectConsulOptions> configure)
        {
            var electHealthCheckOptions = services.BuildServiceProvider().GetService<IOptions<ElectHealthCheckOptions>>();

            if (electHealthCheckOptions == null)
            {
                throw new NotSupportedException("Consul > Please install and setup Elect.HealthCheck to use the Elect.Consul Service.");
            }
            
            var consulOptions = configure.GetValue();

            // Fabio

            if (consulOptions.IsFabioEnable)
            {
                var fabioTag = $"urlprefix-/{consulOptions.ServiceName} strip=/{consulOptions.ServiceName}";

                if (!consulOptions.Tags.Contains(fabioTag))
                {
                    consulOptions.Tags.Add(fabioTag);
                }
            }

            services.Configure(configure);

            if (!consulOptions.IsEnable)
            {
                return services;
            }

            services.AddSingleton<IConsulClient, ConsulClient>(_ =>
                new ConsulClient(config =>
                {
                    var address = consulOptions.ConsulEndpoint;

                    config.Address = new Uri(address);
                    config.Token = consulOptions.ConsulAccessToken;
                }));

            services.AddSingleton<IHostedService, ConsulHostedService>();
            
            services.AddScoped<IElectFabioClient, ElectFabioClient>();

            return services;
        }
    }
}