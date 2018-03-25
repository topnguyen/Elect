#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IRepository_T_.cs </Name>
//         <Created> 24/03/2018 9:57:31 PM </Created>
//         <Key> 87edd4a9-1cb7-40fa-a2a8-122b0cdd92f6 </Key>
//     </File>
//     <Summary>
//         IRepository_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Linq;
using System.Linq.Expressions;

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