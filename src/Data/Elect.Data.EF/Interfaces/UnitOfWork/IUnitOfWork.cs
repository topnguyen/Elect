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
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Elect.Data.EF.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Save Changes Callback

        // Before Save Changes

        /// <summary>
        ///     Function before save changes execute, return false for cancel save changes.
        /// </summary>
        /// <returns>Func Id, use to remove from actions list later.</returns>
        string AddFunctionBeforeSaveChanges(Func<IEnumerable<EntityEntry>, bool> func);

        void RemoveFunctionBeforeSaveChanges(string funcId);

        void EmptyFunctionsBeforeSaveChanges();

        // After Save Changes

        /// <summary>
        ///     Add Action after save changes
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Action Id, use to remove from actions list later.</returns>
        string AddActionAfterSaveChanges(Action<EntityStatesModel> action);

        void RemoveActionAfterSaveChanges(string actionId);

        void EmptyActionsAfterSaveChanges();

        #endregion

        #region Commit Callback

        // Before Commit

        /// <summary>
        ///     Add Action before commit
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Action Id, use to remove from actions list later.</returns>
        string AddActionBeforeCommit(Action action);

        void RemoveActionBeforeCommit(string actionId);

        void EmptyActionsBeforeCommit();

        // After Commit

        /// <summary>
        ///     Add Action after commit
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Action Id, use to remove from actions list later.</returns>
        string AddActionAfterCommit(Action action);

        void RemoveActionAfterCommit(string actionId);

        void EmptyActionsAfterCommit();

        #endregion

        #region Rollback Callback

        // Before Rollback

        /// <summary>
        ///     Add Action before rollback
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Action Id, use to remove from actions list later.</returns>
        string AddActionBeforeRollback(Action action);

        void RemoveActionBeforeRollback(string actionId);

        void EmptyActionsBeforeRollback();

        // After Rollback

        /// <summary>
        ///     Add Action after rollback
        /// </summary>
        /// <param name="action"></param>
        /// <returns>Action Id, use to remove from actions list later.</returns>
        string AddActionAfterRollback(Action action);

        void RemoveActionAfterRollback(string actionId);

        void EmptyActionsAfterRollback();

        #endregion

        #region Transaction

        IUnitOfWorkTransaction BeginTransaction(IsolationLevel isolationLevel);

        IUnitOfWorkTransaction BeginTransaction();

        Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default);

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

        List<T> ExecuteCommand<T>(string text, CommandType type, params SqlParameter[] parameters)
            where T : class, new();

        #endregion
    }
}