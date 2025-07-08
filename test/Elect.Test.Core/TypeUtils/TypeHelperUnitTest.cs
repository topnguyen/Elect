namespace Elect.Test.Core.TypeUtils
{
    [TestClass]
    public class TypeHelperUnitTest
    {
        [TestMethod]
        public void GetNotNullableType_ReturnsUnderlyingType_ForNullable()
        {
            var type = typeof(int?);
            var notNullable = TypeHelper.GetNotNullableType(type);
            Assert.AreEqual(typeof(int), notNullable);
        }
        [TestMethod]
        public void GetNotNullableType_ReturnsSameType_ForNonNullable()
        {
            var type = typeof(double);
            var notNullable = TypeHelper.GetNotNullableType(type);
            Assert.AreEqual(typeof(double), notNullable);
        }
        [TestMethod]
        public void IsNullableType_ReturnsTrue_ForNullable()
        {
            Assert.IsTrue(TypeHelper.IsNullableType(typeof(decimal?)));
        }
        [TestMethod]
        public void IsNullableType_ReturnsFalse_ForNonNullable()
        {
            Assert.IsFalse(TypeHelper.IsNullableType(typeof(decimal)));
        }
        [TestMethod]
        public void IsNumericType_ReturnsTrue_ForNumericTypes()
        {
            Assert.IsTrue(TypeHelper.IsNumericType(typeof(int)));
            Assert.IsTrue(TypeHelper.IsNumericType(typeof(double?)));
            Assert.IsTrue(TypeHelper.IsNumericType(typeof(decimal)));
            Assert.IsTrue(TypeHelper.IsNumericType(typeof(long?)));
        }
        [TestMethod]
        public void IsNumericType_ReturnsFalse_ForNonNumericTypes()
        {
            Assert.IsFalse(TypeHelper.IsNumericType(typeof(string)));
            Assert.IsFalse(TypeHelper.IsNumericType(typeof(DateTime?)));
            Assert.IsFalse(TypeHelper.IsNumericType(typeof(object)));
            Assert.IsFalse(TypeHelper.IsNumericType(typeof(TypeHelperUnitTest)));
        }
    }
}
