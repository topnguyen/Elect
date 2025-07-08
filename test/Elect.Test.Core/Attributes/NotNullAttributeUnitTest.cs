namespace Elect.Test.Core.Attributes
{
    [TestClass]
    public class NotNullAttributeUnitTest
    {
        [TestMethod]
        public void CanInstantiate_NotNullAttribute()
        {
            var attr = new NotNullAttribute();
            Assert.IsNotNull(attr);
        }
        [TestMethod]
        public void NotNullAttribute_HasCorrectUsage()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(typeof(NotNullAttribute), typeof(AttributeUsageAttribute));
            Assert.IsNotNull(usage);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Method) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Parameter) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Property) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Delegate) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Field) != 0);
        }
    }
}
