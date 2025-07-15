namespace Elect.Test.Data.EF.Services.Repository
{
    [TestClass]
    public class EntityRepositoryUnitTest
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
            var repository = new TestVersionEntityRepository(context);
            
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Update_WithChangedProperties_UpdatesLastUpdatedByAndTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            
            // Reset LastUpdatedTime to default to trigger update
            entity.LastUpdatedTime = default(DateTimeOffset);
            repository.Update(entity, x => x.Version);
            
            Assert.IsTrue(entity.LastUpdatedTime > default(DateTimeOffset));
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedBy).IsModified);
            Assert.IsTrue(entry.Property(x => x.Version).IsModified);
        }

        [TestMethod]
        public void Update_WithStringProperties_UpdatesLastUpdatedByAndTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            
            // Reset LastUpdatedTime to default to trigger update
            entity.LastUpdatedTime = default(DateTimeOffset);
            repository.Update(entity, "Version");
            
            Assert.IsTrue(entity.LastUpdatedTime > default(DateTimeOffset));
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.LastUpdatedBy).IsModified);
            Assert.IsTrue(entry.Property(x => x.Version).IsModified);
        }

        [TestMethod]
        public void UpdateWhere_WithExpressionPredicate_UpdatesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity2 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity3 = new TestVersionEntity { Version = new byte[] { 2 } };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var newData = new TestVersionEntity { Version = new byte[] { 3 } };
            var beforeUpdate = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            
            repository.UpdateWhere(x => x.Version.SequenceEqual(new byte[] { 1 }), newData, x => x.Version);
            
            var afterUpdate = DateTimeOffset.UtcNow.AddMilliseconds(100);
            context.SaveChanges();
            
            // Check that entities with Version = [1] were updated
            var updatedEntities = context.Set<TestVersionEntity>().Where(x => x.Id == entity1.Id || x.Id == entity2.Id).ToList();
            foreach (var entity in updatedEntities)
            {
                Assert.IsTrue(entity.LastUpdatedTime >= beforeUpdate && entity.LastUpdatedTime <= afterUpdate);
            }
        }

        [TestMethod]
        public void UpdateWhere_WithStringProperties_UpdatesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity2 = new TestVersionEntity { Version = new byte[] { 1 } };
            
            repository.Add(entity1);
            repository.Add(entity2);
            context.SaveChanges();
            
            var newData = new TestVersionEntity { Version = new byte[] { 3 } };
            var beforeUpdate = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            
            repository.UpdateWhere(x => x.Version.SequenceEqual(new byte[] { 1 }), newData, "Version");
            
            var afterUpdate = DateTimeOffset.UtcNow.AddMilliseconds(100);
            context.SaveChanges();
            
            // Check that entities were updated
            var updatedEntities = context.Set<TestVersionEntity>().Where(x => x.Id == entity1.Id || x.Id == entity2.Id).ToList();
            foreach (var entity in updatedEntities)
            {
                Assert.IsTrue(entity.LastUpdatedTime >= beforeUpdate && entity.LastUpdatedTime <= afterUpdate);
            }
        }

        [TestMethod]
        public void Delete_WithSoftDelete_SetsDeletedTimeAndDeletedBy()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            var beforeDelete = DateTimeOffset.UtcNow.AddMilliseconds(-100);
            repository.Delete(entity, isPhysicalDelete: false);
            var afterDelete = DateTimeOffset.UtcNow.AddMilliseconds(100);
            
            Assert.IsNotNull(entity.DeletedTime);
            Assert.IsTrue(entity.DeletedTime >= beforeDelete && entity.DeletedTime <= afterDelete);
            var entry = context.Entry(entity);
            Assert.IsTrue(entry.Property(x => x.DeletedTime).IsModified);
            Assert.IsTrue(entry.Property(x => x.DeletedBy).IsModified);
        }

        [TestMethod]
        public void Delete_WithPhysicalDelete_RemovesEntity()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            repository.Delete(entity, isPhysicalDelete: true);
            
            var entry = context.Entry(entity);
            Assert.AreEqual(EntityState.Deleted, entry.State);
        }

        [TestMethod]
        public void DeleteWhere_WithPredicate_DeletesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity2 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity3 = new TestVersionEntity { Version = new byte[] { 2 } };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            repository.DeleteWhere(x => x.Version.SequenceEqual(new byte[] { 1 }), isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithPhysicalDelete_PhysicallyDeletesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity2 = new TestVersionEntity { Version = new byte[] { 1 } };
            var entity3 = new TestVersionEntity { Version = new byte[] { 2 } };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            repository.DeleteWhere(x => x.Version.SequenceEqual(new byte[] { 1 }), isPhysicalDelete: true);
            context.SaveChanges();
            
            var remainingEntities = repository.Get(isIncludeDeleted: true).ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithIdList_DeletesEntitiesWithMatchingIds()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity();
            var entity2 = new TestVersionEntity();
            var entity3 = new TestVersionEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var idsToDelete = new List<int> { entity1.Id, entity2.Id };
            repository.DeleteWhere(idsToDelete, isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithIdListPhysical_PhysicallyDeletesEntitiesWithMatchingIds()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity();
            var entity2 = new TestVersionEntity();
            var entity3 = new TestVersionEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var idsToDelete = new List<int> { entity1.Id, entity2.Id };
            repository.DeleteWhere(idsToDelete, isPhysicalDelete: true);
            context.SaveChanges();
            
            var remainingEntities = repository.Get(isIncludeDeleted: true).ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithEmptyIdList_DoesNotDeleteAnything()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity1 = new TestVersionEntity();
            var entity2 = new TestVersionEntity();
            
            repository.Add(entity1);
            repository.Add(entity2);
            context.SaveChanges();
            
            var emptyIds = new List<int>();
            repository.DeleteWhere(emptyIds, isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(2, remainingEntities.Count);
        }

        [TestMethod]
        public void UpdateWhere_WithNoMatchingEntities_DoesNotUpdate()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity { Version = new byte[] { 1 } };
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            var newData = new TestVersionEntity { Version = new byte[] { 3 } };
            
            repository.UpdateWhere(x => x.Version.SequenceEqual(new byte[] { 99 }), newData, x => x.Version);
            context.SaveChanges();
            
            // Reload entity and check it wasn't updated
            var reloadedEntity = repository.GetSingle(x => x.Id == entity.Id);
            Assert.AreEqual(originalLastUpdated, reloadedEntity.LastUpdatedTime);
        }

        [TestMethod]
        public void DeleteWhere_WithNoMatchingEntities_DoesNotDelete()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity { Version = new byte[] { 1 } };
            repository.Add(entity);
            context.SaveChanges();
            
            repository.DeleteWhere(x => x.Version.SequenceEqual(new byte[] { 99 }), isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void Delete_WithException_RefreshesEntity()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestVersionEntityRepository(context);
            
            var entity = new TestVersionEntity();
            repository.Add(entity);
            context.SaveChanges();
            
            // Detach the entity to simulate a concurrency issue
            context.Entry(entity).State = EntityState.Detached;
            
            // This should refresh the entity when exception occurs
            try
            {
                repository.Delete(entity, isPhysicalDelete: false);
            }
            catch
            {
                // Expected to throw due to detached state
            }
            
            // Entity should be refreshed (re-attached)
            Assert.AreNotEqual(EntityState.Detached, context.Entry(entity).State);
        }
    }
}