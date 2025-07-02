namespace Elect.Data.EF.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        #region Refresh
        void RefreshEntity(T entity);
        #endregion
        #region Get
        IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        T GetSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        #endregion
        #region Add
        T Add(T entity);
        #endregion
        #region Update
        void Update(T entity, params Expression<Func<T, object>>[] changedProperties);
        void Update(T entity, params string[] changedProperties);
        void Update(T entity);
        #endregion
        #region Delete
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        #endregion
    }
}
