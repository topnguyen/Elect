using System;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Web.HealthCheck.HealthChecks;
using Elect.Web.HealthCheck.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Web.HealthCheck
{
    public static class IServiceCollectionExtensions
    {
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