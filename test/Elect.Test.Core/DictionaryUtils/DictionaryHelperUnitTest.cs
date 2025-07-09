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
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToDictionary_Object_Null_Throws()
        {
            DictionaryHelper.ToDictionary((object)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToDictionary_Generic_Null_Throws()
        {
            DictionaryHelper.ToDictionary<TestObj>(null);
        }

        public class ComplexObj
        {
            public string Name { get; set; }
            public int? Age { get; set; }
            public DateTime? Date { get; set; }
            public TestEnum? EnumValue { get; set; }
            public TestObj Nested { get; set; }
        }
        public enum TestEnum { A, B }

        [TestMethod]
        public void ToDictionary_Object_ComplexTypes()
        {
            var now = DateTime.UtcNow;
            var obj = new ComplexObj
            {
                Name = null,
                Age = 5,
                Date = now,
                EnumValue = TestEnum.B,
                Nested = new TestObj { Name = "Nested", Age = 1 }
            };
            var dict = DictionaryHelper.ToDictionary(obj);
            // 'Name' is null, so it may not be present in the dictionary
            Assert.IsTrue(dict.ContainsKey("Name") ? dict["Name"] == null : true);
            Assert.AreEqual(5, int.Parse(dict["Age"].ToString()));
            // Compare DateTime string representations to avoid millisecond/format issues
            Assert.AreEqual(now.ToString(), DateTime.Parse(dict["Date"].ToString()).ToString());
            Assert.AreEqual(TestEnum.B.ToString(), dict["EnumValue"].ToString());
            Assert.IsNotNull(dict["Nested"]);
        }

        [TestMethod]
        public void ToDictionary_Generic_ComplexTypes()
        {
            var now = DateTime.UtcNow;
            var obj = new ComplexObj
            {
                Name = null,
                Age = 5,
                Date = now,
                EnumValue = TestEnum.B,
                Nested = new TestObj { Name = "Nested", Age = 1 }
            };
            var dict = DictionaryHelper.ToDictionary(obj);
            Assert.IsTrue(dict.ContainsKey("Name") ? dict["Name"] == null : true);
            Assert.AreEqual("5", dict["Age"]);
            Assert.AreEqual(now.ToString(), dict["Date"]);
            Assert.AreEqual(TestEnum.B.ToString(), dict["EnumValue"]);
            // Nested is serialized as JSON string, not as TestObj.ToString()
            var nestedJson = Newtonsoft.Json.JsonConvert.SerializeObject(obj.Nested, new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
            Assert.AreEqual(nestedJson, dict["Nested"]);
        }

        public class EmptyObj { }
        [TestMethod]
        public void ToDictionary_EmptyObject_ReturnsEmptyDictionary()
        {
            var obj = new EmptyObj();
            var dict = DictionaryHelper.ToDictionary(obj);
            Assert.AreEqual(0, dict.Count);
        }
    }
}
