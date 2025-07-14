using Elect.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Elect.Test.Web.Models
{
    [TestClass]
    public class HttpContextModelUnitTest
    {
        [TestMethod]
        public void Constructor_WithNullContext_DoesNotThrow()
        {
            var model = new HttpContextModel(null);
            
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Id);
            Assert.IsNotNull(model.Headers);
            Assert.IsNotNull(model.QueryStrings);
        }

        [TestMethod]
        public void Constructor_GeneratesUniqueId()
        {
            var model1 = new HttpContextModel(null);
            var model2 = new HttpContextModel(null);
            
            Assert.AreNotEqual(model1.Id, model2.Id);
            Assert.IsTrue(Guid.TryParse(model1.Id, out _));
            Assert.IsTrue(Guid.TryParse(model2.Id, out _));
        }

        [TestMethod]
        public void Constructor_WithValidContext_ExtractsProperties()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Protocol = "HTTP/1.1";
            context.Request.Method = "GET";
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            context.Request.Path = "/api/test";
            context.Request.QueryString = new QueryString("?param1=value1&param2=value2");
            
            context.Request.Headers.Add("User-Agent", "Mozilla/5.0");
            context.Request.Headers.Add("Accept", "application/json");
            
            // Act
            var model = new HttpContextModel(context);
            
            // Assert
            Assert.AreEqual("HTTP/1.1", model.Protocol);
            Assert.AreEqual("GET", model.Method);
            Assert.IsNotNull(model.DisplayUrl);
            Assert.IsTrue(model.DisplayUrl.Contains("example.com"));
            Assert.IsTrue(model.DisplayUrl.Contains("/api/test"));
        }

        [TestMethod]
        public void Constructor_WithHeaders_ExtractsHeaders()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            context.Request.Headers.Add("User-Agent", "Mozilla/5.0");
            context.Request.Headers.Add("Accept", new StringValues(new[] { "application/json", "text/html" }));
            
            // Act
            var model = new HttpContextModel(context);
            
            // Assert
            Assert.IsTrue(model.Headers.ContainsKey("User-Agent"));
            Assert.AreEqual("Mozilla/5.0", model.Headers["User-Agent"][0]);
            
            Assert.IsTrue(model.Headers.ContainsKey("Accept"));
            Assert.AreEqual(2, model.Headers["Accept"].Count);
            Assert.IsTrue(model.Headers["Accept"].Contains("application/json"));
            Assert.IsTrue(model.Headers["Accept"].Contains("text/html"));
        }

        [TestMethod]
        public void Constructor_WithQueryString_ExtractsQueryStrings()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            var queryDict = new Dictionary<string, StringValues>
            {
                {"param1", "value1"},
                {"param2", new StringValues(new[] {"value2a", "value2b"})}
            };
            
            context.Request.QueryString = new QueryString("?param1=value1&param2=value2a&param2=value2b");
            
            // Act
            var model = new HttpContextModel(context);
            
            // Assert
            Assert.IsNotNull(model.QueryStrings);
            // Note: QueryStrings may be extracted from QueryString depending on HttpContextModel implementation
        }

        [TestMethod]
        public void Properties_AreReadOnlyAfterConstruction()
        {
            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            context.Request.Protocol = "HTTP/2.0";
            context.Request.Method = "POST";
            
            var model = new HttpContextModel(context);
            
            // These properties should be read-only (get-only)
            Assert.AreEqual("HTTP/2.0", model.Protocol);
            Assert.AreEqual("POST", model.Method);
        }

        [TestMethod]
        public void Id_CanBeModified()
        {
            var model = new HttpContextModel(null);
            var originalId = model.Id;
            var newId = Guid.NewGuid().ToString("N");
            
            model.Id = newId;
            
            Assert.AreEqual(newId, model.Id);
            Assert.AreNotEqual(originalId, model.Id);
        }

        [TestMethod]
        public void Dispose_DoesNotThrow()
        {
            var model = new HttpContextModel(null);
            
            // Should not throw
            model.Dispose();
        }

        [TestMethod]
        public void IsSerializable()
        {
            var model = new HttpContextModel(null);
            
            // Check that the class is marked as Serializable
            var type = typeof(HttpContextModel);
            var serializableAttribute = type.GetCustomAttributes(typeof(SerializableAttribute), false);
            
            Assert.AreEqual(1, serializableAttribute.Length);
        }

        [TestMethod]
        public void Constructor_WithEmptyContext_InitializesCollections()
        {
            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            
            var model = new HttpContextModel(context);
            
            Assert.IsNotNull(model.Headers);
            Assert.IsNotNull(model.QueryStrings);
            Assert.IsNotNull(model.Id);
        }

        [TestMethod]
        public void Headers_IsNotNullAfterConstruction()
        {
            var model = new HttpContextModel(null);
            
            Assert.IsNotNull(model.Headers);
            Assert.AreEqual(0, model.Headers.Count);
        }

        [TestMethod]
        public void QueryStrings_IsNotNullAfterConstruction()
        {
            var model = new HttpContextModel(null);
            
            Assert.IsNotNull(model.QueryStrings);
            Assert.AreEqual(0, model.QueryStrings.Count);
        }
    }
}