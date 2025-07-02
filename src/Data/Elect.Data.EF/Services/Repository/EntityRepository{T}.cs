namespace Elect.Data.EF.Services.Repository
{
    public abstract class EntityRepository<T> : EntityRepository<T, int> where T : Entity, new()
    {
        protected EntityRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
