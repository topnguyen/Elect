using System;
using System.IO;
using IronOcr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Core
{
    [TestClass]
    public class OCRUnitTest
    {
        [TestMethod]
        public void OCRCase()
        {
            var ocr = new AutoOcr();

            try
            {
                var fileInfo = new FileInfo(@"OCR_Sample.pdf");
                var result = ocr.Read(fileInfo.FullName);
                Console.WriteLine(result.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
          
            Console.ReadLine();
        }
    }
}