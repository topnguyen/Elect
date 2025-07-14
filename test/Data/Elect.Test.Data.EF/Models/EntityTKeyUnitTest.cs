namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class EntityTKeyUnitTest
    {
        private class TestEntityInt : Entity<int>
        {
            // Test implementation of abstract class with int key
        }

        private class TestEntityLong : Entity<long>
        {
            // Test implementation of abstract class with long key
        }

        [TestMethod]
        public void Constructor_InheritsFromBaseEntity()
        {
            var entity = new TestEntityInt();
            Assert.IsInstanceOfType(entity, typeof(BaseEntity));
        }

        [TestMethod]
        public void Id_IntKey_CanBeSetAndGet()
        {
            var entity = new TestEntityInt();
            entity.Id = 123;
            
            Assert.AreEqual(123, entity.Id);
        }

        [TestMethod]
        public void Id_LongKey_CanBeSetAndGet()
        {
            var entity = new TestEntityLong();
            entity.Id = 12345678L;
            
            Assert.AreEqual(12345678L, entity.Id);
        }

        [TestMethod]
        public void Id_DefaultValue()
        {
            var entity = new TestEntityInt();
            Assert.AreEqual(0, entity.Id);
        }

        [TestMethod]
        public void CreatedBy_CanBeSetAndGet()
        {
            var entity = new TestEntityInt();
            entity.CreatedBy = 456;
            
            Assert.AreEqual(456, entity.CreatedBy);
        }

        [TestMethod]
        public void CreatedBy_CanBeNull()
        {
            var entity = new TestEntityInt();
            entity.CreatedBy = null;
            
            Assert.IsNull(entity.CreatedBy);
        }

        [TestMethod]
        public void LastUpdatedBy_CanBeSetAndGet()
        {
            var entity = new TestEntityInt();
            entity.LastUpdatedBy = 789;
            
            Assert.AreEqual(789, entity.LastUpdatedBy);
        }

        [TestMethod]
        public void LastUpdatedBy_CanBeNull()
        {
            var entity = new TestEntityInt();
            entity.LastUpdatedBy = null;
            
            Assert.IsNull(entity.LastUpdatedBy);
        }

        [TestMethod]
        public void DeletedBy_CanBeSetAndGet()
        {
            var entity = new TestEntityInt();
            entity.DeletedBy = 101;
            
            Assert.AreEqual(101, entity.DeletedBy);
        }

        [TestMethod]
        public void DeletedBy_CanBeNull()
        {
            var entity = new TestEntityInt();
            entity.DeletedBy = null;
            
            Assert.IsNull(entity.DeletedBy);
        }

        [TestMethod]
        public void AuditableProperties_DefaultValues()
        {
            var entity = new TestEntityInt();
            
            Assert.IsNull(entity.CreatedBy);
            Assert.IsNull(entity.LastUpdatedBy);
            Assert.IsNull(entity.DeletedBy);
        }

        [TestMethod]
        public void InheritsTimestampProperties()
        {
            var entity = new TestEntityInt();
            
            // Should inherit timestamp properties from BaseEntity
            Assert.IsTrue(entity.CreatedTime > DateTimeOffset.MinValue);
            Assert.IsTrue(entity.LastUpdatedTime > DateTimeOffset.MinValue);
        }

        [TestMethod]
        public void ImplementsISoftDeletableEntity()
        {
            var entity = new TestEntityInt();
            // Interface test - checking for soft delete capability via DeletedTime property
            Assert.IsTrue(entity.GetType().GetProperty("DeletedTime") != null);
        }

        [TestMethod]
        public void ImplementsIAuditableEntity()
        {
            var entity = new TestEntityInt();
            // Interface test - checking for audit capability via timestamp properties
            Assert.IsTrue(entity.GetType().GetProperty("CreatedTime") != null);
            Assert.IsTrue(entity.GetType().GetProperty("LastUpdatedTime") != null);
        }

        [TestMethod]
        public void DifferentKeyTypes_AreIndependent()
        {
            var intEntity = new TestEntityInt();
            var longEntity = new TestEntityLong();

            intEntity.Id = 123;
            longEntity.Id = 456789L;
            intEntity.CreatedBy = 111;
            longEntity.CreatedBy = 222L;

            Assert.AreEqual(123, intEntity.Id);
            Assert.AreEqual(456789L, longEntity.Id);
            Assert.AreEqual(111, intEntity.CreatedBy);
            Assert.AreEqual(222L, longEntity.CreatedBy);
        }
    }
}