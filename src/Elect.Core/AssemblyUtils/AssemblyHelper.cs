namespace Elect.Core.AssemblyUtils
{
    public class AssemblyHelper
    {
        public static string GetDirectoryPath(Assembly assembly)
        {
            UriBuilder uri = new UriBuilder(assembly.CodeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var directoryPath = Path.GetDirectoryName(path);
            return directoryPath;
        }
        public static List<Assembly> LoadAssemblies(params string[] dllFileFullPaths)
        {
            if (dllFileFullPaths?.Any() != true)
            {
                throw new ArgumentException($"{nameof(dllFileFullPaths)} can not be empty.", nameof(dllFileFullPaths));
            }
            dllFileFullPaths = dllFileFullPaths.Distinct().ToArray();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var dllFileFullPath in dllFileFullPaths)
            {
                // Assembly Directory to load
                var assemblyDirectoryPath = Path.GetDirectoryName(dllFileFullPath);
                AssemblyLoader assemblyLoader = new AssemblyLoader(assemblyDirectoryPath);
                // Assembly Name to load
                var dllNameWithoutExtension = Path.GetFileNameWithoutExtension(dllFileFullPath);
                var assemblyName = new AssemblyName(dllNameWithoutExtension);
                var assembly = assemblyLoader.LoadFromAssemblyName(assemblyName);
                assemblies.Add(assembly);
            }
            return assemblies;
        }
    }
}
