using Elect.Data.EF.Interfaces.DbContext;

namespace Elect.Test.AspNetCore.Data
{
    public class UnitOfWork : Elect.Data.EF.Services.UnitOfWork.UnitOfWork
    {
        public UnitOfWork(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}