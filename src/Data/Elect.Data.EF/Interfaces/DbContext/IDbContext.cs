#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IDbContext.cs </Name>
//         <Created> 24/03/2018 10:22:56 PM </Created>
//         <Key> b3afbd61-b698-48a0-8b5b-8e54d4cce3cf </Key>
//     </File>
//     <Summary>
//         IDbContext.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

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

        Task<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        Task<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default);

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

        Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;

        Task<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;

        object Find(Type entityType, params object[] keyValues);

        Task<object> FindAsync(Type entityType, params object[] keyValues);

        Task<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken);

        #endregion
    }
}