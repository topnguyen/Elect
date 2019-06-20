#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
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

        /// <summary>
        ///  Get Entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isIncludeDeleted">[Note] We use query.IgnoreQueryFilters() when <paramref name="isIncludeDeleted"/> is <c>true</c></param> 
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        /// <remarks>[Note] We use query.IgnoreQueryFilters() when <paramref name="isIncludeDeleted"/> is <c>true</c></remarks>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, bool isIncludeDeleted = false,
            params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate = null, bool isIncludeDeleted = false,
            params Expression<Func<TEntity, object>>[] includeProperties);

        #endregion

        #region Add

        /// <summary>
        ///     Add Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set DeletedTime to <c>null</c>, set LastUpdatedTime and CreatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the DeletedTime, LastUpdatedTime and CreatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        TEntity Add(TEntity entity);

        /// <summary>
        ///     Add Entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set DeletedTime to <c>null</c>, set LastUpdatedTime and CreatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the DeletedTime, LastUpdatedTime and CreatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        List<TEntity> AddRange(params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        ///     Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changedProperties">Specific properties changed</param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set LastUpdatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the LastUpdatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] changedProperties);

        /// <summary>
        ///     Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changedProperties">Specific properties changed</param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set LastUpdatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the LastUpdatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void Update(TEntity entity, params string[] changedProperties);

        /// <summary>
        ///     Update Entity by whole properties
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>
        ///     <para>We will auto set LastUpdatedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the LastUpdatedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void Update(TEntity entity);

        #endregion

        #region Delete

        void Delete(TEntity entity, bool isPhysicalDelete = false);

        /// <summary>
        ///     Delete Where by match condition of predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isPhysicalDelete"></param>
        /// <remarks>
        ///     <para>When isPhysicalDelete is <c>true</c>, it's mean auto include soft delete record in query/predicate</para>
        ///     <para>We will auto set DeletedTime to <c>DateTimeOffset.UtcNow</c> if not set before</para>
        ///     <para>You can override the DeletedTime by override StandardizeEntities in UnitOfWork/BaseEntityUnitOfWork</para>
        /// </remarks>
        void DeleteWhere(Expression<Func<TEntity, bool>> predicate, bool isPhysicalDelete = false);

        #endregion
    }
}