namespace Elect.Test.Data.IO
{
    [TestClass]
    public class PathHelperUnitTest
    {
        [TestMethod]
        public void GetFullPath_NullOrWhitespace_ReturnsInput()
        {
            Assert.IsNull(PathHelper.GetFullPath(null));
            Assert.AreEqual("", PathHelper.GetFullPath(""));
            Assert.AreEqual("   ", PathHelper.GetFullPath("   "));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFullPath_InvalidPath_Throws()
        {
            PathHelper.GetFullPath("@@invalid|path");
        }
        [TestMethod]
        public void GetFullPath_AbsolutePath_ReturnsAsIs()
        {
            var absPath = Path.GetFullPath("test.txt");
            Assert.AreEqual(absPath, PathHelper.GetFullPath(absPath));
        }
        [TestMethod]
        [DataRow("folder/file.txt")]
        [DataRow("folder\\file.txt")]
        [DataRow("folder\\subfolder/file.txt")]
        public void GetFullPath_RelativePath_CombinesWithBasePath(string relPath)
        {
            var expected = Path.Combine(
                PlatformServices.Default.Application.ApplicationBasePath,
                relPath.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar)
            );
            Assert.AreEqual(expected, PathHelper.GetFullPath(relPath));
        }
        [TestMethod]
        [DataRow(null, null)]
        [DataRow("", "")]
        [DataRow("   ", "   ")]
        public void CorrectPathSeparatorChar_NullOrWhitespace_ReturnsInput(string input, string expected)
        {
            Assert.AreEqual(expected, PathHelper.CorrectPathSeparatorChar(input));
        }
        [TestMethod]
        [DataRow("folder\\subfolder\\file.txt")]
        [DataRow("folder/subfolder/file.txt")]
        [DataRow("folder\\subfolder/file.txt")]
        public void CorrectPathSeparatorChar_ReplacesWithDirSeparator(string input)
        {
            var expected = input.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
            Assert.AreEqual(expected, PathHelper.CorrectPathSeparatorChar(input));
        }
    }
}
