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

using System;
using System.Collections.Generic;
using System.Linq;
using Elect.Data.EF.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elect.Data.EF.Services.UnitOfWork
{
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        private readonly IDbContextTransaction _dbContextTransaction;
        
        public List<Action> ActionsBeforeCommit { get; set; } = new List<Action>();

        public List<Action> ActionsAfterCommit { get; set; } = new List<Action>();
        
        public List<Action> ActionsBeforeRollback { get; set; } = new List<Action>();
        
        public List<Action> ActionsAfterRollback { get; set; } = new List<Action>();

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
                foreach (var action in ActionsBeforeCommit)
                {
                    action?.Invoke();
                }
            }

            _dbContextTransaction.Commit();
            
            if (ActionsAfterCommit?.Any() == true)
            {
                foreach (var action in ActionsAfterCommit)
                {
                    action?.Invoke();
                }
            }
        }

        public void Rollback()
        {
            if (ActionsBeforeRollback?.Any() == true)
            {
                foreach (var action in ActionsBeforeRollback)
                {
                    action?.Invoke();
                }
            }
            
            _dbContextTransaction.Rollback();
            
            if (ActionsAfterRollback?.Any() == true)
            {
                foreach (var action in ActionsAfterRollback)
                {
                    action?.Invoke();
                }
            }
        }
    }
}