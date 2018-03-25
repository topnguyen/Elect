using Elect.Data.EF.Interfaces.DbContext;

namespace Elect.Test.AspNetCore.Data
{
    public class Repository<T> : Elect.Data.EF.Services.Repository.Repository<T> where T : class, new()
    {
        public Repository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}