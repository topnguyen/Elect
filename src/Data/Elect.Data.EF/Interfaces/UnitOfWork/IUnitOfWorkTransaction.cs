#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IUnitOfWorkTransaction.cs </Name>
//         <Created> 25/03/2018 10:04:53 PM </Created>
//         <Key> 783f5f69-237c-4bb3-8b30-f3add5a6b446 </Key>
//     </File>
//     <Summary>
//         IUnitOfWorkTransaction.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using Elect.Core.ActionUtils;

namespace Elect.Data.EF.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        ActionCollection ActionsBeforeCommit { get; set; }

        ActionCollection ActionsAfterCommit { get; set; }

        ActionCollection ActionsBeforeRollback { get; set; }

        ActionCollection ActionsAfterRollback { get; set; }

        void Commit();

        void Rollback();
    }
}