#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EntityUnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:44:13 PM </Created>
//         <Key> f05aaad4-d362-4062-8cc1-15a680fdf868 </Key>
//     </File>
//     <Summary>
//         EntityUnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Interfaces.Repository;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Elect.Data.EF.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public class EntityUnitOfWork : BaseUnitOfWork, IEntityUnitOfWork
    {
        protected readonly IServiceProvider ServiceProvider;

        protected ConcurrentDictionary<Type, object> Repositories = new ConcurrentDictionary<Type, object>();

        public EntityUnitOfWork(IDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext)
        {
            ServiceProvider = serviceProvider;
        }

        public IEntityRepository<T, TKey> GetRepository<T, TKey>() where T : Entity<TKey>, new() where TKey : struct
        {
            if (!Repositories.TryGetValue(typeof(IEntityRepository<T, TKey>), out var repository))
            {
                Repositories[typeof(IEntityRepository<T, TKey>)] = repository = ServiceProvider.GetRequiredService<IEntityRepository<T, TKey>>();
            }

            return repository as IEntityRepository<T, TKey>;
        }
    }
}