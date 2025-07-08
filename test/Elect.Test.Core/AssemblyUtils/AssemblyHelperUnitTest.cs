namespace Elect.Test.Core.AssemblyUtils
{
    [TestClass]
    public class AssemblyHelperUnitTest
    {
        [TestMethod]
        public void GetDirectoryPath_ReturnsValidPath()
        {
            var assembly = typeof(AssemblyHelperUnitTest).GetTypeInfo().Assembly;
            var path = AssemblyHelper.GetDirectoryPath(assembly);
            Assert.IsFalse(string.IsNullOrWhiteSpace(path));
            Assert.IsTrue(Directory.Exists(path));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LoadAssemblies_ThrowsOnEmptyInput()
        {
            AssemblyHelper.LoadAssemblies();
        }
        [TestMethod]
        public void LoadAssemblies_LoadsCurrentAssembly()
        {
            var assembly = typeof(AssemblyHelperUnitTest).GetTypeInfo().Assembly;
            var path = assembly.Location;
            var loaded = AssemblyHelper.LoadAssemblies(path);
            Assert.IsNotNull(loaded);
            Assert.IsTrue(loaded.Any(a => a.FullName == assembly.FullName));
        }
    }
}
