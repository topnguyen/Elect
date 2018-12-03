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

using System;
using System.Collections.Generic;
using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : IDbContext
    {
        public Func<IEnumerable<EntityEntry>, bool> BeforeSaveChanges { get; set; }
        
        protected readonly TDbContext DbContext;

        protected UnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Transaction

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
            return new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(cancellationToken)
                .ConfigureAwait(true));
        }

        public virtual async Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(true));
        }

        #endregion

        #region Save

        public virtual int SaveChanges()
        {
            if (BeforeSaveChanges == null)
            {
                return DbContext.SaveChanges();
            }
            
            var isContinue = BeforeSaveChanges(DbContext.ChangeTracker.Entries());

            return isContinue ? DbContext.SaveChanges() : default;

        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            if (BeforeSaveChanges == null)
            {
                return DbContext.SaveChanges(acceptAllChangesOnSuccess);
            }
            
            var isContinue = BeforeSaveChanges(DbContext.ChangeTracker.Entries());

            return isContinue ? DbContext.SaveChanges(acceptAllChangesOnSuccess) : default;
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (BeforeSaveChanges == null)
            {
                return DbContext.SaveChangesAsync(cancellationToken);
            }
            
            var isContinue = BeforeSaveChanges(DbContext.ChangeTracker.Entries());

            return isContinue ? DbContext.SaveChangesAsync(cancellationToken) : default;
        }

        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            if (BeforeSaveChanges == null)
            {
                return DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            
            var isContinue = BeforeSaveChanges(DbContext.ChangeTracker.Entries());

            return isContinue ? DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken) : default;
        }

        #endregion

        #region SQL Command

        public DbCommand CreateCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            return DbContext.CreateCommand(text, type, parameters);
        }

        public void ExecuteCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            DbContext.ExecuteCommand(text, type, parameters);
        }

        public List<T> ExecuteCommand<T>(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters) where T : class, new()
        {
            return DbContext.ExecuteCommand<T>(text, type, parameters);
        }

        #endregion
    }
}