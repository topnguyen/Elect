namespace Elect.Test.Web.Api.Models
{
    [TestClass]
    public class LinkModelUnitTest
    {
        [TestMethod]
        public void Url_GetSet_WorksCorrectly()
        {
            var link = new LinkModel();
            var url = "https://example.com/test";
            
            link.Url = url;
            
            Assert.AreEqual(url, link.Url);
        }

        [TestMethod]
        public void Method_GetSet_WorksCorrectly()
        {
            var link = new LinkModel();
            var method = HttpMethod.POST;
            
            link.Method = method;
            
            Assert.AreEqual(method, link.Method);
        }

        [TestMethod]
        public void Data_GetSet_WorksCorrectly()
        {
            var link = new LinkModel();
            var data = new RouteValueDictionary { { "id", 123 }, { "name", "test" } };
            
            link.Data = data;
            
            Assert.AreEqual(data, link.Data);
            Assert.AreEqual(123, link.Data["id"]);
            Assert.AreEqual("test", link.Data["name"]);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var link = new LinkModel();
            
            Assert.IsNull(link.Url);
            Assert.AreEqual(HttpMethod.GET, link.Method);
            Assert.IsNotNull(link.Data);
            Assert.AreEqual(0, link.Data.Count);
        }

        [TestMethod]
        public void Method_CanBeSetToAllHttpMethods()
        {
            var link = new LinkModel();
            
            link.Method = HttpMethod.GET;
            Assert.AreEqual(HttpMethod.GET, link.Method);
            
            link.Method = HttpMethod.POST;
            Assert.AreEqual(HttpMethod.POST, link.Method);
            
            link.Method = HttpMethod.PUT;
            Assert.AreEqual(HttpMethod.PUT, link.Method);
            
            link.Method = HttpMethod.DELETE;
            Assert.AreEqual(HttpMethod.DELETE, link.Method);
            
            link.Method = HttpMethod.PATCH;
            Assert.AreEqual(HttpMethod.PATCH, link.Method);
            
            link.Method = HttpMethod.HEAD;
            Assert.AreEqual(HttpMethod.HEAD, link.Method);
            
            link.Method = HttpMethod.OPTIONS;
            Assert.AreEqual(HttpMethod.OPTIONS, link.Method);
        }

        [TestMethod]
        public void Data_CanBeModified()
        {
            var link = new LinkModel();
            
            link.Data["key1"] = "value1";
            link.Data["key2"] = 42;
            
            Assert.AreEqual("value1", link.Data["key1"]);
            Assert.AreEqual(42, link.Data["key2"]);
            Assert.AreEqual(2, link.Data.Count);
        }

        [TestMethod]
        public void InheritsFromElectDisposableModel()
        {
            var link = new LinkModel();
            
            Assert.IsInstanceOfType(link, typeof(ElectDisposableModel));
        }

        [TestMethod]
        public void Url_CanBeEmpty()
        {
            var link = new LinkModel();
            
            link.Url = string.Empty;
            
            Assert.AreEqual(string.Empty, link.Url);
        }

        [TestMethod]
        public void Url_CanBeWhitespace()
        {
            var link = new LinkModel();
            var whitespaceUrl = "   ";
            
            link.Url = whitespaceUrl;
            
            Assert.AreEqual(whitespaceUrl, link.Url);
        }

        [TestMethod]
        public void Data_CanBeSetToNull()
        {
            var link = new LinkModel();
            
            link.Data = null;
            
            Assert.IsNull(link.Data);
        }
    }
}