#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> UnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:38:56 PM </Created>
//         <Key> a2068b0d-1061-4cf2-a363-60be1a7aeae5 </Key>
//     </File>
//     <Summary>
//         UnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Interfaces.Repository;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        protected readonly IServiceProvider ServiceProvider;

        protected ConcurrentDictionary<Type, object> Repositories = new ConcurrentDictionary<Type, object>();

        public UnitOfWork(IDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext)
        {
            ServiceProvider = serviceProvider;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!Repositories.TryGetValue(typeof(IRepository<T>), out var repository))
            {
                Repositories[typeof(IRepository<T>)] = repository = ServiceProvider.GetRequiredService<IRepository<T>>();
            }

            return repository as IRepository<T>;
        }
    }
}