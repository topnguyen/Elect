﻿#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> UnitOfWorkTransaction.cs </Name>
//         <Created> 25/03/2018 10:26:29 PM </Created>
//         <Key> a280b9b8-3582-43d9-ba74-7861c8825f94 </Key>
//     </File>
//     <Summary>
//         UnitOfWorkTransaction.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System.Linq;
using Elect.Core.ActionUtils;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        public ActionCollection ActionsBeforeCommit { get; set; } = new ActionCollection();

        public ActionCollection ActionsAfterCommit { get; set; } = new ActionCollection();

        public ActionCollection ActionsBeforeRollback { get; set; } = new ActionCollection();

        public ActionCollection ActionsAfterRollback { get; set; } = new ActionCollection();

        public UnitOfWorkTransaction(IDbContextTransaction dbContextTransaction)
        {
            _dbContextTransaction = dbContextTransaction;
        }

        public void Dispose()
        {
            _dbContextTransaction.Dispose();
        }

        public void Commit()
        {
            if (ActionsBeforeCommit?.Get()?.Any() == true)
            {
                foreach (var actionModel in ActionsBeforeCommit.Get())
                {
                    actionModel?.Action?.Invoke();
                }
            }

            _dbContextTransaction.Commit();

            if (ActionsAfterCommit?.Get()?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterCommit.Get())
                {
                    actionModel?.Action?.Invoke();
                }
            }
        }

        public void Rollback()
        {
            if (ActionsBeforeRollback?.Get()?.Any() == true)
            {
                foreach (var actionModel in ActionsBeforeRollback.Get())
                {
                    actionModel?.Action?.Invoke();
                }
            }

            _dbContextTransaction.Rollback();

            if (ActionsAfterRollback?.Get()?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterRollback.Get())
                {
                    actionModel?.Action?.Invoke();
                }
            }
        }
    }
}