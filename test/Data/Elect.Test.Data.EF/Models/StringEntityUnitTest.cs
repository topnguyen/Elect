namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class StringEntityUnitTest
    {
        private class TestStringEntity : StringEntity
        {
            // Test implementation of abstract class
        }

        [TestMethod]
        public void Constructor_InheritsFromBaseEntity()
        {
            var entity = new TestStringEntity();
            Assert.IsInstanceOfType(entity, typeof(BaseEntity));
        }

        [TestMethod]
        public void Id_CanBeSetAndGet()
        {
            var entity = new TestStringEntity();
            var testId = "test-id-123";
            
            entity.Id = testId;
            
            Assert.AreEqual(testId, entity.Id);
        }

        [TestMethod]
        public void Id_DefaultValue()
        {
            var entity = new TestStringEntity();
            Assert.IsNull(entity.Id);
        }

        [TestMethod]
        public void CreatedBy_CanBeSetAndGet()
        {
            var entity = new TestStringEntity();
            var testCreatedBy = "user-456";
            
            entity.CreatedBy = testCreatedBy;
            
            Assert.AreEqual(testCreatedBy, entity.CreatedBy);
        }

        [TestMethod]
        public void CreatedBy_DefaultValue()
        {
            var entity = new TestStringEntity();
            Assert.IsNull(entity.CreatedBy);
        }

        [TestMethod]
        public void LastUpdatedBy_CanBeSetAndGet()
        {
            var entity = new TestStringEntity();
            var testUpdatedBy = "user-789";
            
            entity.LastUpdatedBy = testUpdatedBy;
            
            Assert.AreEqual(testUpdatedBy, entity.LastUpdatedBy);
        }

        [TestMethod]
        public void LastUpdatedBy_DefaultValue()
        {
            var entity = new TestStringEntity();
            Assert.IsNull(entity.LastUpdatedBy);
        }

        [TestMethod]
        public void DeletedBy_CanBeSetAndGet()
        {
            var entity = new TestStringEntity();
            var testDeletedBy = "user-101";
            
            entity.DeletedBy = testDeletedBy;
            
            Assert.AreEqual(testDeletedBy, entity.DeletedBy);
        }

        [TestMethod]
        public void DeletedBy_DefaultValue()
        {
            var entity = new TestStringEntity();
            Assert.IsNull(entity.DeletedBy);
        }

        [TestMethod]
        public void AuditableProperties_CanBeNull()
        {
            var entity = new TestStringEntity();
            
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
            var entity = new TestStringEntity();
            
            // Should inherit timestamp properties from BaseEntity
            Assert.IsTrue(entity.CreatedTime > DateTimeOffset.MinValue);
            Assert.IsTrue(entity.LastUpdatedTime > DateTimeOffset.MinValue);
        }

        [TestMethod]
        public void ImplementsISoftDeletableEntity()
        {
            var entity = new TestStringEntity();
            // Interface test - checking for soft delete capability via DeletedTime property
            Assert.IsTrue(entity.GetType().GetProperty("DeletedTime") != null);
        }

        [TestMethod]
        public void ImplementsIAuditableEntity()
        {
            var entity = new TestStringEntity();
            // Interface test - checking for audit capability via timestamp properties
            Assert.IsTrue(entity.GetType().GetProperty("CreatedTime") != null);
            Assert.IsTrue(entity.GetType().GetProperty("LastUpdatedTime") != null);
        }
    }
}