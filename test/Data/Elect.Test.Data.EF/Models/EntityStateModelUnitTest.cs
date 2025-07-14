namespace Elect.Test.Data.EF.Models
{
    [TestClass]
    public class EntityStateModelUnitTest
    {
        private TestDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TestDbContext(options);
        }

        private class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class TestDbContext : EFDbContext
        {
            public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
            public DbSet<TestEntity> TestEntities { get; set; }
        }

        [TestMethod]
        public void Constructor_WithAddedEntity_SetsCorrectState()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = "Test", Age = 25 };
            context.TestEntities.Add(entity);
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            Assert.AreEqual(entity, stateModel.Entity);
            Assert.AreEqual(EntityState.Added, stateModel.State);
        }

        [TestMethod]
        public void Constructor_WithModifiedEntity_SetsCorrectState()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = "Test", Age = 25 };
            context.TestEntities.Add(entity);
            context.SaveChanges();

            entity.Name = "Modified";
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            Assert.AreEqual(entity, stateModel.Entity);
            Assert.AreEqual(EntityState.Modified, stateModel.State);
        }

        [TestMethod]
        public void Constructor_WithDeletedEntity_SetsCorrectState()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = "Test", Age = 25 };
            context.TestEntities.Add(entity);
            context.SaveChanges();

            context.TestEntities.Remove(entity);
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            Assert.AreEqual(entity, stateModel.Entity);
            Assert.AreEqual(EntityState.Deleted, stateModel.State);
        }

        [TestMethod]
        public void Constructor_InitializesModifiedFields()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = "Test", Age = 25 };
            context.TestEntities.Add(entity);
            context.SaveChanges();

            entity.Name = "Modified";
            entity.Age = 30;
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            Assert.IsNotNull(stateModel.ModifiedFields);
            Assert.IsInstanceOfType(stateModel.ModifiedFields, typeof(Dictionary<string, object>));
        }

        [TestMethod]
        public void Constructor_InitializesTempFieldNames()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = "Test", Age = 25 };
            context.TestEntities.Add(entity);
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            Assert.IsNotNull(stateModel.TempFieldNames);
            Assert.IsInstanceOfType(stateModel.TempFieldNames, typeof(List<string>));
        }

        [TestMethod]
        public void Constructor_HandlesDuplicateModifiedFields()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = "Test", Age = 25 };
            context.TestEntities.Add(entity);
            context.SaveChanges();

            entity.Name = "Modified";
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            // Should not throw even with potential duplicate processing
            Assert.IsNotNull(stateModel.ModifiedFields);
        }

        [TestMethod]
        public void Constructor_HandlesNullPropertyGracefully()
        {
            using var context = CreateInMemoryContext();
            var entity = new TestEntity { Name = null, Age = 25 };
            context.TestEntities.Add(entity);
            var entry = context.Entry(entity);

            var stateModel = new EntityStateModel(entry);

            Assert.IsNotNull(stateModel);
            Assert.AreEqual(entity, stateModel.Entity);
        }
    }
}