#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringEntityUnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:45:22 PM </Created>
//         <Key> 292718c2-da09-4f2e-bb2a-fa905faee22e </Key>
//     </File>
//     <Summary>
//         StringEntityUnitOfWork.cs is a part of Elect
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
    public class StringEntityUnitOfWork : BaseUnitOfWork, IStringEntityUnitOfWork
    {
        protected readonly IServiceProvider ServiceProvider;

        protected ConcurrentDictionary<Type, object> Repositories = new ConcurrentDictionary<Type, object>();

        public StringEntityUnitOfWork(IDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext)
        {
            ServiceProvider = serviceProvider;
        }

        public IStringEntityRepository<T> GetRepository<T>() where T : StringEntity, new()
        {
            if (!Repositories.TryGetValue(typeof(IStringEntityRepository<T>), out var repository))
            {
                Repositories[typeof(IStringEntityRepository<T>)] = repository = ServiceProvider.GetRequiredService<IStringEntityRepository<T>>();
            }

            return repository as IStringEntityRepository<T>;
        }
    }
}