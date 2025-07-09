namespace Elect.Test.Core.ObjUtils
{
    [TestClass]
    public class ObjHelperUnitTest
    {
        public class Dummy { public int X { get; set; } }
        [TestMethod]
        public void ToJsonString_SerializesObject()
        {
            var obj = new Dummy { X = 1 };
            var json = ObjHelper.ToJsonString(obj);
            var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Dummy>(json);
            Assert.AreEqual(1, deserialized.X);
        }
        [TestMethod]
        public void ToJsonString_SerializesJObject()
        {
            var jObj = JObject.Parse("{\"foo\":123}");
            var json = ObjHelper.ToJsonString(jObj);
            Assert.IsTrue(json.Contains("foo"));
        }
        [TestMethod]
        public void ToJsonString_Null_ReturnsNullString()
        {
            var json = ObjHelper.ToJsonString(null);
            Assert.AreEqual("null", json);
        }
        [TestMethod]
        public void Clone_ReturnsDeepCopy()
        {
            var obj = new Dummy { X = 2 };
            var clone = ObjHelper.Clone(obj);
            Assert.AreEqual(obj.X, clone.X);
            Assert.AreNotSame(obj, clone);
        }
        [TestMethod]
        public void Clone_Null_ReturnsDefault()
        {
            Dummy obj = null;
            var clone = ObjHelper.Clone(obj);
            Assert.IsNull(clone);
        }
        [TestMethod]
        public void Clone_ValueType_ReturnsSameValue()
        {
            int x = 42;
            var clone = ObjHelper.Clone(x);
            Assert.AreEqual(x, clone);
        }
        [TestMethod]
        public void ConvertTo_ConvertsType()
        {
            int value = 7;
            var str = ObjHelper.ConvertTo<string>(value);
            Assert.AreEqual("7", str);
        }
        [TestMethod]
        public void ConvertTo_Null_ReturnsDefault()
        {
            string result = ObjHelper.ConvertTo<string>(null);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ConvertTo_AlreadyTargetType_ReturnsSame()
        {
            string s = "abc";
            var result = ObjHelper.ConvertTo<string>(s);
            Assert.AreEqual(s, result);
        }
        [TestMethod]
        public void ConvertTo_IncompatibleType_ThrowsOrReturnsDefault()
        {
            // If conversion fails, should return default (null for ref types, 0 for value types)
            var result = ObjHelper.ConvertTo<int>("notanint");
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void TryConvertTo_SuccessAndFailure()
        {
            int value;
            // Success case
            bool success = ObjHelper.TryConvertTo("123", 0, out value);
            Assert.IsTrue(success);
            Assert.AreEqual(123, value);
            // Failure case
            success = ObjHelper.TryConvertTo("notanint", 42, out value);
            Assert.IsFalse(success);
            Assert.AreEqual(42, value);
        }
        [TestMethod]
        public void WithoutRefLoop_ReturnsObject()
        {
            var obj = new Dummy { X = 3 };
            var result = ObjHelper.WithoutRefLoop(obj);
            Assert.AreEqual(obj.X, result.X);
        }
        [TestMethod]
        public void WithoutRefLoop_Null_ReturnsDefault()
        {
            Dummy obj = null;
            var result = ObjHelper.WithoutRefLoop(obj);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void WithoutVirtualProp_ReturnsObject()
        {
            var obj = new Dummy { X = 4 };
            var result = ObjHelper.WithoutVirtualProp(obj);
            Assert.AreEqual(obj.X, result.X);
        }
        [TestMethod]
        public void WithoutVirtualProp_Null_ReturnsDefault()
        {
            Dummy obj = null;
            var result = ObjHelper.WithoutVirtualProp(obj);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ReplaceNullOrDefault_WorksForNullAndDefault()
        {
            // Null value
            string s = null;
            Assert.AreEqual("new", ObjHelper.ReplaceNullOrDefault(s, "new"));
            // Default value
            int i = 0;
            Assert.AreEqual(5, ObjHelper.ReplaceNullOrDefault(i, 5));
            // Non-default value
            Assert.AreEqual(7, ObjHelper.ReplaceNullOrDefault(7, 5));
        }
    }
}
