namespace Elect.Test.Core.AssemblyUtils
{
    [TestClass]
    public class AssemblyLoaderUnitTest
    {
        [TestMethod]
        public void AssemblyLoader_LoadsAssemblySuccessfully_ViaAssemblyHelper()
        {
            // Test the functionality through the public AssemblyHelper API
            var currentAssembly = typeof(AssemblyLoaderUnitTest).Assembly;
            var assemblyPath = currentAssembly.Location;
            
            var loadedAssemblies = AssemblyHelper.LoadAssemblies(assemblyPath);
            
            Assert.IsNotNull(loadedAssemblies);
            Assert.IsTrue(loadedAssemblies.Count > 0);
            Assert.IsTrue(loadedAssemblies.Any(a => a.FullName == currentAssembly.FullName));
        }
        [TestMethod]
        public void AssemblyLoader_HandlesMultipleLoadsCorrectly_ViaAssemblyHelper()
        {
            // Test loading the same assembly multiple times through AssemblyHelper
            var currentAssembly = typeof(AssemblyLoaderUnitTest).Assembly;
            var assemblyPath = currentAssembly.Location;
            
            // Load the assembly multiple times
            var firstLoad = AssemblyHelper.LoadAssemblies(assemblyPath);
            var secondLoad = AssemblyHelper.LoadAssemblies(assemblyPath);
            
            Assert.IsNotNull(firstLoad);
            Assert.IsNotNull(secondLoad);
            Assert.IsTrue(firstLoad.Count > 0);
            Assert.IsTrue(secondLoad.Count > 0);
            
            // Both loads should succeed and reference assemblies with the same name
            Assert.AreEqual(firstLoad.First().FullName, secondLoad.First().FullName);
        }
    }
}
