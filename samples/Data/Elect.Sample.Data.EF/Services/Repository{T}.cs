namespace Elect.Sample.Data.EF.Services
{
    public class Repository<T> : Elect.Data.EF.Services.Repository.BaseEntityRepository<T>, IRepository<T> where T : BaseEntity, new()
    {
        public Repository(Elect.Data.EF.Interfaces.DbContext.IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
