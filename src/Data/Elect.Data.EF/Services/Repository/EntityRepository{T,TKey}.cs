namespace Elect.Data.EF.Services.Repository
{
    public abstract class EntityRepository<TEntity, TKey> : BaseEntityRepository<TEntity>,
        IEntityRepository<TEntity, TKey> where TEntity : Entity<TKey>, new() where TKey : struct
    {
        protected EntityRepository(IDbContext dbContext) : base(dbContext)
        {
        }
        #region Update
        public override void Update(TEntity entity, params Expression<Func<TEntity, object>>[] changedProperties)
        {
            TryAttach(entity);
            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);
            changedProperties = changedProperties?.Distinct().ToArray();
            if (changedProperties?.Any() == true)
            {
                DbContext.Entry(entity).Property(x => x.LastUpdatedTime).IsModified = true;
                DbContext.Entry(entity).Property(x => x.LastUpdatedBy).IsModified = true;
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
        public override void Update(TEntity entity, params string[] changedProperties)
        {
            TryAttach(entity);
            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);
            changedProperties = changedProperties?.Distinct().ToArray();
            if (changedProperties?.Any() == true)
            {
                DbContext.Entry(entity).Property(x => x.LastUpdatedTime).IsModified = true;
                DbContext.Entry(entity).Property(x => x.LastUpdatedBy).IsModified = true;
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
        public void UpdateWhere(Expression<Func<TEntity, bool>> predicate, TEntity entityNewData,
            params Expression<Func<TEntity, object>>[] changedProperties)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            var entities = Get(predicate).Select(x => new TEntity {Id = x.Id}).ToList();
            foreach (var entity in entities)
            {
                var oldEntity = entityNewData.Clone();
                oldEntity.Id = entity.Id;
                oldEntity.LastUpdatedTime = utcNow;
                Update(oldEntity, changedProperties);
            }
        }
        public void UpdateWhere(Expression<Func<TEntity, bool>> predicate, TEntity entityNewData,
            params string[] changedProperties)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            var entities = Get(predicate).Select(x => new TEntity {Id = x.Id}).ToList();
            foreach (var entity in entities)
            {
                var oldEntity = entityNewData.Clone();
                oldEntity.Id = entity.Id;
                oldEntity.LastUpdatedTime = utcNow;
                Update(oldEntity, changedProperties);
            }
        }
        #endregion
        #region Delete
        public override void Delete(TEntity entity, bool isPhysicalDelete = false)
        {
            try
            {
                TryAttach(entity);
                if (!isPhysicalDelete)
                {
                    entity.DeletedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);
                    DbContext.Entry(entity).Property(x => x.DeletedTime).IsModified = true;
                    DbContext.Entry(entity).Property(x => x.DeletedBy).IsModified = true;
                }
                else
                {
                    DbSet.Remove(entity);
                }
            }
            catch (Exception)
            {
                RefreshEntity(entity);
                throw;
            }
        }
        public override void DeleteWhere(Expression<Func<TEntity, bool>> predicate, bool isPhysicalDelete = false)
        {
            var utcNow = DateTimeOffset.UtcNow;
            // When isPhysicalDelete is true, it mean include soft delete record in query
            var entities = Get(predicate, isPhysicalDelete).Select(x => new TEntity {Id = x.Id}).ToList();
            foreach (var entity in entities)
            {
                entity.DeletedTime = utcNow;
                Delete(entity, isPhysicalDelete);
            }
        }
        public void DeleteWhere(List<TKey> ids, bool isPhysicalDelete = false)
        {
            var utcNow = DateTimeOffset.UtcNow;
            foreach (var id in ids)
            {
                var entity = new TEntity
                {
                    Id = id,
                    DeletedTime = utcNow
                };
                Delete(entity, isPhysicalDelete);
            }
        }
        #endregion
    }
}
