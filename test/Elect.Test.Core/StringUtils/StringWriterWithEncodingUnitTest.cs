using System.Globalization;

namespace Elect.Test.Core.StringUtils
{
    [TestClass]
    public class StringWriterWithEncodingUnitTest
    {
        [TestMethod]
        public void Constructor_Default_UsesBaseEncoding()
        {
            using var writer = new StringWriterWithEncoding();
            Assert.AreEqual(Encoding.Unicode.EncodingName, writer.Encoding.EncodingName);
        }

        [TestMethod]
        public void Constructor_WithFormatProvider_UsesBaseEncoding()
        {
            using var writer = new StringWriterWithEncoding(CultureInfo.InvariantCulture);
            Assert.AreEqual(Encoding.Unicode.EncodingName, writer.Encoding.EncodingName);
        }

        [TestMethod]
        public void Constructor_WithStringBuilder_UsesBaseEncoding()
        {
            var sb = new StringBuilder();
            using var writer = new StringWriterWithEncoding(sb);
            Assert.AreEqual(Encoding.Unicode.EncodingName, writer.Encoding.EncodingName);
        }

        [TestMethod]
        public void Constructor_WithStringBuilderAndFormatProvider_UsesBaseEncoding()
        {
            var sb = new StringBuilder();
            using var writer = new StringWriterWithEncoding(sb, CultureInfo.InvariantCulture);
            Assert.AreEqual(Encoding.Unicode.EncodingName, writer.Encoding.EncodingName);
        }

        [TestMethod]
        public void Constructor_WithEncoding_UsesProvidedEncoding()
        {
            var encoding = Encoding.UTF8;
            using var writer = new StringWriterWithEncoding(encoding);
            Assert.AreEqual(encoding, writer.Encoding);
        }

        [TestMethod]
        public void Constructor_WithFormatProviderAndEncoding_UsesProvidedEncoding()
        {
            var encoding = Encoding.UTF8;
            using var writer = new StringWriterWithEncoding(CultureInfo.InvariantCulture, encoding);
            Assert.AreEqual(encoding, writer.Encoding);
        }

        [TestMethod]
        public void Constructor_WithStringBuilderAndEncoding_UsesProvidedEncoding()
        {
            var sb = new StringBuilder();
            var encoding = Encoding.UTF8;
            using var writer = new StringWriterWithEncoding(sb, encoding);
            Assert.AreEqual(encoding, writer.Encoding);
        }

        [TestMethod]
        public void Constructor_WithStringBuilderFormatProviderAndEncoding_UsesProvidedEncoding()
        {
            var sb = new StringBuilder();
            var encoding = Encoding.UTF8;
            using var writer = new StringWriterWithEncoding(sb, CultureInfo.InvariantCulture, encoding);
            Assert.AreEqual(encoding, writer.Encoding);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullEncoding_ThrowsArgumentNullException()
        {
            new StringWriterWithEncoding((Encoding)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithFormatProviderAndNullEncoding_ThrowsArgumentNullException()
        {
            new StringWriterWithEncoding(CultureInfo.InvariantCulture, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithStringBuilderAndNullEncoding_ThrowsArgumentNullException()
        {
            var sb = new StringBuilder();
            new StringWriterWithEncoding(sb, (Encoding)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithStringBuilderFormatProviderAndNullEncoding_ThrowsArgumentNullException()
        {
            var sb = new StringBuilder();
            new StringWriterWithEncoding(sb, CultureInfo.InvariantCulture, null);
        }

        [TestMethod]
        public void Write_WithDifferentEncodings_WritesCorrectly()
        {
            var testString = "Hello, 世界!";
            
            using var utf8Writer = new StringWriterWithEncoding(Encoding.UTF8);
            utf8Writer.Write(testString);
            Assert.AreEqual(testString, utf8Writer.ToString());
            
            using var asciiWriter = new StringWriterWithEncoding(Encoding.ASCII);
            asciiWriter.Write(testString);
            Assert.AreEqual(testString, asciiWriter.ToString());
        }

        [TestMethod]
        public void Encoding_Property_ReturnsCorrectEncoding()
        {
            var encodings = new[] { Encoding.UTF8, Encoding.ASCII, Encoding.UTF32 };

            foreach (var encoding in encodings)
            {
                using var writer = new StringWriterWithEncoding(encoding);
                Assert.AreEqual(encoding, writer.Encoding);
            }
        }

        [TestMethod]
        public void ToString_WithCustomEncoding_ReturnsWrittenContent()
        {
            const string testContent = "Test content with special chars: ñáéíóú";
            var encoding = Encoding.UTF8;
            
            using var writer = new StringWriterWithEncoding(encoding);
            writer.Write(testContent);
            
            Assert.AreEqual(testContent, writer.ToString());
        }

        [TestMethod]
        public void Encoding_WithoutCustomEncoding_ReturnsBaseEncoding()
        {
            using var defaultWriter = new StringWriterWithEncoding();
            Assert.AreEqual(Encoding.Unicode.EncodingName, defaultWriter.Encoding.EncodingName);
            
            using var formatProviderWriter = new StringWriterWithEncoding(CultureInfo.InvariantCulture);
            Assert.AreEqual(Encoding.Unicode.EncodingName, formatProviderWriter.Encoding.EncodingName);
        }
    }
}