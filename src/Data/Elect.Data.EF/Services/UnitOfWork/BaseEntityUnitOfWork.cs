#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> BaseEntityUnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:49:53 PM </Created>
//         <Key> ae10c6dd-02e5-4eef-b2d6-c53512b734c2 </Key>
//     </File>
//     <Summary>
//         BaseEntityUnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public abstract class BaseEntityUnitOfWork : UnitOfWork<IDbContext>
    {
        protected BaseEntityUnitOfWork(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Save

        public override int SaveChanges()
        {
            StandardizeEntities();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            StandardizeEntities();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            StandardizeEntities();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            StandardizeEntities();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

        #region Helper

        protected virtual void StandardizeEntities()
        {
            var listState = new List<EntityState>
            {
                EntityState.Added,
                EntityState.Modified
            };

            var listEntryAddUpdate = DbContext.ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && listState.Contains(x.State))
                .Select(x => x).ToList();

            var dateTimeNow = DateTimeOffset.UtcNow;

            foreach (var entry in listEntryAddUpdate)
            {
                if (!(entry.Entity is BaseEntity entity))
                {
                    continue;
                }

                if (entry.State == EntityState.Added)
                {
                    entity.DeletedTime = null;
                    entity.LastUpdatedTime = entity.CreatedTime = ObjHelper.ReplaceNullOrDefault(entity.CreatedTime, dateTimeNow);
                }
                else
                {
                    if (entity.DeletedTime != null)
                    {
                        entity.DeletedTime = ObjHelper.ReplaceNullOrDefault(entity.DeletedTime, dateTimeNow);
                    }
                    else
                    {
                        entity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(entity.LastUpdatedTime, dateTimeNow);
                    }
                }
            }
        }
        
        #endregion
    }
}