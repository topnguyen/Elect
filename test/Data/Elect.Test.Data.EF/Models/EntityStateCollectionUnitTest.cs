namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class EntityStateCollectionUnitTest
    {
        [TestMethod]
        public void Constructor_InitializesAllLists()
        {
            var collection = new EntityStateCollection();

            Assert.IsNotNull(collection.ListAdded);
            Assert.IsNotNull(collection.ListModified);
            Assert.IsNotNull(collection.ListDeleted);
            Assert.IsInstanceOfType(collection.ListAdded, typeof(List<EntityStateModel>));
            Assert.IsInstanceOfType(collection.ListModified, typeof(List<EntityStateModel>));
            Assert.IsInstanceOfType(collection.ListDeleted, typeof(List<EntityStateModel>));
        }

        [TestMethod]
        public void Constructor_InitializesEmptyLists()
        {
            var collection = new EntityStateCollection();

            Assert.AreEqual(0, collection.ListAdded.Count);
            Assert.AreEqual(0, collection.ListModified.Count);
            Assert.AreEqual(0, collection.ListDeleted.Count);
        }

        [TestMethod]
        public void ListAdded_CanAddItems()
        {
            var collection = new EntityStateCollection();
            var mockEntry = CreateMockEntityEntry();
            var stateModel = new EntityStateModel(mockEntry);

            collection.ListAdded.Add(stateModel);

            Assert.AreEqual(1, collection.ListAdded.Count);
            Assert.AreEqual(stateModel, collection.ListAdded[0]);
        }

        [TestMethod]
        public void ListModified_CanAddItems()
        {
            var collection = new EntityStateCollection();
            var mockEntry = CreateMockEntityEntry();
            var stateModel = new EntityStateModel(mockEntry);

            collection.ListModified.Add(stateModel);

            Assert.AreEqual(1, collection.ListModified.Count);
            Assert.AreEqual(stateModel, collection.ListModified[0]);
        }

        [TestMethod]
        public void ListDeleted_CanAddItems()
        {
            var collection = new EntityStateCollection();
            var mockEntry = CreateMockEntityEntry();
            var stateModel = new EntityStateModel(mockEntry);

            collection.ListDeleted.Add(stateModel);

            Assert.AreEqual(1, collection.ListDeleted.Count);
            Assert.AreEqual(stateModel, collection.ListDeleted[0]);
        }

        [TestMethod]
        public void Lists_AreIndependent()
        {
            var collection = new EntityStateCollection();
            var mockEntry1 = CreateMockEntityEntry();
            var mockEntry2 = CreateMockEntityEntry();
            var mockEntry3 = CreateMockEntityEntry();
            var stateModel1 = new EntityStateModel(mockEntry1);
            var stateModel2 = new EntityStateModel(mockEntry2);
            var stateModel3 = new EntityStateModel(mockEntry3);

            collection.ListAdded.Add(stateModel1);
            collection.ListModified.Add(stateModel2);
            collection.ListDeleted.Add(stateModel3);

            Assert.AreEqual(1, collection.ListAdded.Count);
            Assert.AreEqual(1, collection.ListModified.Count);
            Assert.AreEqual(1, collection.ListDeleted.Count);
            Assert.AreEqual(stateModel1, collection.ListAdded[0]);
            Assert.AreEqual(stateModel2, collection.ListModified[0]);
            Assert.AreEqual(stateModel3, collection.ListDeleted[0]);
        }

        private EntityEntry CreateMockEntityEntry()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new TestDbContext(options);
            var entity = new TestEntity { Name = "Test" };
            context.TestEntities.Add(entity);
            return context.Entry(entity);
        }

        private class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class TestDbContext : EFDbContext
        {
            public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
            public DbSet<TestEntity> TestEntities { get; set; }
        }
    }
}