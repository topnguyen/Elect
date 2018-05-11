using Elect.Core.DictionaryUtils;
using Elect.Data.IO;
using Elect.Data.IO.DirectoryUtils;
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