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
        public void Clone_ReturnsDeepCopy()
        {
            var obj = new Dummy { X = 2 };
            var clone = ObjHelper.Clone(obj);
            Assert.AreEqual(obj.X, clone.X);
            Assert.AreNotSame(obj, clone);
        }
        [TestMethod]
        public void ConvertTo_ConvertsType()
        {
            int value = 7;
            var str = ObjHelper.ConvertTo<string>(value);
            Assert.AreEqual("7", str);
        }
        [TestMethod]
        public void WithoutRefLoop_ReturnsObject()
        {
            var obj = new Dummy { X = 3 };
            var result = ObjHelper.WithoutRefLoop(obj);
            Assert.AreEqual(obj.X, result.X);
        }
        [TestMethod]
        public void WithoutVirtualProp_ReturnsObject()
        {
            var obj = new Dummy { X = 4 };
            var result = ObjHelper.WithoutVirtualProp(obj);
            Assert.AreEqual(obj.X, result.X);
        }
    }
}
