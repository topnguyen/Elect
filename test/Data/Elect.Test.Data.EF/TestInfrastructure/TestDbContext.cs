namespace Elect.Test.Data.EF.TestInfrastructure
{
    public class TestDbContext : ElectDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        
        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<TestStringEntity> TestStringEntities { get; set; }
        public DbSet<TestVersionEntity> TestVersionEntities { get; set; }
        public DbSet<TestBaseEntity> TestBaseEntities { get; set; }
        public DbSet<ChildEntity> ChildEntities { get; set; }
    }

    public class ChildEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int TestEntityId { get; set; }
        public TestEntity TestEntity { get; set; }
    }

    public class TestEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ChildEntity> Children { get; set; } = new List<ChildEntity>();
    }

    public class TestStringEntity : StringEntity  
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TestVersionEntity : VersionEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TestBaseEntity : BaseEntity
    {
        public int Id { get; set; } // Add primary key for EF Core
        public string Name { get; set; }
        public string Description { get; set; }
    }
}