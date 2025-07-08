namespace Elect.Test.Core.ObjUtils
{
    [TestClass]
    public class ObjExtensionsUnitTest
    {
        public class Dummy { public int X { get; set; } }
        [TestMethod]
        public void ToJsonString_ReturnsJson()
        {
            var obj = new Dummy { X = 5 };
            var json = obj.ToJsonString();
            var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Dummy>(json);
            Assert.AreEqual(5, deserialized.X);
        }
        [TestMethod]
        public void Clone_ReturnsDeepCopy()
        {
            var obj = new Dummy { X = 5 };
            var clone = obj.Clone();
            Assert.AreEqual(obj.X, clone.X);
            Assert.AreNotSame(obj, clone);
        }
        [TestMethod]
        public void ConvertTo_ConvertsType()
        {
            int value = 42;
            var str = value.ConvertTo<string>();
            Assert.AreEqual("42", str);
        }
        [TestMethod]
        public void WithoutRefLoop_ReturnsObject()
        {
            var obj = new Dummy { X = 5 };
            var result = obj.WithoutRefLoop();
            Assert.AreEqual(obj.X, result.X);
        }
        [TestMethod]
        public void WithoutVirtualProp_ReturnsObject()
        {
            var obj = new Dummy { X = 5 };
            var result = obj.WithoutVirtualProp();
            Assert.AreEqual(obj.X, result.X);
        }
    }
}
