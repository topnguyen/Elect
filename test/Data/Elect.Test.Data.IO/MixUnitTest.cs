namespace Elect.Test.Data.IO
{
    [TestClass]
    public class MixUnitTest
    {
        [TestMethod]
        public void GetFullPathTestCase()
        {
            var pathRelative = "";
            var fullPath = PathHelper.GetFullPath(pathRelative);
            Assert.IsNotNull(fullPath);
        }
    }
}
