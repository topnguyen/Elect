using Elect.Data.EF.Interfaces.DbContext;
using System;

namespace Elect.Test.AspNetCore.Data
{
    public class UnitOfWork : Elect.Data.EF.Services.UnitOfWork.UnitOfWork
    {
        public UnitOfWork(IDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
        }
    }
}