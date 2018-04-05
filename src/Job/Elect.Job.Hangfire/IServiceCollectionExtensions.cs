#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
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

namespace Elect.Job.Hangfire
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElectHangfire(this IServiceCollection services)
        {
            return services.AddElectHangfire(_ => { });
        }

        public static IServiceCollection AddElectHangfire(this IServiceCollection services, [NotNull] Action<ElectHangfireOptions> configuration)
        {
            services.Configure(configuration);

            var options = configuration.GetValue();

            switch (options.Provider)
            {
                case HangfireProvider.Memory:
                    {
                        services.AddHangfire(config =>
                        {
                            config.UseMemoryStorage();
                        });
                        break;
                    }
                case HangfireProvider.SqlServer:
                    {
                        services.AddHangfire(config =>
                        {
                            config.UseSqlServerStorage(options.HangfireDatabaseConnectionString);
                        });
                        break;
                    }
            }

            return services;
        }
    }
}