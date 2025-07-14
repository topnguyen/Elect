namespace Elect.Test.DI
{
    [TestClass]
    public class AttributesUnitTest
    {
        [TestMethod]
        public void ScopedDependencyAttribute_ConstructorWorks()
        {
            var attribute = new ScopedDependencyAttribute();
            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void TransientDependencyAttribute_ConstructorWorks()
        {
            var attribute = new TransientDependencyAttribute();
            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void ScopedDependencyAttribute_InheritsFromDependencyAttribute()
        {
            var attribute = new ScopedDependencyAttribute();
            Assert.IsInstanceOfType(attribute, typeof(DependencyAttribute));
        }

        [TestMethod]
        public void TransientDependencyAttribute_InheritsFromDependencyAttribute()
        {
            var attribute = new TransientDependencyAttribute();
            Assert.IsInstanceOfType(attribute, typeof(DependencyAttribute));
        }

        [TestMethod]
        public void ScopedDependencyAttribute_CanBeAppliedToClasses()
        {
            var type = typeof(ScopedTestService);
            var attributes = type.GetCustomAttributes(typeof(ScopedDependencyAttribute), false);
            Assert.AreEqual(1, attributes.Length);
        }

        [TestMethod]
        public void TransientDependencyAttribute_CanBeAppliedToClasses()
        {
            var type = typeof(TransientTestService);
            var attributes = type.GetCustomAttributes(typeof(TransientDependencyAttribute), false);
            Assert.AreEqual(1, attributes.Length);
        }

        [TestMethod]
        public void ScopedDependencyAttribute_CanBeAppliedToInterfaces()
        {
            var type = typeof(ScopedTestService);
            var attributes = type.GetCustomAttributes(typeof(ScopedDependencyAttribute), false);
            Assert.AreEqual(1, attributes.Length);
        }

        [TestMethod]
        public void TransientDependencyAttribute_CanBeAppliedToInterfaces()
        {
            var type = typeof(TransientTestService);
            var attributes = type.GetCustomAttributes(typeof(TransientDependencyAttribute), false);
            Assert.AreEqual(1, attributes.Length);
        }
    }
}