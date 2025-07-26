namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class VersionEntityUnitTest
    {
        private class TestVersionEntity : VersionEntity
        {
            // Test implementation of abstract class
        }

        [TestMethod]
        public void Constructor_InheritsFromVersionEntityWithIntKey()
        {
            var entity = new TestVersionEntity();
            Assert.IsInstanceOfType(entity, typeof(VersionEntity<int>));
        }

        [TestMethod]
        public void Constructor_InheritsFromEntity()
        {
            var entity = new TestVersionEntity();
            Assert.IsInstanceOfType(entity, typeof(Entity<int>));
        }

        [TestMethod]
        public void ImplementsIVersionEntity()
        {
            var entity = new TestVersionEntity();
            Assert.IsInstanceOfType(entity, typeof(IVersionEntity));
        }

        [TestMethod]
        public void Version_CanBeSetAndGet()
        {
            var entity = new TestVersionEntity();
            var testVersion = new byte[] { 1, 2, 3, 4 };
            
            entity.Version = testVersion;
            
            Assert.AreEqual(testVersion, entity.Version);
        }

        [TestMethod]
        public void Version_DefaultValue()
        {
            var entity = new TestVersionEntity();
            Assert.IsNull(entity.Version);
        }

        [TestMethod]
        public void Id_InheritsFromEntity()
        {
            var entity = new TestVersionEntity();
            entity.Id = 123;
            
            Assert.AreEqual(123, entity.Id);
        }

        [TestMethod]
        public void AuditableProperties_InheritsFromEntity()
        {
            var entity = new TestVersionEntity();
            entity.CreatedBy = 456;
            entity.LastUpdatedBy = 789;
            entity.DeletedBy = 101;
            
            Assert.AreEqual(456, entity.CreatedBy);
            Assert.AreEqual(789, entity.LastUpdatedBy);
            Assert.AreEqual(101, entity.DeletedBy);
        }

        [TestMethod]
        public void InheritsTimestampProperties()
        {
            var entity = new TestVersionEntity();
            
            // Should inherit timestamp properties from BaseEntity
            Assert.IsTrue(entity.CreatedTime > DateTimeOffset.MinValue);
            Assert.IsTrue(entity.LastUpdatedTime > DateTimeOffset.MinValue);
        }

        [TestMethod]
        public void DoesNotImplementIGlobalIdentityEntity()
        {
            var entity = new TestVersionEntity();
            // VersionEntity inherits from Entity<TKey>, not Entity, so it doesn't have GlobalId
            Assert.IsFalse(entity is IGlobalIdentityEntity);
            var hasGlobalId = HasPropertyInHierarchy(entity.GetType(), "GlobalId");
            Assert.IsFalse(hasGlobalId, "VersionEntity should not have GlobalId property as it inherits from Entity<TKey>, not Entity");
        }

        private bool HasPropertyInHierarchy(Type type, string propertyName)
        {
            while (type != null && type != typeof(object))
            {
                if (type.GetProperty(propertyName) != null)
                    return true;
                type = type.BaseType;
            }
            return false;
        }

        [TestMethod]
        public void DoesNotHaveGlobalId()
        {
            var entity = new TestVersionEntity();
            
            // VersionEntity inherits from Entity<TKey>, not Entity, so it should not have GlobalId
            var globalIdProp = entity.GetType().GetProperty("GlobalId") ?? entity.GetType().BaseType?.GetProperty("GlobalId");
            Assert.IsNull(globalIdProp, "VersionEntity should not have GlobalId property as it inherits from Entity<TKey>, not Entity");
        }
    }
}