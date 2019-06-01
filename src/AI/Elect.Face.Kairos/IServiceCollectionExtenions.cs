using System;
using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.Face.Kairos.Interfaces;
using Elect.Face.Kairos.Models;
using Elect.Face.Kairos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Face.Kairos
{
    public static class IServiceCollectionExtenions
    {
        public static IServiceCollection AddElectKairos(this IServiceCollection services, [NotNull] ElectKairosOptions configure)
        {
            return services.AddElectKairos(_ =>
            {
                _.AppId = configure.AppId;
                _.AppKey = configure.AppKey;
                _.DefaultGallery = configure.DefaultGallery;
            });
        }

        public static IServiceCollection AddElectKairos(this IServiceCollection services, [NotNull] Action<ElectKairosOptions> configure)
        {
            services.Configure(configure);

            var electKairosOptions = configure.GetValue();

            if (string.IsNullOrWhiteSpace(electKairosOptions.AppId))
            {
                throw new NotSupportedException($"{nameof(electKairosOptions.AppId)} cannot be empty.");
            }
            
            if (string.IsNullOrWhiteSpace(electKairosOptions.AppKey))
            {
                throw new NotSupportedException($"{nameof(electKairosOptions.AppKey)} cannot be empty.");
            }

            services.AddScoped<IElectKairosClient, ElectKairosClient>();

            return services;
        }

    }
}