namespace Elect.Core.AssemblyUtils
{
    internal class AssemblyLoader : AssemblyLoadContext
    {
        internal List<AssemblyName> ListLoadedAssemblyName { get; } = new List<AssemblyName>();
        internal readonly string AssemblyDirectoryPath;
        internal AssemblyLoader(string assemblyDirectoryPath)
        {
            AssemblyDirectoryPath = assemblyDirectoryPath;
            // Update List Loaded Assembly
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            var listLoadedAssemblyName = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId).ToList();
            foreach (var assemblyName in listLoadedAssemblyName)
            {
                if (!ListLoadedAssemblyName.Contains(assemblyName))
                {
                    ListLoadedAssemblyName.Add(assemblyName);
                }
            }
        }
        /// <inheritdoc />
        /// <summary>
        ///     Load an assembly, if the assembly already loaded then return 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            // Check if assembly already added by Dependency (Reference)
            if (ListLoadedAssemblyName.Any(x => string.Equals(x.Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }
            // Load Assembly not yet load
            var assemblyFilePath = Path.Combine(AssemblyDirectoryPath, assemblyName.Name) + ".dll";
            var assembly = File.Exists(assemblyFilePath) ? LoadFromAssemblyPath(assemblyFilePath) : Assembly.Load(assemblyName);
            // Add to loaded
            ListLoadedAssemblyName.Add(assemblyName);
            return assembly;
        }
    }
}
