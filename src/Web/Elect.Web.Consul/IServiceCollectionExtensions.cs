using System;
using Consul;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.Consul.FabioClient;
using Elect.Web.Consul.HostedServices;
using Elect.Web.Consul.Models;
using Elect.Web.HealthCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Elect.Web.Consul
{
    public static class IServiceCollectionExtensions
    {
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
                _.CheckTimeOutInSeconds = configure.CheckTimeOutInSeconds;
                _.CheckInternalInSeconds = configure.CheckInternalInSeconds;
            });
        }

        public static IServiceCollection AddElectConsul(this IServiceCollection services, [NotNull] Action<ElectConsulOptions> configure)
        {
            var electHealthCheckOptions = services.BuildServiceProvider().GetService<IOptions<ElectHealthCheckOptions>>();

            if (electHealthCheckOptions == null)
            {
                throw new NotSupportedException("Consul > Please install and setup Elect.HealthCheck to use the Elect.Consul Service.");
            }
            
            services.Configure(configure);

            var consulOptions = configure.GetValue();

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