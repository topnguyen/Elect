namespace Elect.Test.Core.AssemblyUtils
{
    [TestClass]
    public class AssemblyExtensionsUnitTest
    {
        [TestMethod]
        public void GetDirectoryPath_ReturnsValidPath()
        {
            var assembly = typeof(AssemblyExtensionsUnitTest).GetTypeInfo().Assembly;
            var path = assembly.GetDirectoryPath();
            Assert.IsFalse(string.IsNullOrWhiteSpace(path));
            Assert.IsTrue(System.IO.Directory.Exists(path));
        }
    }
}
