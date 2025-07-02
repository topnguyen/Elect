namespace Elect.Data.EF.Interfaces.DbContext
{
    public interface IDbContext : IDisposable, IInfrastructure<IServiceProvider>
    {
        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        IModel Model { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        #region Save
        [DebuggerStepThrough]
        int SaveChanges();
        [DebuggerStepThrough]
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        #endregion
        #region Entry
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
        #endregion
        #region Add
        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Add(object entity);
        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default (CancellationToken)) where TEntity : class;
        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);
        void AddRange(params object[] entities);
        void AddRange(IEnumerable<object> entities);
        Task AddRangeAsync(params object[] entities);
        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = new CancellationToken());
        #endregion
        #region Attach
        EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Attach(object entity);
        void AttachRange(params object[] entities);
        void AttachRange(IEnumerable<object> entities);
        #endregion
        #region Update
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Update(object entity);
        void UpdateRange(params object[] entities);
        void UpdateRange(IEnumerable<object> entities);
        #endregion
        #region Remove
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Remove(object entity);
        void RemoveRange(params object[] entities);
        void RemoveRange(IEnumerable<object> entities);
        #endregion
        #region Find
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;
        object Find(Type entityType, params object[] keyValues);
        ValueTask<object> FindAsync(Type entityType, params object[] keyValues);
        ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);
        #endregion
        #region SQL Command
        DbCommand CreateCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters);
        void ExecuteCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters);
        List<T> ExecuteCommand<T>(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters) where T : class, new();
        #endregion
    }
}
