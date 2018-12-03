#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IUnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:10:03 PM </Created>
//         <Key> d4db1752-097e-408f-adc6-04b5229f0c98 </Key>
//     </File>
//     <Summary>
//         IUnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Elect.Data.EF.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    { 
        /// <summary>
        ///     Function before save changes execute, return false for cancel save changes.
        /// </summary>
        Func<IEnumerable<EntityEntry>, bool> BeforeSaveChanges { get; set; }
        
        #region Transaction

        IUnitOfWorkTransaction BeginTransaction(IsolationLevel isolationLevel);

        IUnitOfWorkTransaction BeginTransaction();

        Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);

        #endregion

        #region Save

        [DebuggerStepThrough]
        int SaveChanges();

        [DebuggerStepThrough]
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        [DebuggerStepThrough]
        int SaveChanges(bool acceptAllChangesOnSuccess);

        [DebuggerStepThrough]
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        
        #endregion
        
        #region SQL Command

        DbCommand CreateCommand(string text, CommandType type, params SqlParameter[] parameters);
        
        void ExecuteCommand(string text, CommandType type, params SqlParameter[] parameters);

        List<T> ExecuteCommand<T>(string text, CommandType type, params SqlParameter[] parameters) where T : class, new();

        #endregion
    }
}