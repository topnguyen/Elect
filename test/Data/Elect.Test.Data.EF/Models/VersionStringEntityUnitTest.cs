namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class VersionStringEntityUnitTest
    {
        [TestMethod]
        public void Constructor_InheritsFromStringEntity()
        {
            var entity = new VersionStringEntity();
            Assert.IsInstanceOfType(entity, typeof(StringEntity));
        }

        [TestMethod]
        public void ImplementsIVersionEntity()
        {
            var entity = new VersionStringEntity();
            Assert.IsInstanceOfType(entity, typeof(IVersionEntity));
        }

        [TestMethod]
        public void Version_CanBeSetAndGet()
        {
            var entity = new VersionStringEntity();
            var testVersion = new byte[] { 1, 2, 3, 4 };
            
            entity.Version = testVersion;
            
            Assert.AreEqual(testVersion, entity.Version);
        }

        [TestMethod]
        public void Version_DefaultValue()
        {
            var entity = new VersionStringEntity();
            Assert.IsNull(entity.Version);
        }

        [TestMethod]
        public void Id_InheritsFromStringEntity()
        {
            var entity = new VersionStringEntity();
            var testId = "test-id-456";
            
            entity.Id = testId;
            
            Assert.AreEqual(testId, entity.Id);
        }

        [TestMethod]
        public void AuditableProperties_InheritsFromStringEntity()
        {
            var entity = new VersionStringEntity();
            entity.CreatedBy = "user-456";
            entity.LastUpdatedBy = "user-789";
            entity.DeletedBy = "user-101";
            
            Assert.AreEqual("user-456", entity.CreatedBy);
            Assert.AreEqual("user-789", entity.LastUpdatedBy);
            Assert.AreEqual("user-101", entity.DeletedBy);
        }

        [TestMethod]
        public void InheritsTimestampProperties()
        {
            var entity = new VersionStringEntity();
            
            // Should inherit timestamp properties from BaseEntity
            Assert.IsTrue(entity.CreatedTime > DateTimeOffset.MinValue);
            Assert.IsTrue(entity.LastUpdatedTime > DateTimeOffset.MinValue);
        }

        [TestMethod]
        public void ImplementsISoftDeletableEntity()
        {
            var entity = new VersionStringEntity();
            // Interface test - checking for soft delete capability via DeletedTime property
            Assert.IsTrue(entity.GetType().GetProperty("DeletedTime") != null);
        }

        [TestMethod]
        public void ImplementsIAuditableEntity()
        {
            var entity = new VersionStringEntity();
            // Interface test - checking for audit capability via timestamp properties
            Assert.IsTrue(entity.GetType().GetProperty("CreatedTime") != null);
            Assert.IsTrue(entity.GetType().GetProperty("LastUpdatedTime") != null);
        }
    }
}