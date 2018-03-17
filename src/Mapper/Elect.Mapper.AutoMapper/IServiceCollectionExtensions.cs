#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IServiceCollectionExtensions.cs </Name>
//         <Created> 16/03/2018 10:51:29 PM </Created>
//         <Key> e02e6e54-ce6b-425e-af29-8b17c75ebb50 </Key>
//     </File>
//     <Summary>
//         IServiceCollectionExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using AutoMapper;
using Elect.Core.ActionUtils;
using Elect.Core.AssemblyUtils;
using Elect.Core.Attributes;
using Elect.Core.TypeUtils;
using Elect.Mapper.AutoMapper.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
        ///     Add Auto Mapper auto scan and register profile to Mapper Configuration by assembly,
        ///     default is use current runtime application assembly.
        /// </summary>
        /// <param name="services"> </param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddElectAutoMapper(this IServiceCollection services, [NotNull]Action<ElectAutoMapperOptions> configure)
        {
            services.Configure(configure);

            ElectAutoMapperOptions options = configure.GetValue();

            // Scan Assemblies
            var listAllDllPath = new List<string>();

            foreach (var assemblyName in options.ListAssemblyName)
            {
                foreach (var assemblyFolderPath in options.ListAssemblyFolderPath)
                {
                    var listDllPath = 
                        Directory.GetFiles(assemblyFolderPath, $"{assemblyName}.dll")
                        .Concat(Directory.GetFiles(assemblyFolderPath, $"{assemblyName}.*.dll"))
                        .Distinct();

                    listAllDllPath.AddRange(listDllPath);
                }
            }

            List<Assembly> assemblies = AssemblyHelper.LoadAssemblies(listAllDllPath.ToArray());

            var allTypes = assemblies.Where(a => a.GetName().Name != nameof(AutoMapper)).SelectMany(a => a.DefinedTypes).ToArray();

            // Initial Mapper with Profiles
            var profileTypes = allTypes.Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t)).Where(t => !t.IsAbstract).Select(t => t.AsType()).ToList();

            global::AutoMapper.Mapper.Initialize(mapperConfigurationExpression =>
            {
                options.AdditionalInitial(mapperConfigurationExpression);

                foreach (var profile in profileTypes)
                {
                    mapperConfigurationExpression.AddProfile(profile);
                }
            });

            // Assert Config
            if (options.IsAssertConfigurationIsValid)
            {
                global::AutoMapper.Mapper.AssertConfigurationIsValid();
            }

            if (options.IsCompileMappings)
            {
                global::AutoMapper.Mapper.Configuration.CompileMappings();
            }

            // Resolver and Converter with transient lifetime
            var openTypes = new[] { typeof(IValueResolver<,,>), typeof(IMemberValueResolver<,,,>), typeof(ITypeConverter<,>) };

            var dependencyTypes = openTypes.SelectMany(openType => allTypes.Where(t => t.IsClass && !t.IsAbstract && t.AsType().IsImplementGenericInterface(openType)));

            foreach (var type in dependencyTypes)
            {
                services.AddTransient(type.AsType());
            }

            // Config with singleton lifetime
            services.AddSingleton(global::AutoMapper.Mapper.Configuration);

            // Add Mapper to DI
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

            return services;
        }
    }
}