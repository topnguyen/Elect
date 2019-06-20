﻿#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringEntityRepository_T_.cs </Name>
//         <Created> 24/03/2018 10:53:19 PM </Created>
//         <Key> 8e1b8fd5-6113-4f24-bec4-362ce066bea5 </Key>
//     </File>
//     <Summary>
//         StringEntityRepository_T_.cs is a part of Elect
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
    public abstract class EntityStringRepository<T> : BaseEntityRepository<T>, IStringEntityRepository<T>
        where T : StringEntity, new()
    {
        protected EntityStringRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Update

        public override void Update(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            TryAttach(entity);

            changedProperties = changedProperties?.Distinct().ToArray();

            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);

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

        public override void Update(T entity, params string[] changedProperties)
        {
            TryAttach(entity);

            changedProperties = changedProperties?.Distinct().ToArray();

            entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, DateTimeOffset.UtcNow);

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

        public void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData,
            params Expression<Func<T, object>>[] changedProperties)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;

            var entities = Get(predicate).Select(x => new T {Id = x.Id}).ToList();

            foreach (var entity in entities)
            {
                var oldEntity = entityNewData.Clone();
                
                oldEntity.Id = entity.Id;
                
                oldEntity.LastUpdatedTime = utcNow;
                
                Update(oldEntity, changedProperties);
            }
        }

        public void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData, params string[] changedProperties)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;

            var entities = Get(predicate).Select(x => new T {Id = x.Id}).ToList();

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

        public override void Delete(T entity, bool isPhysicalDelete = false)
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

        public override void DeleteWhere(Expression<Func<T, bool>> predicate, bool isPhysicalDelete = false)
        {
            var utcNow = DateTimeOffset.UtcNow;

            // When isPhysicalDelete is true, it mean include soft delete record in query
            var entities = Get(predicate, isPhysicalDelete).Select(x => new T {Id = x.Id}).ToList();

            foreach (var entity in entities)
            {
                entity.DeletedTime = utcNow;
                
                Delete(entity, isPhysicalDelete);
            }
        }

        public void DeleteWhere(List<string> ids, bool isPhysicalDelete = false)
        {
            var utcNow = DateTimeOffset.UtcNow;

            foreach (var id in ids)
            {
                var entity = new T
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