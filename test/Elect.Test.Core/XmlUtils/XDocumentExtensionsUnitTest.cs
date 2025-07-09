namespace Elect.Test.Core.XmlUtils
{
    [TestClass]
    public class XDocumentExtensionsUnitTest
    {
        [TestMethod]
        public void CanUseXDocumentExtensions()
        {
            var doc = new XDocument(new XElement("root", new XElement("child", "value")));
            // Add your extension method tests here if any exist
            Assert.IsNotNull(doc);
        }

        [TestMethod]
        public void ToString_WithEncoding_ReturnsXmlStringWithEncodingDeclaration()
        {
            var doc = new XDocument(new XElement("root", new XElement("child", "value")));
            var xml = doc.ToString(Encoding.UTF8);
            Assert.IsTrue(xml.Contains("<root>"));
            Assert.IsTrue(xml.Contains("<child>value</child>"));
            Assert.IsTrue(xml.Contains("encoding=\"utf-8\""));
        }

        [TestMethod]
        public void ToString_WithDifferentEncodings_ProducesCorrectDeclaration()
        {
            var doc = new XDocument(new XElement("root"));
            var xmlUtf32 = doc.ToString(Encoding.UTF32);
            Assert.IsTrue(xmlUtf32.Contains("encoding=\"utf-32\""));
            var xmlUnicode = doc.ToString(Encoding.Unicode);
            Assert.IsTrue(xmlUnicode.Contains("encoding=\"utf-16\""));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_NullDocument_Throws()
        {
            XDocument doc = null;
            doc.ToString(Encoding.UTF8);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToString_NullEncoding_Throws()
        {
            var doc = new XDocument(new XElement("root"));
            doc.ToString(null);
        }
    }
}
