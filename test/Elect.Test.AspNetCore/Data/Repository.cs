using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Interfaces.Repository;
using Elect.DI.Attributes;

namespace Elect.Test.AspNetCore.Data
{
    [ScopedDependency(ServiceType = typeof(IRepository<>))]
    public class Repository<T> : Elect.Data.EF.Services.Repository.Repository<T> where T : class, new()
    {
        public Repository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}