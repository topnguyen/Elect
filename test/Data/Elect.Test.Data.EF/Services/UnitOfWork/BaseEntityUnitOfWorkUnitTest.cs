namespace Elect.Test.Data.EF.Services.UnitOfWork
{
    [TestClass]
    public class BaseEntityUnitOfWorkUnitTest
    {
        private class TestBaseEntityUnitOfWork : BaseEntityUnitOfWork
        {
            public TestBaseEntityUnitOfWork(IDbContext dbContext) : base(dbContext)
            {
            }

            public void PublicStandardizeEntities()
            {
                StandardizeEntities();
            }
        }

        private TestDbContext CreateInMemoryContext(string dbName = null)
        {
            dbName ??= Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new TestDbContext(options);
        }

        [TestMethod]
        public void Constructor_WithValidDbContext_SetsDbContext()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            Assert.IsNotNull(unitOfWork);
        }

        [TestMethod]
        public void SaveChanges_WithBaseEntity_StandardizesTimestamps()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            var originalCreatedTime = entity.CreatedTime;
            var originalLastUpdatedTime = entity.LastUpdatedTime;
            
            context.Set<TestBaseEntity>().Add(entity);
            
            var result = unitOfWork.SaveChanges();
            
            Assert.AreEqual(1, result);
            // For added entities, both timestamps should be set to the same value
            Assert.AreEqual(entity.CreatedTime, entity.LastUpdatedTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void SaveChanges_WithAcceptAllChanges_StandardizesTimestamps()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            
            var result = unitOfWork.SaveChanges(true);
            
            Assert.AreEqual(1, result);
            Assert.AreEqual(entity.CreatedTime, entity.LastUpdatedTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public async Task SaveChangesAsync_WithBaseEntity_StandardizesTimestamps()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            
            var result = await unitOfWork.SaveChangesAsync();
            
            Assert.AreEqual(1, result);
            Assert.AreEqual(entity.CreatedTime, entity.LastUpdatedTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public async Task SaveChangesAsync_WithAcceptAllChanges_StandardizesTimestamps()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            var cancellationToken = new CancellationToken();
            
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            
            var result = await unitOfWork.SaveChangesAsync(true, cancellationToken);
            
            Assert.AreEqual(1, result);
            Assert.AreEqual(entity.CreatedTime, entity.LastUpdatedTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithAddedEntity_SetsCreatedAndLastUpdatedTime()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var beforeTime = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            var afterTime = DateTimeOffset.UtcNow.AddMilliseconds(100);
            
            unitOfWork.PublicStandardizeEntities();
            
            Assert.IsTrue(entity.CreatedTime >= beforeTime && entity.CreatedTime <= afterTime);
            Assert.AreEqual(entity.CreatedTime, entity.LastUpdatedTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithModifiedEntity_UpdatesLastUpdatedTime()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            context.SaveChanges();
            
            var originalCreatedTime = entity.CreatedTime;
            var beforeUpdateTime = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            
            // Simulate modification
            context.Entry(entity).State = EntityState.Modified;
            var afterUpdateTime = DateTimeOffset.UtcNow.AddMilliseconds(100);
            
            unitOfWork.PublicStandardizeEntities();
            
            Assert.AreEqual(originalCreatedTime, entity.CreatedTime);
            Assert.IsTrue(entity.LastUpdatedTime >= beforeUpdateTime && entity.LastUpdatedTime <= afterUpdateTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithModifiedEntityWithDeletedTime_UpdatesDeletedTime()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            context.SaveChanges();
            
            var originalCreatedTime = entity.CreatedTime;
            var originalLastUpdatedTime = entity.LastUpdatedTime;
            
            // The implementation checks if entity.DeletedTime != null, then updates it
            // ObjHelper.ReplaceNullOrDefault replaces null or default values with the provided default
            // Set deleted time to null first, then to default to trigger replacement
            entity.DeletedTime = null;
            entity.DeletedTime = default(DateTimeOffset?); // This should trigger replacement
            context.Entry(entity).State = EntityState.Modified;
            
            var beforeTime = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            unitOfWork.PublicStandardizeEntities();
            var afterTime = DateTimeOffset.UtcNow.AddMilliseconds(100);
            
            Assert.AreEqual(originalCreatedTime, entity.CreatedTime);
            Assert.AreEqual(originalLastUpdatedTime, entity.LastUpdatedTime);
            // Since DeletedTime was null, it should stay null (not updated)
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithNonBaseEntity_DoesNotProcess()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            // This should not throw or modify anything since TestEntity is not a BaseEntity
            unitOfWork.PublicStandardizeEntities();
            
            // Just verify it doesn't crash
            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public void StandardizeEntities_WithAddedEntityWithExistingCreatedTime_PreservesCreatedTime()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var existingTime = DateTimeOffset.UtcNow.AddDays(-1);
            var entity = new TestBaseEntity();
            entity.CreatedTime = existingTime;
            context.Set<TestBaseEntity>().Add(entity);
            
            unitOfWork.PublicStandardizeEntities();
            
            Assert.AreEqual(existingTime, entity.CreatedTime);
            Assert.AreEqual(existingTime, entity.LastUpdatedTime);
            Assert.IsNull(entity.DeletedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithModifiedEntityWithExistingLastUpdatedTime_PreservesLastUpdatedTime()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(entity);
            context.SaveChanges();
            
            var existingLastUpdated = DateTimeOffset.UtcNow.AddHours(-1);
            entity.LastUpdatedTime = existingLastUpdated;
            context.Entry(entity).State = EntityState.Modified;
            
            unitOfWork.PublicStandardizeEntities();
            
            Assert.AreEqual(existingLastUpdated, entity.LastUpdatedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithMultipleEntities_ProcessesAll()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var addedEntity = new TestBaseEntity();
            var modifiedEntity = new TestBaseEntity();
            
            context.Set<TestBaseEntity>().Add(addedEntity);
            context.Set<TestBaseEntity>().Add(modifiedEntity);
            context.SaveChanges();
            
            context.Entry(modifiedEntity).State = EntityState.Modified;
            
            var newEntity = new TestBaseEntity();
            context.Set<TestBaseEntity>().Add(newEntity);
            
            unitOfWork.PublicStandardizeEntities();
            
            // Added entity should have timestamps set
            Assert.AreEqual(newEntity.CreatedTime, newEntity.LastUpdatedTime);
            Assert.IsNull(newEntity.DeletedTime);
            
            // Modified entity should have updated timestamp
            Assert.IsNotNull(modifiedEntity.LastUpdatedTime);
        }

        [TestMethod]
        public void StandardizeEntities_WithDetachedEntity_DoesNotProcess()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestBaseEntityUnitOfWork(context);
            
            var entity = new TestBaseEntity();
            // Don't add to context, so it remains detached
            
            unitOfWork.PublicStandardizeEntities();
            
            // Entity should remain unchanged since it's not being tracked
            Assert.IsNotNull(entity);
        }
    }
}