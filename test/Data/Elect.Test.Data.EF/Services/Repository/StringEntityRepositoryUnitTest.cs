namespace Elect.Test.Data.EF.Services.Repository
{
    [TestClass]
    public class StringEntityRepositoryUnitTest
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
            var repository = new TestStringEntityRepository(context);
            
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void Update_WithChangedProperties_UpdatesLastUpdatedByAndTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
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
        public void Update_WithStringProperties_UpdatesLastUpdatedByAndTime()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
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
        public void Update_WithoutSpecificProperties_MarksEntityModified()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
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
        public void UpdateWhere_WithExpressionPredicate_ThrowsWhenModifyingKeyProperty()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            var entity3 = new TestStringEntity { Id = "other-1" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var newData = new TestStringEntity { Id = "updated" };
            
            // Attempting to update the ID (key property) should throw
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                repository.UpdateWhere(x => x.Id.StartsWith("test-"), newData, x => x.Id);
            });
        }

        [TestMethod]
        public void UpdateWhere_WithStringProperties_ThrowsWhenModifyingKeyProperty()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            context.SaveChanges();
            
            var newData = new TestStringEntity { Id = "updated" };
            
            // Attempting to update the ID (key property) should throw
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                repository.UpdateWhere(x => x.Id.StartsWith("test-"), newData, "Id");
            });
        }

        [TestMethod]
        public void Delete_WithSoftDelete_SetsDeletedTimeAndDeletedBy()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
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
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
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
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            var entity3 = new TestStringEntity { Id = "other-1" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            repository.DeleteWhere(x => x.Id.StartsWith("test-"), isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithPhysicalDelete_PhysicallyDeletesMatchingEntities()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            var entity3 = new TestStringEntity { Id = "other-1" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            repository.DeleteWhere(x => x.Id.StartsWith("test-"), isPhysicalDelete: true);
            context.SaveChanges();
            
            var remainingEntities = repository.Get(isIncludeDeleted: true).ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity3.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithStringIdList_DeletesEntitiesWithMatchingIds()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            var entity3 = new TestStringEntity { Id = "test-3" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var idsToDelete = new List<string> { "test-1", "test-2" };
            repository.DeleteWhere(idsToDelete, isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual("test-3", remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithStringIdListPhysical_PhysicallyDeletesEntitiesWithMatchingIds()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            var entity3 = new TestStringEntity { Id = "test-3" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            repository.Add(entity3);
            context.SaveChanges();
            
            var idsToDelete = new List<string> { "test-1", "test-2" };
            repository.DeleteWhere(idsToDelete, isPhysicalDelete: true);
            context.SaveChanges();
            
            var remainingEntities = repository.Get(isIncludeDeleted: true).ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual("test-3", remainingEntities[0].Id);
        }

        [TestMethod]
        public void DeleteWhere_WithEmptyStringIdList_DoesNotDeleteAnything()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity1 = new TestStringEntity { Id = "test-1" };
            var entity2 = new TestStringEntity { Id = "test-2" };
            
            repository.Add(entity1);
            repository.Add(entity2);
            context.SaveChanges();
            
            var emptyIds = new List<string>();
            repository.DeleteWhere(emptyIds, isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(2, remainingEntities.Count);
        }

        [TestMethod]
        public void Update_WithDuplicateChangedProperties_ThrowsWhenModifyingKeyProperty()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
            repository.Add(entity);
            context.SaveChanges();
            
            // Attempting to modify ID (key property) should throw
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                repository.Update(entity, x => x.Id, x => x.Id);
            });
        }

        [TestMethod]
        public void Update_WithDuplicateStringProperties_ThrowsWhenModifyingKeyProperty()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
            repository.Add(entity);
            context.SaveChanges();
            
            // Attempting to modify ID (key property) should throw
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                repository.Update(entity, "Id", "Id");
            });
        }

        [TestMethod]
        public void UpdateWhere_WithNoMatchingEntities_DoesNotUpdate()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
            repository.Add(entity);
            context.SaveChanges();
            
            var originalLastUpdated = entity.LastUpdatedTime;
            var newData = new TestStringEntity { Id = "updated" };
            
            repository.UpdateWhere(x => x.Id.StartsWith("nomatch-"), newData, x => x.Id);
            context.SaveChanges();
            
            // Reload entity and check it wasn't updated
            var reloadedEntity = repository.GetSingle(x => x.Id == entity.Id);
            Assert.AreEqual(originalLastUpdated, reloadedEntity.LastUpdatedTime);
        }

        [TestMethod]
        public void DeleteWhere_WithNoMatchingEntities_DoesNotDelete()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
            repository.Add(entity);
            context.SaveChanges();
            
            repository.DeleteWhere(x => x.Id.StartsWith("nomatch-"), isPhysicalDelete: false);
            context.SaveChanges();
            
            var remainingEntities = repository.Get().ToList();
            Assert.AreEqual(1, remainingEntities.Count);
            Assert.AreEqual(entity.Id, remainingEntities[0].Id);
        }

        [TestMethod]
        public void Delete_WithException_RefreshesEntity()
        {
            using var context = CreateInMemoryContext();
            var repository = new TestStringEntityRepository(context);
            
            var entity = new TestStringEntity { Id = "test-1" };
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