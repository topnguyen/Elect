namespace Elect.Test.Core.DictionaryUtils
{
    [TestClass]
    public class DirectoryExtensionsUnitTest
    {
        private class TestObj
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        [TestMethod]
        public void ToDictionary_Object_ReturnsDictionary()
        {
            var obj = new TestObj { Name = "Test", Age = 30 };
            var dict = obj.ToDictionary();
            CollectionAssert.Contains(dict.Keys, "Age");
            Assert.AreEqual("Test", dict["Name"].ToString());
            Assert.AreEqual(30, Convert.ToInt32(dict["Age"]));
        }
        [TestMethod]
        public void ToDictionary_Generic_ReturnsDictionary()
        {
            var obj = new TestObj { Name = "Test", Age = 30 };
            var dict = obj.ToDictionary();
            CollectionAssert.Contains(dict.Keys, "Age");
            Assert.AreEqual("Test", dict["Name"].ToString());
            Assert.AreEqual(30, Convert.ToInt32(dict["Age"]));
        }
        [TestMethod]
        public void GetValue_ExistingKey_ReturnsValue()
        {
            var dict = new Dictionary<string, object> { { "key", 123 } };
            var value = DirectoryExtensions.GetValue<int>(dict, "key");
            Assert.AreEqual(123, value);
        }
        [TestMethod]
        public void GetValue_NonExistingKey_ReturnsDefault()
        {
            var dict = new Dictionary<string, object>();
            var value = DirectoryExtensions.GetValue<int>(dict, "key");
            Assert.AreEqual(0, value);
        }
        [TestMethod]
        public void GetValue_WithDefault_ReturnsDefaultIfMissing()
        {
            var dict = new Dictionary<string, object>();
            var value = DirectoryExtensions.GetValue(dict, "key", 42);
            Assert.AreEqual(42, value);
        }
        [TestMethod]
        public void AddOrUpdate_AddsAndUpdates()
        {
            var dict = new Dictionary<string, int>();
            dict.AddOrUpdate("a", 1);
            Assert.AreEqual(1, dict["a"]);
            dict.AddOrUpdate("a", 2);
            Assert.AreEqual(2, dict["a"]);
        }
        [TestMethod]
        public void ToDictionary_Object_MissingAgeKey_FailsTest()
        {
            var obj = new TestObj { Name = "Test" };
            var dict = obj.ToDictionary();
            if (!dict.ContainsKey("Age"))
            {
                Assert.Fail($"Keys: {string.Join(", ", dict.Keys)}; Values: {string.Join(", ", dict.Values)}");
            }
        }
    }
}
