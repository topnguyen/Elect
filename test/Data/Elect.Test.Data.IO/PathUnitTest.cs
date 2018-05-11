using Elect.Data.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Data.IO
{
    [TestClass]
    public class PathUnitTest
    {
        [TestMethod]
        public void GetFullPath()
        {
            var pathRelative = "";

            var fullPath = PathHelper.GetFullPath(pathRelative);

            Assert.IsNotNull(fullPath);
        }
    }
}