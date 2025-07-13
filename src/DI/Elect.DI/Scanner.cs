namespace Elect.DI
{
    public class Scanner
    {
        public void RegisterAssembly([NotNull] IServiceCollection services, [NotNull] Assembly assembly)
        {
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                var attributes = typeInfo?.GetCustomAttributes().ToList();
                if (attributes?.Any() != true)
                {
                    continue;
                }
                foreach (var attribute in attributes)
                {
                    var isDependencyAttribute = attribute is DependencyAttribute;
                    if (!isDependencyAttribute)
                    {
                        continue;
                    }
                    var dependencyAttribute = (DependencyAttribute) attribute;
                    var serviceDescriptor = dependencyAttribute.BuildServiceDescriptor(typeInfo);
                    // Check is service already register from difference implementation => throw exception
                    var implementationTypeRegistered =
                        services
                            .FirstOrDefault(x => x.ServiceType.FullName == serviceDescriptor.ServiceType.FullName
                                                 && x.ImplementationType != serviceDescriptor.ImplementationType)
                            ?.ImplementationType;
                    if (implementationTypeRegistered != null)
                    {
                        throw new NotSupportedException(
                            $"Conflict implementation, ${serviceDescriptor.ImplementationType} try to register for {serviceDescriptor.ServiceType.FullName} but it already register by {implementationTypeRegistered.FullName} before.");
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
        /// <param name="services">          </param>
        /// <param name="assemblyFolderPath"></param>
        /// <param name="fileSearchPattern">  Search Pattern by <c> Directory.GetFiles </c> </param>
        /// <returns> List of loaded assembly </returns>
        public List<Assembly> RegisterAssemblies([NotNull] IServiceCollection services, [NotNull] string assemblyFolderPath, [NotNull] string fileSearchPattern)
        {
            if (services == null)
            {
                throw new ArgumentNullException($"{nameof(services)} cannot be null.", nameof(services));
            }
            CheckHelper.CheckNullOrWhiteSpace(assemblyFolderPath, nameof(assemblyFolderPath));
            CheckHelper.CheckNullOrWhiteSpace(fileSearchPattern, nameof(fileSearchPattern));
            var listDllPath = Directory.GetFiles(assemblyFolderPath, fileSearchPattern, SearchOption.AllDirectories)
                .ToList();
            if (listDllPath.Any() != true)
            {
                return null;
            }
            List<Assembly> assemblies = AssemblyHelper.LoadAssemblies(listDllPath.ToArray());
            foreach (var assembly in assemblies)
            {
                RegisterAssembly(services, assembly);
            }
            return assemblies;
        }
        /// <param name="folderPaths">The folder path store assemblies, default is null - mean search project base folder path</param>
        /// <param name="searchPatterns"> Dll files search pattern. Use <c> Directory.GetFiles </c> to search files, default is null = mean "{root assembly}.dll" and "{root assembly}.*.dll"</param>
        /// <returns> List of loaded assembly </returns>
        public List<Assembly> GetAssemblies([NotNull] List<string> folderPaths = null, [NotNull] List<string> searchPatterns = null)
        {
            if (folderPaths?.Any() != true)
            {
                folderPaths = new List<string>
                {
                    PlatformServices.Default.Application.ApplicationBasePath
                };
            }
            if (searchPatterns?.Any() != true)
            {
                searchPatterns = new List<string>
                {
                    PlatformServices.Default.Application.ApplicationName
                };
            }
            // Scan Assemblies
            var listAllDllPath = new List<string>();
            foreach (var assemblyName in searchPatterns)
            {
                var rootAssemblyName = assemblyName.Split('.').FirstOrDefault();
                foreach (var assemblyFolderPath in folderPaths)
                {
                    var dllFiles = Directory.GetFiles(assemblyFolderPath, $"{rootAssemblyName}.dll", SearchOption.AllDirectories);
                    var dllPrefixFiles = Directory.GetFiles(assemblyFolderPath, $"{rootAssemblyName}.*.dll", SearchOption.AllDirectories);
                    var listDllPath = dllFiles.Concat(dllPrefixFiles).Distinct();
                    listAllDllPath.AddRange(listDllPath);
                }
            }
            if (!listAllDllPath.Any())
            {
                return null;
            }
            List<Assembly> assemblies = AssemblyHelper.LoadAssemblies(listAllDllPath.ToArray());
            return assemblies;
        }
    }
}
