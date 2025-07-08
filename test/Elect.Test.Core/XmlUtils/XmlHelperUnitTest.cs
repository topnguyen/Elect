namespace Elect.Test.Core.XmlUtils
{
    [TestClass]
    public class XmlHelperUnitTest
    {
        public class Dummy
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        [TestMethod]
        public void ToXmlString_And_FromXmlString_RoundTrip()
        {
            var obj = new Dummy { Id = 123, Name = "TestName" };
            var xml = XmlHelper.ToXmlString(obj);
            var result = XmlHelper.FromXmlString<Dummy>(xml);
            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
        }
        [TestMethod]
        public void ToXmlString_WithNamespaceOption()
        {
            var obj = new Dummy { Id = 1, Name = "A" };
            var xml = XmlHelper.ToXmlString(obj, isRemoveNameSpace: false);
            Assert.IsTrue(xml.Contains("<Id>1</Id>"));
            Assert.IsTrue(xml.Contains("<Name>A</Name>"));
        }
        [TestMethod]
        public void FromXmlString_ThrowsOnInvalidXml()
        {
            Assert.ThrowsException<InvalidOperationException>(() => XmlHelper.FromXmlString<Dummy>("<notxml>"));
        }
    }
}
