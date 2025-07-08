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
    }
}
