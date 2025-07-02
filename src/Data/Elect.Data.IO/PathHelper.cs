namespace Elect.Data.IO
{
    public class PathHelper
    {
        /// <summary>
        ///     Return absolute path 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            path = CorrectPathSeparatorChar(path);
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }
            if (!Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out var pathUri))
            {
                throw new ArgumentException($"Invalid path: {path}");
            }
            if (pathUri.IsAbsoluteUri)
            {
                return path;
            }
            path = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, path);
            return path;
        }
        /// <summary>
        ///     Correct path separator char fit with runtime OS.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CorrectPathSeparatorChar(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }
            path = path.Replace('\\', Path.DirectorySeparatorChar);
            path = path.Replace('/', Path.DirectorySeparatorChar);
            return path;
        }
    }
}
