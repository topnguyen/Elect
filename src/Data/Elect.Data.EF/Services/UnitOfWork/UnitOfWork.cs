#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> UnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:14:25 PM </Created>
//         <Key> 40b48b5c-6fe3-4783-b280-d2a915cc9431 </Key>
//     </File>
//     <Summary>
//         UnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly IDbContext DbContext;

        protected UnitOfWork(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IUnitOfWorkTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new UnitOfWorkTransaction(DbContext.Database.BeginTransaction(isolationLevel));
        }

        public virtual IUnitOfWorkTransaction BeginTransaction()
        {
            return new UnitOfWorkTransaction(DbContext.Database.BeginTransaction());
        }

        public virtual async Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(true));
        }

        public virtual async Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(true));
        }

        #region Save

        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return DbContext.SaveChanges(acceptAllChangesOnSuccess);
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion
    }
}