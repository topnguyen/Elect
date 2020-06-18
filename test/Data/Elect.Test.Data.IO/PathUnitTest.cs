using System;
using System.IO;
using Elect.Data.IO;
using Elect.Data.IO.ImageUtils.CompressUtils;
using ImageMagick;
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
            var input1Path = "/Users/top/Downloads/Test File 1.png";
            var input2Path = "/Users/top/Downloads/Test File 2.png";
            var input3Path = "/Users/top/Downloads/Test File 3.JPG";
            var input4Path = "/Users/top/Downloads/Test File 4.JPG";

            // var optimizer = new ImageOptimizer();
            //
            // optimizer.LosslessCompress(input1Path);
            //
            // optimizer.LosslessCompress(input3Path);
            //
            // optimizer.Compress(input2Path);
            //
            // optimizer.Compress(input4Path);

            using var image = new MagickImage(input3Path)
            {
                Quality = 75,
                Format = MagickFormat.Jpg
            };

            image.Strip();
            
            image.Write("/Users/top/Downloads/Test File 1 Compressed.jpg");
        }
    }
}