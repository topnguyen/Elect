namespace Elect.Test.Core.AssemblyUtils
{
    [TestClass]
    public class AssemblyLoaderUnitTest
    {
        [TestMethod]
        [Ignore("Direct reflection-based construction of AssemblyLoader (inherits AssemblyLoadContext) is not supported on this runtime. Covered via AssemblyHelper public API.")]
        public void AssemblyLoader_LoadsAssemblySuccessfully()
        {
            // Skipped: See reason above.
        }
        [TestMethod]
        [Ignore("Direct reflection-based construction of AssemblyLoader (inherits AssemblyLoadContext) is not supported on this runtime. Covered via AssemblyHelper public API.")]
        public void AssemblyLoader_ReturnsNullIfAlreadyLoaded()
        {
            // Skipped: See reason above.
        }
    }
}
