namespace Elect.Sample.Data.EF.Services
{
    public class UnitOfWork : Elect.Data.EF.Services.UnitOfWork.BaseEntityUnitOfWork, IUnitOfWork
    {
        protected readonly IServiceProvider ServiceProvider;
        protected ConcurrentDictionary<Type, object> Repositories = new ConcurrentDictionary<Type, object>();
        public UnitOfWork(Elect.Data.EF.Interfaces.DbContext.IDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext)
        {
            ServiceProvider = serviceProvider;
        }
        public IRepository<T> GetRepository<T>() where T : BaseEntity, new()
        {
            if (!Repositories.TryGetValue(typeof(IRepository<T>), out var repository))
            {
                Repositories[typeof(IRepository<T>)] = repository = ServiceProvider.GetRequiredService<IRepository<T>>();
            }
            return repository as IRepository<T>;
        }
        protected override void StandardizeEntities()
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
                if (entry.Entity is BaseEntity baseEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        baseEntity.DeletedTime = null;
                        baseEntity.LastUpdatedTime = baseEntity.CreatedTime = ObjHelper.ReplaceNullOrDefault(baseEntity.CreatedTime, dateTimeNow);
                    }
                    else
                    {
                        if (baseEntity.DeletedTime != null)
                        {
                            baseEntity.DeletedTime = ObjHelper.ReplaceNullOrDefault(baseEntity.DeletedTime, dateTimeNow);
                        }
                        else
                        {
                            baseEntity.LastUpdatedTime = ObjHelper.ReplaceNullOrDefault(baseEntity.LastUpdatedTime, dateTimeNow);
                        }
                    }
                }
                if (!(entry.Entity is Entity entity))
                {
                    continue;
                }
                // Add more information if Entry is Entity Type
                Guid? loggedInUserId = null; // TODO implementation to get logged in user id
                if (entry.State == EntityState.Added)
                {
                    entity.LastUpdatedBy = entity.CreatedBy = entity.CreatedBy;
                }
                else
                {
                    if (entity.DeletedTime != null)
                    {
                        entity.DeletedBy = entity.DeletedBy ?? loggedInUserId;
                    }
                    else
                    {
                        entity.LastUpdatedBy = entity.LastUpdatedBy ?? loggedInUserId;
                    }
                }
            }
        }
    }
}
