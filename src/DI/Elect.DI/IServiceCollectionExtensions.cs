#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 15/03/2018 8:52:01 PM </Created>
//         <Key> 8dd973ac-3c2c-4af1-b71e-a2e706ee1a99 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.DI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection Removes(this IServiceCollection services, params Type[] serviceTypes)
        {
            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = services.FirstOrDefault(x => x.ServiceType == serviceType);

                if (serviceDescriptor != null)
                {
                    services.Remove(serviceDescriptor);
                }
            }

            return services;
        }

        public static bool IsRegistered<TService>(this IServiceCollection services) where TService : class
        {
            bool isExist = services.All(x => x.ServiceType == typeof(TService));

            return isExist;
        }

        #region Add If Any

        public static IServiceCollection AddScopedIfAny<TService, TImplementation>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class where TImplementation : class, TService
        {
            if (services.Any(predicate))
            {
                services.AddScoped<TService, TImplementation>();
            }
            return services;
        }

        public static IServiceCollection AddScopedIfAny<TService>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.Any(predicate))
            {
                services.AddScoped<TService>();
            }
            return services;
        }

        public static IServiceCollection AddTransientIfAny<TService, TImplementation>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class where TImplementation : class, TService
        {
            if (services.Any(predicate))
            {
                services.AddTransient<TService, TImplementation>();
            }
            return services;
        }

        public static IServiceCollection AddTransientIfAny<TService>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.Any(predicate))
            {
                services.AddTransient<TService>();
            }
            return services;
        }

        public static IServiceCollection AddSingletonIfAny<TService, TImplementation>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class where TImplementation : class, TService
        {
            if (services.Any(predicate))
            {
                services.AddSingleton<TService, TImplementation>();
            }
            return services;
        }

        public static IServiceCollection AddSingletonIfAny<TService>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.Any(predicate))
            {
                services.AddSingleton<TService>();
            }
            return services;
        }

        #endregion

        #region Add If All

        public static IServiceCollection AddScopedIfAll<TService, TImplementation>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class where TImplementation : class, TService
        {
            if (services.All(predicate))
            {
                services.AddScoped<TService, TImplementation>();
            }
            return services;
        }

        public static IServiceCollection AddScopedIfAll<TService>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.All(predicate))
            {
                services.AddScoped<TService>();
            }
            return services;
        }

        public static IServiceCollection AddTransientIfAll<TService, TImplementation>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class where TImplementation : class, TService
        {
            if (services.All(predicate))
            {
                services.AddTransient<TService, TImplementation>();
            }
            return services;
        }

        public static IServiceCollection AddTransientIfAll<TService>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.All(predicate))
            {
                services.AddTransient<TService>();
            }
            return services;
        }

        public static IServiceCollection AddSingletonIfAll<TService, TImplementation>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class where TImplementation : class, TService
        {
            if (services.All(predicate))
            {
                services.AddSingleton<TService, TImplementation>();
            }
            return services;
        }

        public static IServiceCollection AddSingletonIfAll<TService>(this IServiceCollection services, Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.All(predicate))
            {
                services.AddSingleton<TService>();
            }
            return services;
        }

        #endregion

        #region Add If Not Exist

        public static IServiceCollection AddScopedIfNotExist<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            services.AddScopedIfAll<TService, TImplementation>(x => x.ServiceType != typeof(TService));
            return services;
        }

        public static IServiceCollection AddScopedIfNotExist<TService>(this IServiceCollection services) where TService : class
        {
            services.AddScopedIfAll<TService>(x => x.ServiceType != typeof(TService));
            return services;
        }

        public static IServiceCollection AddTransientIfNotExist<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            services.AddTransientIfAll<TService, TImplementation>(x => x.ServiceType != typeof(TService));
            return services;
        }

        public static IServiceCollection AddTransientIfNotExist<TService>(this IServiceCollection services) where TService : class
        {
            services.AddTransientIfAll<TService>(x => x.ServiceType != typeof(TService));
            return services;
        }

        public static IServiceCollection AddSingletonIfNotExist<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            services.AddSingletonIfAll<TService, TImplementation>(x => x.ServiceType != typeof(TService));
            return services;
        }

        public static IServiceCollection AddSingletonIfNotExist<TService>(this IServiceCollection services) where TService : class
        {
            services.AddSingletonIfAll<TService>(x => x.ServiceType != typeof(TService));
            return services;
        }

        #endregion

        #region Scanner

        /// <summary>
        ///     Auto scan and register implementation of service follow
        ///     <see cref="Elect.DI.Attributes" /> life time setup.
        /// </summary>
        /// <param name="services">          </param>
        /// <param name="assemblyName">      
        ///     System assembly name or dll file name, default is use application name
        /// </param>
        /// <param name="assemblyFolderPath"> Assembly folder path of all system dll </param>
        /// <returns></returns>
        public static IServiceCollection AddElectDI(this IServiceCollection services, string assemblyName = null, string assemblyFolderPath = null)
        {
            var scanner = new Scanner();

            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                assemblyName = PlatformServices.Default.Application.ApplicationName;
            }

            scanner.RegisterAssemblies(services, $"{assemblyName}.dll", assemblyFolderPath);

            scanner.RegisterAssemblies(services, $"{assemblyName}.*.dll", assemblyFolderPath);

            services.AddSingleton(scanner);

            return services;
        }

        #endregion

        #region Print

        public static IServiceCollection PrintRegisteredToConsole(this IServiceCollection services, string assemblyName = null)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                assemblyName = PlatformServices.Default.Application.ApplicationName;
            }

            var listServiceDescriptors = services.ToList();

            listServiceDescriptors = listServiceDescriptors.Where(x => x.ServiceType.FullName.Contains(assemblyName)).ToList();

            Console.WriteLine($"{Environment.NewLine}{new string('-', 50)}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[Total Dependency Injection {listServiceDescriptors.Count}] Services");

            for (var index = 0; index < listServiceDescriptors.Count; index++)
            {
                var service = listServiceDescriptors[index];

                var no = index + 1;

                var maximumCharacter = new List<int> {
                        service.ServiceType?.Name?.Length ?? 0,
                        service.ImplementationType?.Name?.Length ?? 0,
                        service.Lifetime.ToString().Length
                    }.Max();

                Console.ResetColor();
                Console.WriteLine($"{no}.");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("    Service         |  ");
                Console.ResetColor();
                Console.Write($"{service.ServiceType?.Name?.PadRight(maximumCharacter)}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  |  {service.ServiceType?.FullName}");

                Console.Write("    Implementation  |  ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{service.ImplementationType?.Name?.PadRight(maximumCharacter)}");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  |  {service.ImplementationType?.FullName}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("    Lifetime        |  ");
                Console.WriteLine($"[{service.Lifetime.ToString()}]");
                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine($"{new string('-', 50)}{Environment.NewLine}");

            return services;
        }

        #endregion
    }
}