namespace Elect.Test.Data.EF.TestInfrastructure
{
    public class TestRepository : Repository<TestEntity>
    {
        public TestRepository(IDbContext dbContext) : base(dbContext) { }
    }

    public class TestBaseEntityRepository : BaseEntityRepository<TestBaseEntity>
    {
        public TestBaseEntityRepository(IDbContext dbContext) : base(dbContext) { }
    }

    public class TestEntityRepository : EntityRepository<TestEntity>
    {
        public TestEntityRepository(IDbContext dbContext) : base(dbContext) { }
    }

    public class TestStringEntityRepository : EntityStringRepository<TestStringEntity>
    {
        public TestStringEntityRepository(IDbContext dbContext) : base(dbContext) { }
    }

    public class TestVersionEntityRepository : EntityRepository<TestVersionEntity, int>
    {
        public TestVersionEntityRepository(IDbContext dbContext) : base(dbContext) { }
    }
}