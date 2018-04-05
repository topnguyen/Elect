using Elect.Data.EF.Interfaces.DbContext;
using Elect.DI.Attributes;

namespace Elect.Test.AspNetCore.Data
{
    [ScopedDependency(ServiceType = typeof(UnitOfWork))]
    public class UnitOfWork : Elect.Data.EF.Services.UnitOfWork.UnitOfWork
    {
        public UnitOfWork(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}