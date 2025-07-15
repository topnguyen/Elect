namespace Elect.Test.Data.EF.Services.UnitOfWork
{
    [TestClass]
    public class UnitOfWorkUnitTest
    {
        private class TestUnitOfWork : Elect.Data.EF.Services.UnitOfWork.UnitOfWork<TestDbContext>
        {
            public TestUnitOfWork(TestDbContext dbContext) : base(dbContext)
            {
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
            var unitOfWork = new TestUnitOfWork(context);
            
            Assert.IsNotNull(unitOfWork);
        }

        [TestMethod]
        public void Constructor_InitializesActionCollections()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            Assert.IsNotNull(unitOfWork.FunctionsBeforeSaveChanges);
            Assert.IsNotNull(unitOfWork.ActionsAfterSaveChanges);
            Assert.IsNotNull(unitOfWork.ActionsBeforeCommit);
            Assert.IsNotNull(unitOfWork.ActionsAfterCommit);
            Assert.IsNotNull(unitOfWork.ActionsBeforeRollback);
            Assert.IsNotNull(unitOfWork.ActionsAfterRollback);
        }

        [TestMethod]
        public void BeginTransaction_WithIsolationLevel_ThrowsForInMemoryProvider()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            // InMemory provider doesn't support transactions
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                using var transaction = unitOfWork.BeginTransaction(IsolationLevel.ReadCommitted);
            });
        }

        [TestMethod]
        public void BeginTransaction_WithoutIsolationLevel_ThrowsForInMemoryProvider()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            // InMemory provider doesn't support transactions
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                using var transaction = unitOfWork.BeginTransaction();
            });
        }

        [TestMethod]
        public async Task BeginTransactionAsync_WithoutIsolationLevel_ThrowsForInMemoryProvider()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            // InMemory provider doesn't support transactions
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                using var transaction = await unitOfWork.BeginTransactionAsync();
            });
        }

        [TestMethod]
        public async Task BeginTransactionAsync_WithIsolationLevel_ThrowsForInMemoryProvider()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            // InMemory provider doesn't support transactions
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                using var transaction = await unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            });
        }

        [TestMethod]
        public async Task BeginTransactionAsync_WithCancellationToken_ThrowsForInMemoryProvider()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            var cancellationToken = new CancellationToken();
            
            // InMemory provider doesn't support transactions
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
            {
                using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            });
        }

        [TestMethod]
        public void SaveChanges_WithoutHooks_ReturnsResult()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            var result = unitOfWork.SaveChanges();
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void SaveChanges_WithBeforeSaveHook_ExecutesHook()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            bool hookExecuted = false;
            
            unitOfWork.FunctionsBeforeSaveChanges.Add((entries) =>
            {
                hookExecuted = true;
                return true;
            });
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            unitOfWork.SaveChanges();
            
            Assert.IsTrue(hookExecuted);
        }

        [TestMethod]
        public void SaveChanges_WithBeforeSaveHookReturnsFalse_DoesNotSave()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            unitOfWork.FunctionsBeforeSaveChanges.Add((entries) => false);
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            var result = unitOfWork.SaveChanges();
            
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SaveChanges_WithAfterSaveHook_ExecutesHook()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            bool hookExecuted = false;
            EntityStateCollection capturedState = null;
            
            unitOfWork.ActionsAfterSaveChanges.Add((entityStates) =>
            {
                hookExecuted = true;
                capturedState = entityStates;
            });
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            unitOfWork.SaveChanges();
            
            Assert.IsTrue(hookExecuted);
            Assert.IsNotNull(capturedState);
            Assert.AreEqual(1, capturedState.ListAdded.Count);
        }

        [TestMethod]
        public void SaveChanges_WithAcceptAllChanges_ReturnsResult()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            var result = unitOfWork.SaveChanges(true);
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task SaveChangesAsync_WithoutParameters_ReturnsResult()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            var result = await unitOfWork.SaveChangesAsync();
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task SaveChangesAsync_WithCancellationToken_ReturnsResult()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            var cancellationToken = new CancellationToken();
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task SaveChangesAsync_WithAcceptAllChanges_ReturnsResult()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            var cancellationToken = new CancellationToken();
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            var result = await unitOfWork.SaveChangesAsync(true, cancellationToken);
            
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void SplitEntity_WithAddedModifiedDeletedEntities_GroupsCorrectly()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            var addedEntity = new TestEntity { Name = "Added" };
            var modifiedEntity = new TestEntity { Name = "Original" };
            var deletedEntity = new TestEntity { Name = "ToDelete" };
            
            // Add entities first
            context.TestEntities.Add(addedEntity);
            context.TestEntities.Add(modifiedEntity);
            context.TestEntities.Add(deletedEntity);
            context.SaveChanges();
            
            // Modify and delete
            modifiedEntity.Name = "Modified";
            context.Entry(modifiedEntity).State = EntityState.Modified;
            context.Entry(deletedEntity).State = EntityState.Deleted;
            
            // Add new entity
            var newEntity = new TestEntity { Name = "New" };
            context.TestEntities.Add(newEntity);
            
            // Access protected method via reflection
            var method = typeof(Elect.Data.EF.Services.UnitOfWork.UnitOfWork<TestDbContext>)
                .GetMethod("SplitEntity", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method.Invoke(unitOfWork, null) as EntityStateCollection;
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ListAdded.Count >= 1);
            Assert.IsTrue(result.ListModified.Count >= 1);
            Assert.IsTrue(result.ListDeleted.Count >= 1);
        }

        [TestMethod]
        public void CreateCommand_DelegatesToDbContext()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            var sql = "SELECT 1";
            
            // InMemory provider doesn't support GetDbConnection
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var command = unitOfWork.CreateCommand(sql);
            });
        }

        [TestMethod]
        public void ExecuteCommand_DelegatesToDbContext()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            // InMemory provider doesn't support GetDbConnection
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                unitOfWork.ExecuteCommand("SELECT 1");
            });
        }

        [TestMethod]
        public void ExecuteCommand_Generic_DelegatesToDbContext()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            
            // InMemory provider doesn't support GetDbConnection
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                var result = unitOfWork.ExecuteCommand<TestEntity>("SELECT * FROM TestEntities");
            });
        }

        [TestMethod]
        public void FunctionsBeforeSaveChanges_MultipleHooks_AllExecuted()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            int executionCount = 0;
            
            unitOfWork.FunctionsBeforeSaveChanges.Add((entries) =>
            {
                executionCount++;
                return true;
            });
            
            unitOfWork.FunctionsBeforeSaveChanges.Add((entries) =>
            {
                executionCount++;
                return true;
            });
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            unitOfWork.SaveChanges();
            
            Assert.AreEqual(2, executionCount);
        }

        [TestMethod]
        public void ActionsAfterSaveChanges_MultipleHooks_AllExecuted()
        {
            using var context = CreateInMemoryContext();
            var unitOfWork = new TestUnitOfWork(context);
            int executionCount = 0;
            
            unitOfWork.ActionsAfterSaveChanges.Add((entityStates) =>
            {
                executionCount++;
            });
            
            unitOfWork.ActionsAfterSaveChanges.Add((entityStates) =>
            {
                executionCount++;
            });
            
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            
            unitOfWork.SaveChanges();
            
            Assert.AreEqual(2, executionCount);
        }
    }
}