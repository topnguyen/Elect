#region	License

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

using System.Collections.Generic;
using System.Linq;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;

        public List<ActionModel> ActionsBeforeCommit { get; set; } = new List<ActionModel>();

        public List<ActionModel> ActionsAfterCommit { get; set; } = new List<ActionModel>();

        public List<ActionModel> ActionsBeforeRollback { get; set; } = new List<ActionModel>();

        public List<ActionModel> ActionsAfterRollback { get; set; } = new List<ActionModel>();

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
            if (ActionsBeforeCommit?.Any() == true)
            {
                foreach (var actionModel in ActionsBeforeCommit)
                {
                    actionModel?.Action?.Invoke();
                }
            }

            _dbContextTransaction.Commit();

            if (ActionsAfterCommit?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterCommit)
                {
                    actionModel?.Action?.Invoke();
                }
            }
        }

        public void Rollback()
        {
            if (ActionsBeforeRollback?.Any() == true)
            {
                foreach (var actionModel in ActionsBeforeRollback)
                {
                    actionModel?.Action?.Invoke();
                }
            }

            _dbContextTransaction.Rollback();

            if (ActionsAfterRollback?.Any() == true)
            {
                foreach (var actionModel in ActionsAfterRollback)
                {
                    actionModel?.Action?.Invoke();
                }
            }
        }
    }
}