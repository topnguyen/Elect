#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IBaseEntityRepository_T_.cs </Name>
//         <Created> 24/03/2018 10:40:08 PM </Created>
//         <Key> 4f0d8d00-7d45-48ab-9e0e-a56687bb2210 </Key>
//     </File>
//     <Summary>
//         IBaseEntityRepository_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Data.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Elect.Data.EF.Interfaces.Repository
{
    public interface IBaseEntityRepository<TEntity> where TEntity : BaseEntity
    {
        #region Refresh

        void RefreshEntity(TEntity entity);

        #endregion

        #region Get

        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, bool isIncludeDeleted = false,
            params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate = null, bool isIncludeDeleted = false,
            params Expression<Func<TEntity, object>>[] includeProperties);

        #endregion

        #region Add

        TEntity Add(TEntity entity);

        List<TEntity> AddRange(params TEntity[] listEntity);

        #endregion

        #region Update

        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] changedProperties);

        void Update(TEntity entity, params string[] changedProperties);

        void Update(TEntity entity);

        #endregion

        #region Delete

        void Delete(TEntity entity, bool isPhysicalDelete = false);

        /// <summary>
        ///     Delete Where by match condition of predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isPhysicalDelete"></param>
        /// <remarks>When isPhysicalDelete is <c>true</c>, it's mean auto include soft delete record in query/predicate</remarks>
        void DeleteWhere(Expression<Func<TEntity, bool>> predicate, bool isPhysicalDelete = false);

        #endregion
    }
}