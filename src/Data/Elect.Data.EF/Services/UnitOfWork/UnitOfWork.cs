namespace Elect.Data.EF.Services.UnitOfWork
{
    public abstract class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : IDbContext
    {
        public FuncCollection<IEnumerable<EntityEntry>, bool> FunctionsBeforeSaveChanges { get; } =
            new FuncCollection<IEnumerable<EntityEntry>, bool>();
        public ActionCollection<EntityStateCollection> ActionsAfterSaveChanges { get; } =
            new ActionCollection<EntityStateCollection>();
        public ActionCollection ActionsBeforeCommit { get; } = new ActionCollection();
        public ActionCollection ActionsAfterCommit { get; } = new ActionCollection();
        public ActionCollection ActionsBeforeRollback { get; } = new ActionCollection();
        public ActionCollection ActionsAfterRollback { get; } = new ActionCollection();
        protected readonly TDbContext DbContext;
        protected UnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
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
            var isContinue = true;
            if (FunctionsBeforeSaveChanges?.Get()?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges.Get())
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
            if (!isContinue || ActionsAfterSaveChanges?.Get()?.Any() != true)
            {
                return result;
            }
            foreach (var actionModel in ActionsAfterSaveChanges.Get())
            {
                actionModel?.Action?.Invoke(entityStatesModel);
            }
            return result;
        }
        public virtual int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var isContinue = true;
            if (FunctionsBeforeSaveChanges?.Get()?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges.Get())
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
            if (!isContinue || ActionsAfterSaveChanges?.Get()?.Any() != true)
            {
                return result;
            }
            foreach (var actionModel in ActionsAfterSaveChanges.Get())
            {
                actionModel?.Action?.Invoke(entityStatesModel);
            }
            return result;
        }
        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var isContinue = true;
            if (FunctionsBeforeSaveChanges?.Get()?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges.Get())
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
            if (!isContinue || ActionsAfterSaveChanges?.Get()?.Any() != true)
            {
                return result;
            }
            foreach (var actionModel in ActionsAfterSaveChanges.Get())
            {
                actionModel?.Action?.Invoke(entityStatesModel);
            }
            return result;
        }
        public virtual Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            bool isContinue = true;
            if (FunctionsBeforeSaveChanges?.Get()?.Any() == true)
            {
                foreach (var funcModel in FunctionsBeforeSaveChanges.Get())
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
            if (!isContinue || ActionsAfterSaveChanges?.Get()?.Any() != true)
            {
                return result;
            }
            foreach (var actionModel in ActionsAfterSaveChanges.Get())
            {
                actionModel?.Action?.Invoke(entityStatesModel);
            }
            return result;
        }
        #endregion
        protected virtual EntityStateCollection SplitEntity()
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
            var entityStatesModel = new EntityStateCollection
            {
                ListAdded = listEntry.Where(x => x.State == EntityState.Added).Select(x => new EntityStateModel(x))
                    .ToList(),
                ListModified = listEntry.Where(x => x.State == EntityState.Modified)
                    .Select(x => new EntityStateModel(x)).ToList(),
                ListDeleted = listEntry.Where(x => x.State == EntityState.Deleted).Select(x => new EntityStateModel(x))
                    .ToList()
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
