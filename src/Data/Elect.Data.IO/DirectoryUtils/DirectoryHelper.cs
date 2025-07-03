namespace Elect.Data.IO.DirectoryUtils
{
    public class DirectoryHelper
    {
        public static void CreateIfNotExist(params string[] paths)
        {
            foreach (var path in paths)
            {
                CheckHelper.CheckNullOrWhiteSpace(path, nameof(paths));
                var fullPath = PathHelper.GetFullPath(path);
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
            }
        }

        /// <summary>
        ///     Clear all content in the directory 
        /// </summary>
        /// <param name="paths"></param>
        public static void Empty(params string[] paths)
        {
            foreach (var path in paths)
            {
                // Remove all files
                var listFileInDirectory = Directory.GetFiles(path);
                foreach (var filePath in listFileInDirectory)
                {
                    File.Delete(filePath);
                }
                // Remove all directories
                var listDirectoryInDirectory = Directory.GetDirectories(path);
                foreach (var directoryPath in listDirectoryInDirectory)
                {
                    Directory.Delete(directoryPath);
                }
            }
        }

        /// <summary>
        ///     Check directory is empty or not 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        public class SpecialFolder
        {
            public static string GetCurrentWindowUserFolder()
            {
                string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    path = Directory.GetParent(path).ToString();
                }
                return path;
            }
        }
    }
}
