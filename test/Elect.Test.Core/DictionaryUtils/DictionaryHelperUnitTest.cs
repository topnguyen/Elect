namespace Elect.Test.Core.DictionaryUtils
{
    public class TestObj
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
    [TestClass]
    public class DictionaryHelperUnitTest
    {
        [TestMethod]
        public void ToDictionary_Object_ReturnsDictionary()
        {
            var obj = new TestObj { Name = "Test", Age = 30 };
            var dict = DictionaryHelper.ToDictionary(obj);
            Console.WriteLine("Keys: " + string.Join(", ", dict.Keys));
            CollectionAssert.Contains(dict.Keys, "Age");
            Assert.AreEqual("Test", dict["Name"].ToString());
            Assert.AreEqual(30, Convert.ToInt32(dict["Age"]));
        }
        [TestMethod]
        public void ToDictionary_Generic_ReturnsDictionary()
        {
            var obj = new TestObj { Name = "Test", Age = 30 };
            var dict = DictionaryHelper.ToDictionary(obj);
            CollectionAssert.Contains(dict.Keys, "Age");
            Assert.AreEqual("Test", dict["Name"]);
            Assert.AreEqual("30", dict["Age"]);
        }
        [TestMethod]
        public void GetValue_ExistingKey_ReturnsValue()
        {
            var dict = new Dictionary<string, object> { { "key", 123 } };
            var value = DictionaryHelper.GetValue<int>(dict, "key");
            Assert.AreEqual(123, value);
        }
        [TestMethod]
        public void GetValue_NonExistingKey_ReturnsDefault()
        {
            var dict = new Dictionary<string, object>();
            var value = DictionaryHelper.GetValue<int>(dict, "key");
            Assert.AreEqual(0, value);
        }
        [TestMethod]
        public void GetValue_WithDefault_ReturnsDefaultIfMissing()
        {
            var dict = new Dictionary<string, object>();
            var value = DictionaryHelper.GetValue(dict, "key", 42);
            Assert.AreEqual(42, value);
        }
        [TestMethod]
        public void AddOrUpdate_AddsAndUpdates()
        {
            var dict = new Dictionary<string, int>();
            DictionaryHelper.AddOrUpdate(dict, "a", 1);
            Assert.AreEqual(1, dict["a"]);
            DictionaryHelper.AddOrUpdate(dict, "a", 2);
            Assert.AreEqual(2, dict["a"]);
        }
        [TestMethod]
        public void ToDictionary_Object_ReturnsDictionary_WithFailureOnMissingAge()
        {
            var obj = new TestObj { Name = "Test", Age = 30 };
            var dict = DictionaryHelper.ToDictionary(obj);
            Console.WriteLine("Keys: " + string.Join(", ", dict.Keys));
            CollectionAssert.Contains(dict.Keys, "Age");
            if (!dict.ContainsKey("Age"))
            {
                Assert.Fail($"Keys: {string.Join(", ", dict.Keys)}; Values: {string.Join(", ", dict.Values)}");
            }
            Assert.AreEqual("Test", dict["Name"].ToString());
            Assert.AreEqual(30, Convert.ToInt32(dict["Age"]));
        }
    }
}
