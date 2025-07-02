namespace Elect.Mapper.AutoMapper
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        ///     Add Auto Mapper auto scan and register profile to Mapper Configuration by current
        ///     application assembly,
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddElectAutoMapper(this IServiceCollection services)
        {
            return services.AddElectAutoMapper(_ => { });
        }
        /// <summary>
        ///     Add Auto Mapper auto scan and register profile to Mapper Configuration by current
        ///     application assembly,
        /// </summary>
        /// <param name="services"> </param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddElectAutoMapper(this IServiceCollection services, [NotNull]ElectAutoMapperOptions configure)
        {
            return services.AddElectAutoMapper(_ =>
            {
                _.AdditionalInitial = configure.AdditionalInitial;
                _.IMapperLifeTime = configure.IMapperLifeTime;
                _.IsAssertConfigurationIsValid = configure.IsAssertConfigurationIsValid;
                _.IsCompileMappings = configure.IsCompileMappings;
                _.ListAssemblyFolderPath = configure.ListAssemblyFolderPath;
                _.ListAssemblyName = configure.ListAssemblyName;
            });
        }
        /// <summary>
        ///     Add Auto Mapper auto scan and register profile to Mapper Configuration by assembly,
        ///     default is use current runtime application assembly.
        /// </summary>
        /// <param name="services"> </param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddElectAutoMapper(this IServiceCollection services, [NotNull]Action<ElectAutoMapperOptions> configure)
        {
            services.Configure(configure);
            var options = configure.GetValue();
            // Scan Assemblies
            var listAllDllPath = new List<string>();
            foreach (var assemblyName in options.ListAssemblyName)
            {
                var rootAssemblyName = assemblyName.Split('.').FirstOrDefault();
                foreach (var assemblyFolderPath in options.ListAssemblyFolderPath)
                {
                    var dllFiles = Directory.GetFiles(assemblyFolderPath, $"{rootAssemblyName}.dll", SearchOption.AllDirectories);
                    var dllPrefixFiles = Directory.GetFiles(assemblyFolderPath, $"{rootAssemblyName}.*.dll", SearchOption.AllDirectories);
                    var listDllPath = dllFiles.Concat(dllPrefixFiles).Distinct();
                    listAllDllPath.AddRange(listDllPath);
                }
            }
            // Scan Mapper Profiles by Assemblies
            List<Assembly> assemblies = AssemblyHelper.LoadAssemblies(listAllDllPath.ToArray());
            var allTypes = assemblies.Where(a => a.GetName().Name != nameof(AutoMapper)).SelectMany(a => a.DefinedTypes).ToArray();
            var profileTypes = allTypes.Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t)).Where(t => !t.IsAbstract).Select(t => t.AsType()).ToList();
            // Initial Mapper with Profiles
            var mapperConfigurationExpression = new MapperConfigurationExpression();
            foreach (var profile in profileTypes)
            {
                mapperConfigurationExpression.AddProfile(profile);
            }
            options.AdditionalInitial?.Invoke(mapperConfigurationExpression);
            global::AutoMapper.Mapper.Initialize(mapperConfigurationExpression);
            // Add Mapper Config to DI
            // Resolver and Converter with transient lifetime
            var openTypes = new[] { typeof(IValueResolver<,,>), typeof(IMemberValueResolver<,,,>), typeof(ITypeConverter<,>) };
            var dependencyTypes = openTypes.SelectMany(openType => allTypes.Where(t => t.IsClass && !t.IsAbstract && t.AsType().IsImplementGenericInterface(openType)));
            foreach (var type in dependencyTypes)
            {
                services.AddTransient(type.AsType());
            }
            // Config with singleton lifetime
            services.AddSingleton(global::AutoMapper.Mapper.Configuration);
            // Mapper
            switch (options.IMapperLifeTime)
            {
                case ServiceLifetime.Scoped:
                    {
                        services.AddScoped<IMapper>(provider => new global::AutoMapper.Mapper(provider.GetRequiredService<IConfigurationProvider>(), provider.GetService));
                        break;
                    }
                case ServiceLifetime.Transient:
                    {
                        services.AddTransient<IMapper>(provider => new global::AutoMapper.Mapper(provider.GetRequiredService<IConfigurationProvider>(), provider.GetService));
                        break;
                    }
                case ServiceLifetime.Singleton:
                    {
                        services.AddSingleton<IMapper>(provider => new global::AutoMapper.Mapper(provider.GetRequiredService<IConfigurationProvider>(), provider.GetService));
                        break;
                    }
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(options.IMapperLifeTime), options.IMapperLifeTime, null);
                    }
            }
            // Assert Config
            if (options.IsAssertConfigurationIsValid)
            {
                global::AutoMapper.Mapper.AssertConfigurationIsValid();
            }
            // Compile Mapping
            if (options.IsCompileMappings)
            {
                global::AutoMapper.Mapper.Configuration.CompileMappings();
            }
            return services;
        }
    }
}
