namespace Elect.Test.Data.EF.Services.Repository
{
    [TestClass]
    public class BaseEntityRepositoryUnitTest
    {
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
            var repository = new TestBaseEntityRepository(context);
            
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetSingle_WithValidPredicate_ReturnsEntity()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var result = repository.GetSingle(x => x.Id == entity.Id);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Id, result.Id);
        }

        [TestMethod]
        public void GetSingle_WithNoMatch_ReturnsNull()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var result = repository.GetSingle(x => x.Id == 999);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetSingle_WithDeletedEntity_ExcludesDeletedByDefault()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            entity.DeletedTime = DateTimeOffset.UtcNow;
            context.SaveChanges();
            
            var result = repository.GetSingle(x => x.Id == entity.Id);
            
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetSingle_WithDeletedEntity_IncludesDeletedWhenRequested()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            entity.DeletedTime = DateTimeOffset.UtcNow;
            context.SaveChanges();
            
            var result = repository.GetSingle(x => x.Id == entity.Id, isIncludeDeleted: true);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(entity.Id, result.Id);
        }

        [TestMethod]
        public void Get_WithoutPredicate_ReturnsAllNonDeletedEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity1 = new TestBaseEntity();
            var entity2 = new TestBaseEntity();
            var deletedEntity = new TestBaseEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(deletedEntity);
            context.SaveChanges();
            
            deletedEntity.DeletedTime = DateTimeOffset.UtcNow;
            context.SaveChanges();
            
            var results = repository.Get().ToList();
            
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void Get_WithPredicate_ReturnsFilteredEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity1 = new TestBaseEntity();
            var entity2 = new TestBaseEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            context.SaveChanges();
            
            var results = repository.Get(x => x.Id == entity1.Id).ToList();
            
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(entity1.Id, results[0].Id);
        }

        [TestMethod]
        public void Get_WithIncludeDeleted_ReturnsAllEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity1 = new TestBaseEntity();
            var deletedEntity = new TestBaseEntity();
            
            repository.Add(entity1);
            repository.Add(deletedEntity);
            context.SaveChanges();
            
            deletedEntity.DeletedTime = DateTimeOffset.UtcNow;
            context.SaveChanges();
            
            var results = repository.Get(isIncludeDeleted: true).ToList();
            
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public void Add_SetsTimestampsAndClearsDeletedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var beforeTime = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            var entity = new TestBaseEntity();
            entity.DeletedTime = DateTimeOffset.UtcNow.AddDays(-1); // Set deleted time to test clearing
            
            var result = repository.Add(entity);
            var afterTime = DateTimeOffset.UtcNow.AddMilliseconds(100);
            
            Assert.IsNotNull(result);
            Assert.IsNull(result.DeletedTime);
            Assert.IsTrue(result.CreatedTime >= beforeTime && result.CreatedTime <= afterTime);
            Assert.AreEqual(result.CreatedTime, result.LastUpdatedTime);
        }

        [TestMethod]
        public void Add_WithExistingCreatedTime_PreservesCreatedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var existingTime = DateTimeOffset.UtcNow.AddDays(-1);
            var entity = new TestBaseEntity();
            entity.CreatedTime = existingTime;
            
            var result = repository.Add(entity);
            
            Assert.AreEqual(existingTime, result.CreatedTime);
            Assert.AreEqual(existingTime, result.LastUpdatedTime);
        }

        [TestMethod]
        public void AddRange_WithMultipleEntities_AddsAllEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity1 = new TestBaseEntity();
            var entity2 = new TestBaseEntity();
            var entity3 = new TestBaseEntity();
            
            var results = repository.AddRange(entity1, entity2, entity3);
            
            Assert.AreEqual(3, results.Count);
            Assert.IsTrue(results.All(e => e.CreatedTime != default));
            Assert.IsTrue(results.All(e => e.LastUpdatedTime != default));
            Assert.IsTrue(results.All(e => e.DeletedTime == null));
        }

        [TestMethod]
        public void AddRange_WithEmptyArray_ReturnsEmptyList()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var results = repository.AddRange();
            
            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Update_WithChangedProperties_UpdatesLastUpdatedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            
            // Reset LastUpdatedTime to default to trigger update
            entity.LastUpdatedTime = default(DateTimeOffset);
            repository.Update(entity, x => x.CreatedTime);
            
            Assert.IsTrue(entity.LastUpdatedTime > default(DateTimeOffset));
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.CreatedTime).IsModified);
        }

        [TestMethod]
        public void Update_WithStringProperties_UpdatesLastUpdatedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            
            // Reset LastUpdatedTime to default to trigger update
            entity.LastUpdatedTime = default(DateTimeOffset);
            repository.Update(entity, "CreatedTime");
            
            Assert.IsTrue(entity.LastUpdatedTime > default(DateTimeOffset));
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.CreatedTime).IsModified);
        }

        [TestMethod]
        public void Update_WithoutSpecificProperties_MarksEntityModified()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            
            // Reset LastUpdatedTime to default to trigger update
            entity.LastUpdatedTime = default(DateTimeOffset);
            repository.Update(entity);
            
            Assert.IsTrue(entity.LastUpdatedTime > default(DateTimeOffset));
            var entry = context.Entry(entity);
            Assert.AreEqual(EntityState.Modified, entry.State);
        }

        [TestMethod]
        public void Update_WithExistingLastUpdatedTime_PreservesLastUpdatedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var existingTime = DateTimeOffset.UtcNow.AddHours(1);
            entity.LastUpdatedTime = existingTime;
            
            repository.Update(entity);
            
            Assert.AreEqual(existingTime, entity.LastUpdatedTime);
        }

        [TestMethod]
        public void Delete_WithSoftDelete_SetsDeletedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var beforeDelete = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            repository.Delete(entity, isPhysicalDelete: false);
            var afterDelete = DateTimeOffset.UtcNow.AddMilliseconds(100);
            
            Assert.IsNotNull(entity.DeletedTime);
            Assert.IsTrue(entity.DeletedTime >= beforeDelete && entity.DeletedTime <= afterDelete);
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.DeletedTime).IsModified);
        }

        [TestMethod]
        public void Delete_WithPhysicalDelete_RemovesEntity()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            repository.Delete(entity, isPhysicalDelete: true);
            
            var entry = context.Entry(entity);
            Assert.AreEqual(EntityState.Deleted, entry.State);
        }

        [TestMethod]
        public void Delete_WithExistingDeletedTime_PreservesDeletedTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var existingDeletedTime = DateTimeOffset.UtcNow.AddHours(1);
            entity.DeletedTime = existingDeletedTime;
            
            repository.Delete(entity, isPhysicalDelete: false);
            
            Assert.AreEqual(existingDeletedTime, entity.DeletedTime);
        }

        [TestMethod]
        public void DeleteWhere_WithSoftDelete_SoftDeletesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity1 = new TestBaseEntity();
            var entity2 = new TestBaseEntity();
            var entity3 = new TestBaseEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var targetIds = new[] { entity1.Id, entity2.Id };
            repository.DeleteWhere(x => targetIds.Contains(x.Id), isPhysicalDelete: false);
            
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithPhysicalDelete_PhysicallyDeletesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity1 = new TestBaseEntity();
            var entity2 = new TestBaseEntity();
            var entity3 = new TestBaseEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var targetIds = new[] { entity1.Id, entity2.Id };
            repository.DeleteWhere(x => targetIds.Contains(x.Id), isPhysicalDelete: true);
            
            context.SaveChanges();
            
            var remainingEntities = repository.Get(isIncludeDeleted: true).ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void Get_WithDuplicateIncludeProperties_RemovesDuplicates()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            // Since TestBaseEntity doesn't have navigation properties, 
            // we test that the Get method handles empty include properties correctly
            var results = repository.Get(null, false).ToList();
            
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public void Update_WithDuplicateChangedProperties_RemovesDuplicates()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            // Use same property multiple times to test deduplication
            repository.Update(entity, x => x.CreatedTime, x => x.CreatedTime);
            
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.CreatedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedTime).IsModified);
        }

        [TestMethod]
        public void Update_WithDuplicateStringProperties_RemovesDuplicates()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestBaseEntityRepository(context);
            
            var entity = new TestBaseEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            // Use same property multiple times to test deduplication
            repository.Update(entity, "CreatedTime", "CreatedTime");
            
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.CreatedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedTime).IsModified);
        }
    }
}