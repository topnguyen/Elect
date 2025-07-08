namespace Elect.Test.Core.TypeUtils
{
    [TestClass]
    public class TypeExtensionsUnitTest
    {
        [TestMethod]
        public void GetNotNullableType_Extension_ReturnsUnderlyingType()
        {
            Assert.AreEqual(typeof(int), typeof(int?).GetNotNullableType());
        }
        [TestMethod]
        public void IsNullableType_Extension_ReturnsCorrectly()
        {
            Assert.IsTrue(typeof(double?).IsNullableType());
            Assert.IsFalse(typeof(double).IsNullableType());
        }
        [TestMethod]
        public void IsNumericType_Extension_ReturnsCorrectly()
        {
            Assert.IsTrue(typeof(decimal).IsNumericType());
            Assert.IsFalse(typeof(string).IsNumericType());
        }
        [TestMethod]
        public void IsGenericType_ReturnsTrueForGeneric()
        {
            Assert.IsTrue(typeof(System.Collections.Generic.List<int>).IsGenericType(typeof(System.Collections.Generic.List<>)));
            Assert.IsFalse(typeof(int).IsGenericType(typeof(System.Collections.Generic.List<>)));
        }
        [TestMethod]
        public void IsImplementGenericInterface_ReturnsTrueForImplemented()
        {
            Assert.IsTrue(typeof(System.Collections.Generic.List<int>).IsImplementGenericInterface(typeof(System.Collections.Generic.IEnumerable<>)));
            Assert.IsFalse(typeof(int).IsImplementGenericInterface(typeof(System.Collections.Generic.IEnumerable<>)));
        }
    }
}
