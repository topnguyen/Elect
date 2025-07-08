namespace Elect.Test.Core.Attributes
{
    [TestClass]
    public class CanBeNullAttributeUnitTest
    {
        // Add tests for CanBeNullAttribute here
        [TestMethod]
        public void CanInstantiate_CanBeNullAttribute()
        {
            var attr = new CanBeNullAttribute();
            Assert.IsNotNull(attr);
        }
        [TestMethod]
        public void CanBeNullAttribute_HasCorrectUsage()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(typeof(CanBeNullAttribute), typeof(AttributeUsageAttribute));
            Assert.IsNotNull(usage);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Method) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Parameter) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Property) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Delegate) != 0);
            Assert.IsTrue((usage.ValidOn & AttributeTargets.Field) != 0);
        }
    }
}
