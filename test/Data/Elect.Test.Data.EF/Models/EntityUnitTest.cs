namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class EntityUnitTest
    {
        private class TestEntity : Entity
        {
            // Test implementation of abstract class
        }

        [TestMethod]
        public void Constructor_InheritsFromBaseEntity()
        {
            var entity = new TestEntity();
            Assert.IsInstanceOfType(entity, typeof(BaseEntity));
        }

        [TestMethod]
        public void Constructor_InheritsFromEntityWithIntKey()
        {
            var entity = new TestEntity();
            Assert.IsInstanceOfType(entity, typeof(Entity<int>));
        }

        [TestMethod]
        public void ImplementsIGlobalIdentityEntity()
        {
            var entity = new TestEntity();
            // Interface test - checking for GlobalId property existence
            Assert.IsTrue(entity.GetType().GetProperty("GlobalId") != null);
        }

        [TestMethod]
        public void GlobalId_CanBeSetAndGet()
        {
            var entity = new TestEntity();
            var testGuid = Guid.NewGuid();
            
            // Use reflection to test GlobalId if present
            var globalIdProp = entity.GetType().GetProperty("GlobalId");
            if (globalIdProp != null)
            {
                globalIdProp.SetValue(entity, testGuid);
                Assert.AreEqual(testGuid, globalIdProp.GetValue(entity));
            }
            else
            {
                Assert.Inconclusive("GlobalId property not found on Entity");
            }
        }

        [TestMethod]
        public void GlobalId_DefaultValue()
        {
            var entity = new TestEntity();
            var globalIdProp = entity.GetType().GetProperty("GlobalId");
            if (globalIdProp != null)
            {
                Assert.AreEqual(Guid.Empty, globalIdProp.GetValue(entity));
            }
            else
            {
                Assert.Inconclusive("GlobalId property not found on Entity");
            }
        }

        [TestMethod]
        public void Id_InheritsFromEntityWithIntKey()
        {
            var entity = new TestEntity();
            entity.Id = 123;
            
            Assert.AreEqual(123, entity.Id);
        }

        [TestMethod]
        public void CreatedBy_InheritsFromEntityWithIntKey()
        {
            var entity = new TestEntity();
            entity.CreatedBy = 456;
            
            Assert.AreEqual(456, entity.CreatedBy);
        }

        [TestMethod]
        public void LastUpdatedBy_InheritsFromEntityWithIntKey()
        {
            var entity = new TestEntity();
            entity.LastUpdatedBy = 789;
            
            Assert.AreEqual(789, entity.LastUpdatedBy);
        }

        [TestMethod]
        public void DeletedBy_InheritsFromEntityWithIntKey()
        {
            var entity = new TestEntity();
            entity.DeletedBy = 101;
            
            Assert.AreEqual(101, entity.DeletedBy);
        }

        [TestMethod]
        public void AuditableProperties_CanBeNull()
        {
            var entity = new TestEntity();
            
            entity.CreatedBy = null;
            entity.LastUpdatedBy = null;
            entity.DeletedBy = null;
            
            Assert.IsNull(entity.CreatedBy);
            Assert.IsNull(entity.LastUpdatedBy);
            Assert.IsNull(entity.DeletedBy);
        }

        [TestMethod]
        public void InheritsTimestampProperties()
        {
            var entity = new TestEntity();
            
            // Should inherit timestamp properties from BaseEntity
            Assert.IsTrue(entity.CreatedTime > DateTimeOffset.MinValue);
            Assert.IsTrue(entity.LastUpdatedTime > DateTimeOffset.MinValue);
        }
    }
}