using System;
using System.IO;
using Elect.Data.IO;
using Elect.Data.IO.ImageUtils.CompressUtils;
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

        [TestMethod]
        public void Compress()
        {
            var input2Path = "/Users/top/Downloads/Test File 2.png";
            var input3Path = "/Users/top/Downloads/Test File 3.JPG";
            var input4Path = "/Users/top/Downloads/Test File 4.JPG";
            var input5Path = "/Users/top/Downloads/Test File 5.gif";

            // ImageCompressor.Compress(input2Path, "/Users/top/Downloads/Test File 2 Compressed.png");
            ImageCompressor.Compress(input3Path, "/Users/top/Downloads/Test File 3 Compressed.JPG");
            // ImageCompressor.Compress(input5Path, "/Users/top/Downloads/Test File 5 Compressed.gif");
        }
    }
}