namespace Elect.Core.AssemblyUtils
{
    public class AssemblyHelper
    {
        public static string GetDirectoryPath(Assembly assembly)
        {
            var assemblyLocation = assembly.Location;
            
            // Handle cross-platform assembly location formats
            if (string.IsNullOrWhiteSpace(assemblyLocation))
            {
                throw new InvalidOperationException("Assembly location is not available.");
            }
            
            // Check if the location is already a file path (Unix/Linux/macOS) or a URI (Windows)
            string path;
            if (Uri.TryCreate(assemblyLocation, UriKind.Absolute, out var uri) && uri.IsFile)
            {
                // Windows format: file:///C:/path/to/assembly.dll
                path = Uri.UnescapeDataString(uri.LocalPath);
            }
            else
            {
                // Unix/Linux/macOS format: /path/to/assembly.dll
                path = assemblyLocation;
            }
            
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
            var assemblies = new List<Assembly>();
            foreach (var dllFileFullPath in dllFileFullPaths)
            {
                // Assembly Directory to load
                var assemblyDirectoryPath = Path.GetDirectoryName(dllFileFullPath);
                var assemblyLoader = new AssemblyLoader(assemblyDirectoryPath);
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
