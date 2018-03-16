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

using Elect.Core.ActionUtils;
using Elect.Core.Attributes;
using Elect.DI.Options;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddElectDI(this IServiceCollection services)
        {
            return services.AddElectDI(_ => { });
        }

        /// <summary>
        ///     Auto scan and register implementation of service follow
        ///     <see cref="Elect.DI.Attributes" /> life time setup.
        /// </summary>
        public static IServiceCollection AddElectDI(this IServiceCollection services, [NotNull]Action<ElectDIOptions> configure)
        {
            ElectDIOptions options = configure.GetValue();

            var scanner = new Scanner();

            scanner.RegisterAssemblies(services, options.AssembliesFolderPath, $"{options.RootAssemblyName}.dll");

            scanner.RegisterAssemblies(services, options.AssembliesFolderPath, $"{options.RootAssemblyName}.*.dll");

            services.AddSingleton(scanner);

            return services;
        }

        #endregion

        #region Print

        public static IServiceCollection PrintRegisteredToConsole(this IServiceCollection services)
        {
            return services.PrintRegisteredToConsole(_ => { });
        }

        public static IServiceCollection PrintRegisteredToConsole(this IServiceCollection services, [NotNull]Action<ElectPrintRegisteredToConsoleOptions> configure)
        {
            ElectPrintRegisteredToConsoleOptions options = configure.GetValue();

            var listServiceDescriptors = services.ToList();

            listServiceDescriptors = listServiceDescriptors.Where(x => x.ServiceType.FullName.Contains(options.RootAssemblyName)).ToList();

            var consoleTextColor = ConsoleColor.Yellow;
            var consoleDimColor = ConsoleColor.DarkGray;

            Console.WriteLine();
            Console.WriteLine($"{new string('-', 100)}");
            Console.WriteLine();

            Console.ForegroundColor = consoleTextColor;
            Console.WriteLine($"# Elect DI > Registered: {listServiceDescriptors.Count} Services");
            Console.WriteLine();

            if (options.IsMinimalDisplay)
            {
                PrintRegisteredToConsoleMinimalDisplayFormat(listServiceDescriptors, consoleDimColor, consoleTextColor);
            }
            else
            {
                PrintRegisteredToConsoleFullDisplayFormat(listServiceDescriptors, consoleDimColor, consoleTextColor);
            }

            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine($"{new string('-', 100)}");

            return services;
        }

        private static void PrintRegisteredToConsoleMinimalDisplayFormat(List<ServiceDescriptor> listServiceDescriptors, ConsoleColor consoleDimColor, ConsoleColor consoleTextColor)
        {
            var noMaxLength = listServiceDescriptors.Count + 1;
            if (noMaxLength < "No.".Length)
            {
                noMaxLength = "No.".Length;
            }

            var serviceNameMaxLength = listServiceDescriptors.Select(x => x.ServiceType.Name.Length).Max();
            if (serviceNameMaxLength < "Service".Length)
            {
                serviceNameMaxLength = "Service".Length;
            }

            var implementationNameMaxLength = listServiceDescriptors.Select(x => x.ImplementationType.Name.Length).Max();
            if (implementationNameMaxLength < "Implementation".Length)
            {
                implementationNameMaxLength = "Implementation".Length;
            }

            var lifeTimeMaxLength = listServiceDescriptors.Select(x => x.Lifetime.ToString().Length).Max();
            if (lifeTimeMaxLength < "Lifetime".Length)
            {
                lifeTimeMaxLength = "Lifetime".Length;
            }

            // Header

            Console.ResetColor();
            Console.Write("    ");
            Console.Write("No.".PadRight(noMaxLength));
            Console.Write("    |    ");
            Console.Write("Service".PadRight(serviceNameMaxLength));
            Console.Write("    |    ");
            Console.Write("Implementation".PadRight(implementationNameMaxLength));
            Console.Write("    |    ");
            Console.Write("Lifetime");
            Console.WriteLine();

            Console.WriteLine($"{new string('-', 4 + noMaxLength + serviceNameMaxLength + implementationNameMaxLength + lifeTimeMaxLength + "    |    ".Length * 3)}");

            for (var index = 0; index < listServiceDescriptors.Count; index++)
            {
                var service = listServiceDescriptors[index];

                var no = index + 1;

                // No

                Console.ResetColor();
                Console.Write("    ");
                Console.Write($"{no}.".PadRight(noMaxLength));

                Console.ResetColor();
                Console.Write("    |    ");

                // Service

                Console.ForegroundColor = consoleTextColor;
                Console.Write(service.ServiceType?.Name?.PadRight(serviceNameMaxLength));

                Console.ResetColor();
                Console.Write("    |    ");

                // Implementation

                Console.ForegroundColor = consoleDimColor;
                Console.Write(service.ImplementationType?.Name?.PadRight(implementationNameMaxLength));

                Console.ResetColor();
                Console.Write("    |    ");

                // Lifetime

                Console.ForegroundColor = consoleTextColor;
                Console.WriteLine(service.Lifetime.ToString().PadRight(lifeTimeMaxLength));
            }
        }

        private static void PrintRegisteredToConsoleFullDisplayFormat(List<ServiceDescriptor> listServiceDescriptors, ConsoleColor consoleDimColor, ConsoleColor consoleTextColor)
        {
            var maximumCharacter =
                new List<int>
                {
                    listServiceDescriptors.Select(x => x.ServiceType.Name.Length).Max(),
                    listServiceDescriptors.Select(x => x.ImplementationType.Name.Length).Max(),
                    listServiceDescriptors.Select(x => x.Lifetime.ToString().Length).Max()
                }.Max();

            for (var index = 0; index < listServiceDescriptors.Count; index++)
            {
                var service = listServiceDescriptors[index];

                var no = index + 1;

                // No

                Console.ResetColor();
                Console.Write($"{no}.".PadRight(18));
                Console.Write("    |    ");
                Console.Write("".PadRight(maximumCharacter));
                Console.Write("    |    ");
                Console.WriteLine();

                // Service

                Console.ForegroundColor = consoleDimColor;
                Console.Write("    Service       ");

                Console.ResetColor();
                Console.Write("    |    ");

                Console.ForegroundColor = consoleTextColor;
                Console.Write(service.ServiceType?.Name?.PadRight(maximumCharacter));

                Console.ResetColor();
                Console.Write("    |    ");

                Console.ForegroundColor = consoleDimColor;
                Console.WriteLine(service.ServiceType?.FullName);

                // Implementation

                Console.ForegroundColor = consoleDimColor;
                Console.Write("    Implementation");

                Console.ResetColor();
                Console.Write("    |    ");

                Console.ForegroundColor = consoleDimColor;
                Console.Write(service.ImplementationType?.Name?.PadRight(maximumCharacter));

                Console.ResetColor();
                Console.Write("    |    ");

                Console.ForegroundColor = consoleDimColor;
                Console.WriteLine(service.ImplementationType?.FullName);

                // Life Time

                Console.ForegroundColor = consoleDimColor;
                Console.Write("    Lifetime      ");

                Console.ResetColor();
                Console.Write("    |    ");

                Console.ForegroundColor = consoleTextColor;
                Console.Write(service.Lifetime.ToString().PadRight(maximumCharacter));

                Console.ResetColor();
                Console.WriteLine("    |    ");
            }
        }

        #endregion
    }
}