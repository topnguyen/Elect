#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> MixUnitTest.cs </Name>
//         <Created> 02/04/2018 9:01:18 PM </Created>
//         <Key> 725d0671-f4a3-4600-bb74-ff2fbb85932d </Key>
//     </File>
//     <Summary>
//         MixUnitTest.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO.ImageUtils.CompressUtils;
using Elect.Data.IO.ImageUtils.ResizeUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Elect.Test.UnitTest
{
    [TestClass]
    public class MixUnitTest
    {
        [TestMethod]
        public void ImageCompressorUnitTest()
        {
            ImageCompressor.Compress(@"D:\Collection Image\1.jpg", @"D:\_Temp\1.jpg");
            ImageCompressor.Compress(@"D:\Collection Image\toptal-blog-1_B.png", @"D:\_Temp\toptal-blog-1_B.png");
            ImageCompressor.Compress(@"D:\Collection Image\toptal-blog-image-1387828457247.gif", @"D:\_Temp\toptal-blog-image-1387828457247.gif");
        }

        [TestMethod]
        public void ResizeUnitTest()
        {
            var resizedImage = ImageResizeHelper.Resize(File.ReadAllBytes(@"D:\Collection Image\1.jpg"), 100, 100);

            File.WriteAllBytes(@"D:\_Temp\1.jpg", resizedImage);
        }
    }
}