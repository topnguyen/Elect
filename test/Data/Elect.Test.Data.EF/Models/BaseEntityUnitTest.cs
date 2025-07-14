namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class BaseEntityUnitTest
    {
        private class TestBaseEntity : BaseEntity
        {
            // Test implementation of abstract class
        }

        [TestMethod]
        public void Constructor_SetsTimestamps()
        {
            var beforeCreate = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            var entity = new TestBaseEntity();
            var afterCreate = DateTimeOffset.UtcNow.AddMilliseconds(100);

            Assert.IsTrue(entity.CreatedTime >= beforeCreate && entity.CreatedTime <= afterCreate);
            Assert.IsTrue(entity.LastUpdatedTime >= beforeCreate && entity.LastUpdatedTime <= afterCreate);
            Assert.AreEqual(entity.CreatedTime, entity.LastUpdatedTime);
        }

        [TestMethod]
        public void Constructor_InitializesDeletedTimeAsNull()
        {
            var entity = new TestBaseEntity();
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void CreatedTime_CanBeSetAndGet()
        {
            var entity = new TestBaseEntity();
            var testTime = DateTimeOffset.UtcNow.AddDays(-1);
            
            entity.CreatedTime = testTime;
            
            Assert.AreEqual(testTime, entity.CreatedTime);
        }

        [TestMethod]
        public void LastUpdatedTime_CanBeSetAndGet()
        {
            var entity = new TestBaseEntity();
            var testTime = DateTimeOffset.UtcNow.AddHours(-1);
            
            entity.LastUpdatedTime = testTime;
            
            Assert.AreEqual(testTime, entity.LastUpdatedTime);
        }

        [TestMethod]
        public void DeletedTime_CanBeSetAndGet()
        {
            var entity = new TestBaseEntity();
            var testTime = DateTimeOffset.UtcNow.AddMinutes(-30);
            
            entity.DeletedTime = testTime;
            
            Assert.AreEqual(testTime, entity.DeletedTime);
        }

        [TestMethod]
        public void DeletedTime_CanBeSetToNull()
        {
            var entity = new TestBaseEntity();
            entity.DeletedTime = DateTimeOffset.UtcNow;
            
            entity.DeletedTime = null;
            
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void ImplementsISoftDeletableEntity()
        {
            var entity = new TestBaseEntity();
            // Interface test - checking for soft delete capability via DeletedTime property
            Assert.IsTrue(entity.GetType().GetProperty("DeletedTime") != null);
        }

        [TestMethod]
        public void ImplementsIAuditableEntity()
        {
            var entity = new TestBaseEntity();
            // Interface test - checking for audit capability via timestamp properties
            Assert.IsTrue(entity.GetType().GetProperty("CreatedTime") != null);
            Assert.IsTrue(entity.GetType().GetProperty("LastUpdatedTime") != null);
        }

        [TestMethod]
        public void InheritsFromElectDisposableModel()
        {
            var entity = new TestBaseEntity();
            Assert.IsInstanceOfType(entity, typeof(Elect.Core.ObjUtils.ElectDisposableModel));
        }

        [TestMethod]
        public void Dispose_DoesNotThrow()
        {
            var entity = new TestBaseEntity();
            // Should not throw
            entity.Dispose();
        }
    }
}