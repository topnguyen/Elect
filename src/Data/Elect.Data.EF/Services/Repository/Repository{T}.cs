namespace Elect.Data.EF.Services.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly IDbContext DbContext;
        private DbSet<T> _dbSet;
        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet != null)
                {
                    return _dbSet;
                }
                _dbSet = DbContext.Set<T>();
                return _dbSet;
            }
        }
        protected Repository(IDbContext dbContext)
        {
            DbContext = dbContext;
        }
        #region Refresh
        public virtual void RefreshEntity(T entity)
        {
            DbContext.Entry(entity).Reload();
        }
        #endregion
        #region Get
        public virtual IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbSet.AsNoTracking();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbSet.AsNoTracking();
            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() == true)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return predicate == null ? query : query.Where(predicate);
        }
        public virtual T GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return Get(predicate, includeProperties).FirstOrDefault();
        }
        #endregion
        #region Add
        public virtual T Add(T entity)
        {
            entity = DbSet.Add(entity).Entity;
            return entity;
        }
        #endregion
        #region Update
        public virtual void Update(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            TryAttach(entity);
            changedProperties = changedProperties?.Distinct().ToArray();
            if (changedProperties?.Any() == true)
            {
                foreach (var property in changedProperties)
                {
                    DbContext.Entry(entity).Property(property).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }
        public virtual void Update(T entity, params string[] changedProperties)
        {
            TryAttach(entity);
            changedProperties = changedProperties?.Distinct().ToArray();
            if (changedProperties?.Any() == true)
            {
                foreach (var property in changedProperties)
                {
                    DbContext.Entry(entity).Property(property).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }
        public virtual void Update(T entity)
        {
            TryAttach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        #endregion
        #region Delete
        public virtual void Delete(T entity)
        {
            try
            {
                TryAttach(entity);
                DbSet.Remove(entity);
            }
            catch (Exception)
            {
                RefreshEntity(entity);
                throw;
            }
        }
        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entities = Get(predicate).AsEnumerable();
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
        #endregion
        #region Helper
        protected void TryAttach(T entity)
        {
            try
            {
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }
            catch
            {
                // ignored
            }
        }
        protected void GetEntityEntries(out List<EntityEntry> listEntryAdded, out List<EntityEntry> listEntryModified, out List<EntityEntry> listEntryDeleted)
        {
            var listState = new List<EntityState>
            {
                EntityState.Added,
                EntityState.Modified,
                EntityState.Deleted
            };
            var listEntry = DbContext.ChangeTracker.Entries().Where(x => x.Entity is T && listState.Contains(x.State)).ToList();
            listEntryAdded = listEntry.Where(x => x.State == EntityState.Added).ToList();
            listEntryModified = listEntry.Where(x => x.State == EntityState.Modified).ToList();
            listEntryDeleted = listEntry.Where(x => x.State == EntityState.Deleted).ToList();
        }
        protected void GetEntities(out List<T> listEntityAdded, out List<T> listEntityModified, out List<T> listEntityDeleted)
        {
            GetEntityEntries(out var listEntryAdded, out var listEntryModified, out var listEntryDeleted);
            listEntityAdded = listEntryAdded.Cast<T>().ToList();
            listEntityModified = listEntryModified.Cast<T>().ToList();
            listEntityDeleted = listEntryDeleted.Cast<T>().ToList();
        }
        #endregion
    }
}
