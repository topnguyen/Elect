namespace Elect.Data.IO.FileUtils
{
    public class FileHelper
    {
        public static void CreateIfNotExist(params string[] paths)
        {
            foreach (var path in paths)
            {
                CheckHelper.CheckNullOrWhiteSpace(path, nameof(paths));
                var fullPath = PathHelper.GetFullPath(path);
                if (!File.Exists(fullPath))
                {
                    File.Create(fullPath);
                }
            }
        }    
        /// <summary>
        ///     Create file then set permission (only apply for Linux platform)
        /// </summary>
        /// <param name="permissions"></param>
        /// <param name="paths"></param>
        public static void CreateIfNotExist(FileAccessPermissions permissions, params string[] paths)
        {
            CreateIfNotExist(paths);
            SetLinuxFilePermission(FileAccessPermissions.AllPermissions, paths);
        }
        public static void SetLinuxFilePermission(FileAccessPermissions permissions, params string[] paths)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return;
            }
            foreach (var path in paths)
            {
                var unixFileInfo = new UnixFileInfo(path) {FileAccessPermissions = permissions};
                unixFileInfo.Refresh();
            }
        }
        /// <summary>
        ///     Create an empty temp file with extension. 
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string CreateTempFile(string extension)
        {
            string temp = Path.GetTempFileName();
            extension = extension.StartsWith(".") ? extension : $".{extension}";
            var path = Path.ChangeExtension(temp, extension);
            File.Move(temp, path);
            return path;
        }
        /// <summary>
        ///     Create an temp file from stream with extension 
        /// </summary>
        /// <param name="stream">   </param>
        /// <param name="extension"></param>
        /// <returns></returns>
        internal static string CreateTempFile(Stream stream, string extension)
        {
            string filePath = CreateTempFile(extension);
            using (var fileStream = File.OpenWrite(filePath))
            {
                // Copy file to stream
                stream.Position = 0;
                stream.CopyTo(fileStream);
                // Reset position after copy
                stream.Position = 0;
            }
            return filePath;
        }
        /// <summary>
        ///     Delete file if exist and can delete 
        /// </summary>
        /// <param name="path"></param>
        /// <returns> return true if success delete file, else is fail </returns>
        public static bool SafeDelete(string path)
        {
            try
            {
                path = PathHelper.GetFullPath(path);
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    return true;
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidBase64(string value)
        {
            try
            {
                Convert.FromBase64String(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void WriteToStream(string filePath, MemoryStream stream)
        {
            if (!Uri.IsWellFormedUriString(filePath, UriKind.RelativeOrAbsolute) && !File.Exists(filePath))
            {
                return;
            }
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                // Replace contents.
                stream.SetLength(0);
                // Copy file to stream
                fileStream.Position = 0;
                fileStream.CopyTo(stream);
                // Reset position after copy
                stream.Position = 0;
            }
        }
        /// <summary>
        ///     Replaces characters in <c> fileName </c> that are not allowed in file names with the
        ///     specified replacement character.
        /// </summary>
        /// <param name="fileName">      
        ///     Text to make into a valid filename. The same string is returned if it is valid already.
        /// </param>
        /// <param name="replacement">   
        ///     Replacement character, or null to simply remove bad characters.
        /// </param>
        /// <param name="isFancy">       
        ///     Whether to replace quotes and slashes with the non-ASCII characters ” and ⁄.
        /// </param>
        /// <param name="isRemoveAccent"> Remove all diacritics (accents) in string </param>
        /// <returns>
        ///     A string that can be used as a filename. If the output string would otherwise be
        ///     empty, returns <see params="replacement" /> as string.
        /// </returns>
        /// <remarks>
        ///     Valid file name also follow maximum length is 260 characters rule (split from right
        ///     to left if length &gt; 260)
        /// </remarks>
        public static string MakeValidFileName(string fileName, char? replacement = '_', bool isFancy = true, bool isRemoveAccent = true)
        {
            if (isRemoveAccent)
            {
                fileName = StringHelper.RemoveAccents(fileName);
            }
            var builder = new StringBuilder(fileName.Length);
            var invalids = Path.GetInvalidFileNameChars();
            bool changed = false;
            foreach (var c in fileName)
            {
                if (invalids.Contains(c))
                {
                    changed = true;
                    var replace = replacement ?? '\0';
                    if (isFancy)
                    {
                        if (c == '"')
                        {
                            replace = '”'; // U+201D right double quotation mark
                        }
                        else if (c == '\'')
                        {
                            replace = '’'; // U+2019 right single quotation mark
                        }
                        else if (c == '/')
                        {
                            replace = '⁄'; // U+2044 fraction slash
                        }
                    }
                    if (replace != '\0')
                    {
                        builder.Append(replace);
                    }
                }
                else
                {
                    builder.Append(c);
                }
            }
            if (builder.Length == 0)
            {
                return replacement?.ToString();
            }
            fileName = changed ? builder.ToString() : fileName;
            if (fileName?.Length > 260)
            {
                return fileName.Substring(fileName.Length - 260);
            }
            return fileName;
        }
    }
}
