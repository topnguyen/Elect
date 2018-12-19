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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elect.Core.LinqUtils;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : IDbContext
    {
        public List<FuncModel<IEnumerable<EntityEntry>, bool>> FunctionsBeforeSaveChanges { get; set; } =
            new List<FuncModel<IEnumerable<EntityEntry>, bool>>();

        public List<ActionModel<EntityStatesModel>> ActionsAfterSaveChanges { get; set; } =
            new List<ActionModel<EntityStatesModel>>();

        public List<ActionModel> ActionsBeforeCommit { get; set; } = new List<ActionModel>();

        public List<ActionModel> ActionsAfterCommit { get; set; } = new List<ActionModel>();

        public List<ActionModel> ActionsBeforeRollback { get; set; } = new List<ActionModel>();

        public List<ActionModel> ActionsAfterRollback { get; set; } = new List<ActionModel>();

        protected readonly TDbContext DbContext;

        protected UnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Save Changes Callback

        // Before Save Changes

        public string AddFunctionBeforeSaveChanges(Func<IEnumerable<EntityEntry>, bool> func)
        {
            var funcModel = new FuncModel<IEnumerable<EntityEntry>, bool>(func);

            FunctionsBeforeSaveChanges.Add(funcModel);

            return funcModel.Id;
        }

        public void RemoveFunctionBeforeSaveChanges(string funcId)
        {
            FunctionsBeforeSaveChanges = FunctionsBeforeSaveChanges.RemoveWhere(x => x.Id == funcId).ToList();
        }

        public void EmptyFunctionsBeforeSaveChanges()
        {
            if (FunctionsBeforeSaveChanges?.Any() != true)
            {
                return;
            }

            FunctionsBeforeSaveChanges = FunctionsBeforeSaveChanges.RemoveWhere(x => x.Func != null).ToList();
        }

        // After Save Changes

        public string AddActionAfterSaveChanges(Action<EntityStatesModel> action)
        {
            var actionModel = new ActionModel<EntityStatesModel>(action);

            ActionsAfterSaveChanges.Add(actionModel);

            return actionModel.Id;
        }

        public void RemoveActionAfterSaveChanges(string actionId)
        {
            ActionsAfterSaveChanges = ActionsAfterSaveChanges.RemoveWhere(x => x.Id == actionId).ToList();
        }

        public void EmptyActionsAfterSaveChanges()
        {
            if (ActionsAfterSaveChanges?.Any() != true)
            {
                return;
            }

            ActionsAfterSaveChanges = ActionsAfterSaveChanges.RemoveWhere(x => x.Action != null).ToList();
        }

        #endregion

        #region Commit Callback

        // Before Commit

        public string AddActionBeforeCommit(Action action)
        {
            var actionModel = new ActionModel(action);

            ActionsBeforeCommit.Add(actionModel);

            return actionModel.Id;
        }

        public void RemoveActionBeforeCommit(string actionId)
        {
            ActionsBeforeCommit = ActionsBeforeCommit.RemoveWhere(x => x.Id == actionId).ToList();
        }

        public void EmptyActionsBeforeCommit()
        {
            if (ActionsBeforeCommit?.Any() != true)
            {
                return;
            }

            ActionsBeforeCommit = ActionsBeforeCommit.RemoveWhere(x => x.Action != null).ToList();
        }

        // After Commit

        public string AddActionAfterCommit(Action action)
        {
            var actionModel = new ActionModel(action);

            ActionsAfterCommit.Add(actionModel);

            return actionModel.Id;
        }

        public void RemoveActionAfterCommit(string actionId)
        {
            ActionsAfterCommit = ActionsAfterCommit.RemoveWhere(x => x.Id == actionId).ToList();
        }

        public void EmptyActionsAfterCommit()
        {
            if (ActionsAfterCommit?.Any() != true)
            {
                return;
            }

            ActionsAfterCommit = ActionsAfterCommit.RemoveWhere(x => x.Action != null).ToList();
        }

        #endregion

        #region Rollback Callback

        // Before Rollback

        public string AddActionBeforeRollback(Action action)
        {
            var actionModel = new ActionModel(action);

            ActionsBeforeRollback.Add(actionModel);

            return actionModel.Id;
        }

        public void RemoveActionBeforeRollback(string actionId)
        {
            ActionsBeforeRollback = ActionsBeforeRollback.RemoveWhere(x => x.Id == actionId).ToList();
        }

        public void EmptyActionsBeforeRollback()
        {
            if (ActionsBeforeRollback?.Any() != true)
            {
                return;
            }

            ActionsBeforeRollback = ActionsBeforeRollback.RemoveWhere(x => x.Action != null).ToList();
        }

        // After Rollback

        public string AddActionAfterRollback(Action action)
        {
            var actionModel = new ActionModel(action);

            ActionsAfterRollback.Add(actionModel);

            return actionModel.Id;
        }

        public void RemoveActionAfterRollback(string actionId)
        {
            ActionsAfterRollback = ActionsAfterRollback.RemoveWhere(x => x.Id == actionId).ToList();
        }

        public void EmptyActionsAfterRollback()
        {
            if (ActionsAfterRollback?.Any() != true)
            {
                return;
            }

            ActionsAfterRollback = ActionsAfterRollback.RemoveWhere(x => x.Action != null).ToList();
        }

        #endregion

        #region Transaction

        public virtual IUnitOfWorkTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            var transaction = new UnitOfWorkTransaction(DbContext.Database.BeginTransaction(isolationLevel))
            {
                ActionsBeforeCommit = ActionsBeforeCommit,
                ActionsAfterCommit = ActionsAfterCommit,
                ActionsBeforeRollback = ActionsBeforeRollback,
                ActionsAfterRollback = ActionsAfterRollback
            };

            return transaction;
        }

        public virtual IUnitOfWorkTransaction BeginTransaction()
        {
            var transaction = new UnitOfWorkTransaction(DbContext.Database.BeginTransaction())
            {
                ActionsBeforeCommit = ActionsBeforeCommit,
                ActionsAfterCommit = ActionsAfterCommit,
                ActionsBeforeRollback = ActionsBeforeRollback,
                ActionsAfterRollback = ActionsAfterRollback
            };

            return transaction;
        }

        public virtual async Task<IUnitOfWorkTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default)
        {
            var transaction =
                new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(cancellationToken)
                    .ConfigureAwait(true))
                {
                    ActionsBeforeCommit = ActionsBeforeCommit,
                    ActionsAfterCommit = ActionsAfterCommit,
                    ActionsBeforeRollback = ActionsBeforeRollback,
                    ActionsAfterRollback = ActionsAfterRollback
                };

            return transaction;
        }

        public virtual async Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default)
        {
            var transaction = new UnitOfWorkTransaction(await DbContext.Database
                .BeginTransactionAsync(isolationLevel, cancellationToken).ConfigureAwait(true))
            {
                ActionsBeforeCommit = ActionsBeforeCommit,
                ActionsAfterCommit = ActionsAfterCommit,
                ActionsBeforeRollback = ActionsBeforeRollback,
                ActionsAfterRollback = ActionsAfterRollback
            };

            return transaction;
        }

        #endregion

        #region Save

        public virtual int SaveChanges()
        {
            bool isContinue = true;

            if (FunctionsBeforeSaveChanges?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges)
                {
                    var tempContinue = funcModel?.Func?.Invoke(DbContext.ChangeTracker.Entries()) ?? true;

                    if (!tempContinue)
                    {
                        isContinue = false;
                    }
                }
            }

            var entityStatesModel = SplitEntity();

            var result = isContinue ? DbContext.SaveChanges() : default;

            if (isContinue && ActionsAfterSaveChanges?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterSaveChanges)
                {
                    actionModel?.Action?.Invoke(entityStatesModel);
                }
            }

            return result;
        }

        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            bool isContinue = true;

            if (FunctionsBeforeSaveChanges?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges)
                {
                    var tempContinue = funcModel?.Func?.Invoke(DbContext.ChangeTracker.Entries()) ?? true;

                    if (!tempContinue)
                    {
                        isContinue = false;
                    }
                }
            }

            var entityStatesModel = SplitEntity();

            var result = isContinue ? DbContext.SaveChanges(acceptAllChangesOnSuccess) : default;

            if (isContinue && ActionsAfterSaveChanges?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterSaveChanges)
                {
                    actionModel?.Action?.Invoke(entityStatesModel);
                }
            }

            return result;
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            bool isContinue = true;

            if (FunctionsBeforeSaveChanges?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges)
                {
                    var tempContinue = funcModel?.Func?.Invoke(DbContext.ChangeTracker.Entries()) ?? true;

                    if (!tempContinue)
                    {
                        isContinue = false;
                    }
                }
            }

            var entityStatesModel = SplitEntity();

            var result = isContinue ? DbContext.SaveChangesAsync(cancellationToken) : default;

            if (isContinue && ActionsAfterSaveChanges?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterSaveChanges)
                {
                    actionModel?.Action?.Invoke(entityStatesModel);
                }
            }

            return result;
        }

        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            bool isContinue = true;

            if (FunctionsBeforeSaveChanges?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges)
                {
                    var tempContinue = funcModel?.Func?.Invoke(DbContext.ChangeTracker.Entries()) ?? true;

                    if (!tempContinue)
                    {
                        isContinue = false;
                    }
                }
            }

            var entityStatesModel = SplitEntity();

            var result = isContinue
                ? DbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken)
                : default;

            if (isContinue && ActionsAfterSaveChanges?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterSaveChanges)
                {
                    actionModel?.Action?.Invoke(entityStatesModel);
                }
            }

            return result;
        }

        #endregion

        protected virtual EntityStatesModel SplitEntity()
        {
            var listState = new List<EntityState>
            {
                EntityState.Added,
                EntityState.Modified,
                EntityState.Deleted
            };

            var listEntry = DbContext.ChangeTracker.Entries()
                .Where(x => listState.Contains(x.State))
                .ToList();

            EntityStatesModel entityStatesModel = new EntityStatesModel
            {
                ListAdded = listEntry.Where(x => x.State == EntityState.Added).Select(x => x.Entity).ToList(),
                ListModified = listEntry.Where(x => x.State == EntityState.Modified).Select(x => x.Entity).ToList(),
                ListDeleted = listEntry.Where(x => x.State == EntityState.Deleted).Select(x => x.Entity).ToList()
            };

            return entityStatesModel;
        }

        #region SQL Command

        public DbCommand CreateCommand(string text, CommandType type = CommandType.Text,
            params SqlParameter[] parameters)
        {
            return DbContext.CreateCommand(text, type, parameters);
        }

        public void ExecuteCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            DbContext.ExecuteCommand(text, type, parameters);
        }

        public List<T> ExecuteCommand<T>(string text, CommandType type = CommandType.Text,
            params SqlParameter[] parameters) where T : class, new()
        {
            return DbContext.ExecuteCommand<T>(text, type, parameters);
        }

        #endregion
    }
}