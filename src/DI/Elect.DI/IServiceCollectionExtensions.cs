namespace Elect.DI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection Removes(this IServiceCollection services, [NotNull] params Type[] serviceTypes)
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
            return services.Any(x => x.ServiceType == typeof(TService));
        }
        #region Add If Any
        public static IServiceCollection AddScopedIfAny<TService, TImplementation>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate)
            where TService : class where TImplementation : class, TService
        {
            if (services.Any(predicate))
            {
                services.AddScoped<TService, TImplementation>();
            }
            return services;
        }
        public static IServiceCollection AddScopedIfAny<TService>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.Any(predicate))
            {
                services.AddScoped<TService>();
            }
            return services;
        }
        public static IServiceCollection AddTransientIfAny<TService, TImplementation>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate)
            where TService : class where TImplementation : class, TService
        {
            if (services.Any(predicate))
            {
                services.AddTransient<TService, TImplementation>();
            }
            return services;
        }
        public static IServiceCollection AddTransientIfAny<TService>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.Any(predicate))
            {
                services.AddTransient<TService>();
            }
            return services;
        }
        public static IServiceCollection AddSingletonIfAny<TService, TImplementation>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate)
            where TService : class where TImplementation : class, TService
        {
            if (services.Any(predicate))
            {
                services.AddSingleton<TService, TImplementation>();
            }
            return services;
        }
        public static IServiceCollection AddSingletonIfAny<TService>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.Any(predicate))
            {
                services.AddSingleton<TService>();
            }
            return services;
        }
        #endregion
        #region Add If All
        public static IServiceCollection AddScopedIfAll<TService, TImplementation>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate)
            where TService : class where TImplementation : class, TService
        {
            if (services.All(predicate))
            {
                services.AddScoped<TService, TImplementation>();
            }
            return services;
        }
        public static IServiceCollection AddScopedIfAll<TService>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.All(predicate))
            {
                services.AddScoped<TService>();
            }
            return services;
        }
        public static IServiceCollection AddTransientIfAll<TService, TImplementation>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate)
            where TService : class where TImplementation : class, TService
        {
            if (services.All(predicate))
            {
                services.AddTransient<TService, TImplementation>();
            }
            return services;
        }
        public static IServiceCollection AddTransientIfAll<TService>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.All(predicate))
            {
                services.AddTransient<TService>();
            }
            return services;
        }
        public static IServiceCollection AddSingletonIfAll<TService, TImplementation>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate)
            where TService : class where TImplementation : class, TService
        {
            if (services.All(predicate))
            {
                services.AddSingleton<TService, TImplementation>();
            }
            return services;
        }
        public static IServiceCollection AddSingletonIfAll<TService>(this IServiceCollection services,
            [NotNull] Func<ServiceDescriptor, bool> predicate) where TService : class
        {
            if (services.All(predicate))
            {
                services.AddSingleton<TService>();
            }
            return services;
        }
        #endregion
        #region Add If Not Exist
        public static IServiceCollection
            AddScopedIfNotExist<TService, TImplementation>(this IServiceCollection services) where TService : class
            where TImplementation : class, TService
        {
            services.AddScopedIfAll<TService, TImplementation>(x => x.ServiceType != typeof(TService));
            return services;
        }
        public static IServiceCollection AddScopedIfNotExist<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddScopedIfAll<TService>(x => x.ServiceType != typeof(TService));
            return services;
        }
        public static IServiceCollection
            AddTransientIfNotExist<TService, TImplementation>(this IServiceCollection services) where TService : class
            where TImplementation : class, TService
        {
            services.AddTransientIfAll<TService, TImplementation>(x => x.ServiceType != typeof(TService));
            return services;
        }
        public static IServiceCollection AddTransientIfNotExist<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddTransientIfAll<TService>(x => x.ServiceType != typeof(TService));
            return services;
        }
        public static IServiceCollection
            AddSingletonIfNotExist<TService, TImplementation>(this IServiceCollection services) where TService : class
            where TImplementation : class, TService
        {
            services.AddSingletonIfAll<TService, TImplementation>(x => x.ServiceType != typeof(TService));
            return services;
        }
        public static IServiceCollection AddSingletonIfNotExist<TService>(this IServiceCollection services)
            where TService : class
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
        public static IServiceCollection AddElectDI(this IServiceCollection services,
            [NotNull] ElectDIOptions configure)
        {
            return services.AddElectDI(_ =>
            {
                _.ListAssemblyFolderPath = configure.ListAssemblyFolderPath;
                _.ListAssemblyName = configure.ListAssemblyName;
            });
        }
        /// <summary>
        ///     Auto scan and register implementation of service follow
        ///     <see cref="Elect.DI.Attributes" /> life time setup.
        /// </summary>
        public static IServiceCollection AddElectDI(this IServiceCollection services,
            [NotNull] Action<ElectDIOptions> configure)
        {
            services.Configure(configure);
            ElectDIOptions options = configure.GetValue();
            var scanner = new Scanner();
            foreach (var assemblyName in options.ListAssemblyName)
            {
                var rootAssemblyName = assemblyName.Split('.').FirstOrDefault();
                foreach (var assemblyFolderPath in options.ListAssemblyFolderPath)
                {
                    // dll files
                    scanner.RegisterAssemblies(services, assemblyFolderPath, $"{rootAssemblyName}.dll");
                    // dll prefix files
                    scanner.RegisterAssemblies(services, assemblyFolderPath, $"{rootAssemblyName}.*.dll");
                }
            }
            return services;
        }
        #endregion
        #region Print
        public static IServiceCollection PrintServiceAddedToConsole(this IServiceCollection services)
        {
            return services.PrintServiceAddedToConsole(_ => { });
        }
        public static IServiceCollection PrintServiceAddedToConsole(this IServiceCollection services,
            [NotNull] ElectPrintRegisteredToConsoleOptions configure)
        {
            return services.PrintServiceAddedToConsole(_ =>
            {
                _.ListAssemblyName = configure.ListAssemblyName;
                _.IsMinimalDisplay = configure.IsMinimalDisplay;
                _.PrimaryColor = configure.PrimaryColor;
                _.SecondaryColor = configure.SecondaryColor;
                _.SortAscBy = configure.SortAscBy;
            });
        }
        public static IServiceCollection PrintServiceAddedToConsole(this IServiceCollection services,
            [NotNull] Action<ElectPrintRegisteredToConsoleOptions> configure)
        {
            services.Configure(configure);
            var options = configure.GetValue();
            var listServiceDescriptors = services.ToList();
            listServiceDescriptors = listServiceDescriptors
                .Where(x =>
                    options.ListAssemblyName.Any(y => x?.ServiceType?.FullName != null &&
                                                      x.ServiceType.FullName.Contains(y.Split('.').First())
                    )
                )
                .ToList();
            if (options.SortAscBy == ElectDIPrintSortBy.Service)
            {
                listServiceDescriptors = listServiceDescriptors.OrderBy(x => GetNormForServiceAdded(x.ServiceType.Name))
                    .ToList();
            }
            else if (options.SortAscBy == ElectDIPrintSortBy.Implementation)
            {
                listServiceDescriptors = listServiceDescriptors
                    .OrderBy(x => GetNormForServiceAdded(x.ImplementationType?.Name)).ToList();
            }
            else
            {
                listServiceDescriptors = listServiceDescriptors
                    .OrderBy(x => GetNormForServiceAdded(x.Lifetime.ToString())).ToList();
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = options.PrimaryColor;
            Console.WriteLine($"Elect DI > Registered {listServiceDescriptors.Count} Services.");
            Console.WriteLine();
            if (!listServiceDescriptors.Any())
            {
                return services;
            }
            if (options.IsMinimalDisplay)
            {
                PrintServiceAddedToConsoleMinimalDisplayFormat(listServiceDescriptors, options.SecondaryColor,
                    options.PrimaryColor);
            }
            else
            {
                PrintServiceAddedToConsoleFullDisplayFormat(listServiceDescriptors, options.SecondaryColor,
                    options.PrimaryColor);
            }
            Console.ResetColor();
            Console.WriteLine();
            return services;
        }
        private static void PrintServiceAddedToConsoleMinimalDisplayFormat(
            List<ServiceDescriptor> listServiceDescriptors, ConsoleColor consoleDimColor, ConsoleColor consoleTextColor)
        {
            var noMaxLength = listServiceDescriptors.Count.ToString().Length;
            if (noMaxLength < "No.".Length)
            {
                noMaxLength = "No.".Length;
            }
            var serviceNameMaxLength = listServiceDescriptors
                .Select(x => GetNormForServiceAdded(x.ServiceType.Name).Length).Max();
            if (serviceNameMaxLength < "Service".Length)
            {
                serviceNameMaxLength = "Service".Length;
            }
            var implementationNameMaxLength = listServiceDescriptors
                .Select(x => GetNormForServiceAdded(x.ImplementationType?.Name ?? x.ServiceType.GenericTypeArguments.LastOrDefault()?.Name ?? "-").Length).Max();
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
                Console.Write($"{no}".PadRight(noMaxLength));
                Console.ResetColor();
                Console.Write("    |    ");
                // Service
                Console.ForegroundColor = consoleTextColor;
                Console.Write(GetNormForServiceAdded(service.ServiceType?.Name).PadRight(serviceNameMaxLength));
                Console.ResetColor();
                Console.Write("    |    ");
                // Implementation
                var implementationName = service.ImplementationType?.Name ?? service.ServiceType.GenericTypeArguments.LastOrDefault()?.Name ?? "-";
                implementationName = GetNormForServiceAdded(implementationName);
                Console.ForegroundColor = consoleDimColor;
                Console.Write(implementationName.PadRight(implementationNameMaxLength));
                Console.ResetColor();
                Console.Write("    |    ");
                // Lifetime
                Console.ForegroundColor = consoleTextColor;
                Console.WriteLine(service.Lifetime.ToString().PadRight(lifeTimeMaxLength));
            }
        }
        private static void PrintServiceAddedToConsoleFullDisplayFormat(List<ServiceDescriptor> listServiceDescriptors,
            ConsoleColor consoleDimColor, ConsoleColor consoleTextColor)
        {
            var maximumCharacter =
                new List<int>
                {
                    listServiceDescriptors.Select(x => GetNormForServiceAdded(x.ServiceType.Name).Length).Max(),
                    listServiceDescriptors.Select(x => GetNormForServiceAdded(x.ImplementationType?.Name ?? x.ServiceType.GenericTypeArguments.LastOrDefault()?.Name ?? "-").Length).Max(),
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
                Console.Write(GetNormForServiceAdded(service.ServiceType?.Name).PadRight(maximumCharacter));
                Console.ResetColor();
                Console.Write("    |    ");
                Console.ForegroundColor = consoleDimColor;
                Console.WriteLine(GetNormForServiceAdded(service.ServiceType?.FullName).PadRight(maximumCharacter));
                // Implementation
                Console.ForegroundColor = consoleDimColor;
                Console.Write("    Implementation");
                Console.ResetColor();
                Console.Write("    |    ");
                Console.ForegroundColor = consoleDimColor;
                Console.Write(GetNormForServiceAdded(service.ImplementationType?.Name ?? service.ServiceType.GenericTypeArguments.LastOrDefault()?.Name ?? "-").PadRight(maximumCharacter)?.PadRight(maximumCharacter));
                Console.ResetColor();
                Console.Write("    |    ");
                Console.ForegroundColor = consoleDimColor;
                Console.WriteLine(GetNormForServiceAdded(service.ImplementationType?.FullName ?? service.ServiceType.GenericTypeArguments.LastOrDefault()?.FullName ?? "-").PadRight(maximumCharacter));
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
        [NotNull]
        private static string GetNormForServiceAdded(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "-";
            }
            // Replace To Readable Generic if can
            return Regex.Replace(value, @"`\d", "<T>");
        }
        #endregion
    }
}
