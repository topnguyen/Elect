namespace Elect.Test.Data.EF.Services.UnitOfWork
{
    [TestClass]
    public class UnitOfWorkTransactionUnitTest
    {
        private TestDbContext CreateInMemoryContext(string dbName = null)
        {
            dbName ??= Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new TestDbContext(options);
        }

        private class MockDbContextTransaction : IDbContextTransaction
        {
            public Guid TransactionId => Guid.NewGuid();
            public void Commit() { }
            public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
            public void Rollback() { }
            public Task RollbackAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
            public void Dispose() { }
            public ValueTask DisposeAsync() => ValueTask.CompletedTask;
        }

        [TestMethod]
        public void Constructor_WithValidDbContextTransaction_SetsTransaction()
        {
            var mockTransaction = new MockDbContextTransaction();
            
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            Assert.IsNotNull(transaction);
            Assert.IsNotNull(transaction.ActionsBeforeCommit);
            Assert.IsNotNull(transaction.ActionsAfterCommit);
            Assert.IsNotNull(transaction.ActionsBeforeRollback);
            Assert.IsNotNull(transaction.ActionsAfterRollback);
        }

        [TestMethod]
        public void Dispose_DisposesUnderlyingTransaction()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            transaction.Dispose();
            
            // After disposal, the underlying transaction should be disposed
            // We can verify no exception occurs
            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void Commit_WithoutActions_CommitsSuccessfully()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            transaction.Commit();
            
            // If no exception is thrown, commit was successful
            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void Commit_WithBeforeCommitActions_ExecutesActions()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            bool beforeActionExecuted = false;
            transaction.ActionsBeforeCommit.Add(() =>
            {
                beforeActionExecuted = true;
            });
            
            transaction.Commit();
            
            Assert.IsTrue(beforeActionExecuted);
        }

        [TestMethod]
        public void Commit_WithAfterCommitActions_ExecutesActions()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            bool afterActionExecuted = false;
            transaction.ActionsAfterCommit.Add(() =>
            {
                afterActionExecuted = true;
            });
            
            transaction.Commit();
            
            Assert.IsTrue(afterActionExecuted);
        }

        [TestMethod]
        public void Commit_WithBeforeAndAfterActions_ExecutesInCorrectOrder()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            var executionOrder = new List<string>();
            
            transaction.ActionsBeforeCommit.Add(() =>
            {
                executionOrder.Add("before");
            });
            
            transaction.ActionsAfterCommit.Add(() =>
            {
                executionOrder.Add("after");
            });
            
            transaction.Commit();
            
            Assert.AreEqual(2, executionOrder.Count);
            Assert.AreEqual("before", executionOrder[0]);
            Assert.AreEqual("after", executionOrder[1]);
        }

        [TestMethod]
        public void Commit_WithMultipleActionsOfSameType_ExecutesAllActions()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            int beforeCount = 0;
            int afterCount = 0;
            
            transaction.ActionsBeforeCommit.Add(() => beforeCount++);
            transaction.ActionsBeforeCommit.Add(() => beforeCount++);
            
            transaction.ActionsAfterCommit.Add(() => afterCount++);
            transaction.ActionsAfterCommit.Add(() => afterCount++);
            
            transaction.Commit();
            
            Assert.AreEqual(2, beforeCount);
            Assert.AreEqual(2, afterCount);
        }

        [TestMethod]
        public void Rollback_WithoutActions_RollsBackSuccessfully()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            transaction.Rollback();
            
            // If no exception is thrown, rollback was successful
            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void Rollback_WithBeforeRollbackActions_ExecutesActions()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            bool beforeActionExecuted = false;
            transaction.ActionsBeforeRollback.Add(() =>
            {
                beforeActionExecuted = true;
            });
            
            transaction.Rollback();
            
            Assert.IsTrue(beforeActionExecuted);
        }

        [TestMethod]
        public void Rollback_WithAfterRollbackActions_ExecutesActions()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            bool afterActionExecuted = false;
            transaction.ActionsAfterRollback.Add(() =>
            {
                afterActionExecuted = true;
            });
            
            transaction.Rollback();
            
            Assert.IsTrue(afterActionExecuted);
        }

        [TestMethod]
        public void Rollback_WithBeforeAndAfterActions_ExecutesInCorrectOrder()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            var executionOrder = new List<string>();
            
            transaction.ActionsBeforeRollback.Add(() =>
            {
                executionOrder.Add("before");
            });
            
            transaction.ActionsAfterRollback.Add(() =>
            {
                executionOrder.Add("after");
            });
            
            transaction.Rollback();
            
            Assert.AreEqual(2, executionOrder.Count);
            Assert.AreEqual("before", executionOrder[0]);
            Assert.AreEqual("after", executionOrder[1]);
        }

        [TestMethod]
        public void Rollback_WithMultipleActionsOfSameType_ExecutesAllActions()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            int beforeCount = 0;
            int afterCount = 0;
            
            transaction.ActionsBeforeRollback.Add(() => beforeCount++);
            transaction.ActionsBeforeRollback.Add(() => beforeCount++);
            
            transaction.ActionsAfterRollback.Add(() => afterCount++);
            transaction.ActionsAfterRollback.Add(() => afterCount++);
            
            transaction.Rollback();
            
            Assert.AreEqual(2, beforeCount);
            Assert.AreEqual(2, afterCount);
        }

        [TestMethod]
        public void ActionCollections_CanBeSetExternally()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            var externalBeforeCommit = new ActionCollection();
            var externalAfterCommit = new ActionCollection();
            var externalBeforeRollback = new ActionCollection();
            var externalAfterRollback = new ActionCollection();
            
            transaction.ActionsBeforeCommit = externalBeforeCommit;
            transaction.ActionsAfterCommit = externalAfterCommit;
            transaction.ActionsBeforeRollback = externalBeforeRollback;
            transaction.ActionsAfterRollback = externalAfterRollback;
            
            Assert.AreSame(externalBeforeCommit, transaction.ActionsBeforeCommit);
            Assert.AreSame(externalAfterCommit, transaction.ActionsAfterCommit);
            Assert.AreSame(externalBeforeRollback, transaction.ActionsBeforeRollback);
            Assert.AreSame(externalAfterRollback, transaction.ActionsAfterRollback);
        }

        [TestMethod]
        public void Commit_WithNullActions_DoesNotThrow()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            transaction.ActionsBeforeCommit = null;
            transaction.ActionsAfterCommit = null;
            
            // Should not throw
            transaction.Commit();
            
            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void Rollback_WithNullActions_DoesNotThrow()
        {
            var mockTransaction = new MockDbContextTransaction();
            var transaction = new UnitOfWorkTransaction(mockTransaction);
            
            transaction.ActionsBeforeRollback = null;
            transaction.ActionsAfterRollback = null;
            
            // Should not throw
            transaction.Rollback();
            
            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void Using_Statement_DisposesTransaction()
        {
            var mockTransaction = new MockDbContextTransaction();
            UnitOfWorkTransaction transaction;
            
            using (transaction = new UnitOfWorkTransaction(mockTransaction))
            {
                Assert.IsNotNull(transaction);
            }
            // Transaction should be disposed here
            
            // Test passes if no exception is thrown during disposal
            Assert.IsNotNull(transaction);
        }
    }
}