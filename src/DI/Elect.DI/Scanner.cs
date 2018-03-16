#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> Scanner.cs </Name>
//         <Created> 16/03/2018 11:15:36 AM </Created>
//         <Key> 2f339332-c6a2-47a6-89d0-cc39b26c3fc3 </Key>
//     </File>
//     <Summary>
//         Scanner.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.DI.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Elect.DI
{
    public class Scanner
    {
        public AssemblyLoader AssemblyLoader { get; private set; } = new AssemblyLoader();

        /// <summary>
        ///     Register Assembly by Name 
        /// </summary>
        /// <param name="services">    </param>
        /// <param name="assemblyName"></param>
        public void RegisterAssembly(IServiceCollection services, AssemblyName assemblyName)
        {
            var assembly = AssemblyLoader.LoadFromAssemblyName(assemblyName);

            foreach (var typeInfo in assembly.DefinedTypes)
            {
                foreach (var customAttribute in typeInfo.GetCustomAttributes())
                {
                    var customAttributeType = customAttribute.GetType();

                    var isDependencyAttribute = typeof(DependencyAttribute).IsAssignableFrom(customAttributeType);

                    if (!isDependencyAttribute)
                    {
                        continue;
                    }

                    var serviceDescriptor = ((DependencyAttribute)customAttribute).BuildServiceDescriptor(typeInfo);

                    // Check is service already register from difference implementation => throw exception

                    var implementationTypeRegistered =
                        services
                            .FirstOrDefault(x => x.ServiceType.FullName == serviceDescriptor.ServiceType.FullName
                                                 && x.ImplementationType != serviceDescriptor.ImplementationType)
                            ?.ImplementationType;

                    if (implementationTypeRegistered != null)
                    {
                        throw new NotSupportedException(
                            "Conflict implementation, " +
                            $"${serviceDescriptor.ImplementationType} try to register for {serviceDescriptor.ServiceType.FullName} but it already register by {implementationTypeRegistered.FullName} before.");
                    }

                    // Check is service already register from same implementation => remove existing,
                    // replace by new one to make use last define life time cycle

                    var isExistSameImplementationRegistered =
                        services.Any(x => x.ServiceType.FullName == serviceDescriptor.ServiceType.FullName
                                          && x.ImplementationType == serviceDescriptor.ImplementationType);

                    if (isExistSameImplementationRegistered)
                    {
                        services = services.Replace(serviceDescriptor);
                    }
                    else
                    {
                        services.Add(serviceDescriptor);
                    }
                }
            }
        }

        /// <summary>
        ///     Register all assemblies 
        /// </summary>
        /// <param name="services">          </param>
        /// <param name="searchPattern">      Search Pattern by Directory.GetFiles </param>
        /// <param name="assemblyFolderPath">
        ///     null or empty will use Application Base Path
        /// </param>
        public void RegisterAssemblies(IServiceCollection services, string searchPattern = "*.dll", string assemblyFolderPath = null)
        {
            // Update assembly loader with folder path
            AssemblyLoader = new AssemblyLoader(assemblyFolderPath);

            var listDllFileFullPath = Directory.GetFiles(assemblyFolderPath, searchPattern).ToList();

            foreach (var dllFileFullPath in listDllFileFullPath)
            {
                var dllNameWithoutExtension = Path.GetFileNameWithoutExtension(dllFileFullPath);

                RegisterAssembly(services, new AssemblyName(dllNameWithoutExtension));
            }
        }
    }
}