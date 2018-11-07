﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> BaseEntityRepository_T_.cs </Name>
//         <Created> 24/03/2018 10:38:57 PM </Created>
//         <Key> 145230f3-7472-4013-8076-3249443e96e9 </Key>
//     </File>
//     <Summary>
//         BaseEntityRepository_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Interfaces.Repository;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Elect.Data.EF.Services.Repository
{
    public abstract class BaseEntityRepository<T> : Repository<T>, IBaseEntityRepository<T> where T : BaseEntity, new()
    {
        protected BaseEntityRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Get

        public virtual T GetSingle(Expression<Func<T, bool>> predicate, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            return Get(predicate, isIncludeDeleted, includeProperties).FirstOrDefault();
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = DbSet.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();

            if (includeProperties?.Any() == true)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // NOTE: Don't use Query Filter (query.IgnoreQueryFilters()), it affect to load data
            //       business logic. Currently not flexible, please check https://github.com/aspnet/EntityFrameworkCore/issues/8576
            query = isIncludeDeleted ? query : query.Where(x => x.DeletedTime == null);

            return query;
        }

        #endregion

        #region Add

        public override T Add(T entity)
        {
            entity.DeletedTime = null;
            entity.LastUpdatedTime = entity.CreatedTime = ObjHelper.ReplaceNullOrDefault(entity.CreatedTime, DateTimeOffset.UtcNow);
            entity = DbSet.Add(entity).Entity;
            return entity;
        }

        public virtual List<T> AddRange(params T[] lisT)
        {
            var dateTimeUtcNow = DateTimeOffset.UtcNow;

            List<T> listAddedEntity = new List<T>();

            foreach (var entity in lisT)
            {
                entity.CreatedTime = dateTimeUtcNow;

                var addedEntity = Add(entity);

                listAddedEntity.Add(addedEntity);
            }

            return listAddedEntity;
        }

        #endregion

        #region Update

        public override void Update(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            TryAttach(entity);

            changedProperties = changedProperties?.Distinct().ToArray();

            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);

            if (changedProperties?.Any() == true)
            {
                DbContext.Entry(entity).Property(x => x.LastUpdatedTime).IsModified = true;

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

        public override void Update(T entity, params string[] changedProperties)
        {
            TryAttach(entity);

            changedProperties = changedProperties?.Distinct().ToArray();

            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);

            if (changedProperties?.Any() == true)
            {
                DbContext.Entry(entity).Property(x => x.LastUpdatedTime).IsModified = true;

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

        public override void Update(T entity)
        {
            TryAttach(entity);

            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);

            DbContext.Entry(entity).State = EntityState.Modified;
        }

        #endregion

        #region Delete

        public virtual void Delete(T entity, bool isPhysicalDelete = false)
        {
            try
            {
                TryAttach(entity);

                if (!isPhysicalDelete)
                {
                    entity.DeletedTime = ObjHelper.ReplaceNullOrDefault(entity.DeletedTime, DateTimeOffset.UtcNow);

                    DbContext.Entry(entity).Property(x => x.DeletedTime).IsModified = true;
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

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate, bool isPhysicalDelete = false)
        {
            var entities = Get(predicate, isPhysicalDelete).AsEnumerable();

            foreach (var entity in entities)
            {
                Delete(entity, isPhysicalDelete);
            }
        }

        #endregion
    }
}