#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IUnitOfWork.cs </Name>
//         <Created> 25/03/2018 9:59:34 PM </Created>
//         <Key> d77ac0c3-38cf-4da4-9b65-f2f9c6de37b6 </Key>
//     </File>
//     <Summary>
//         IUnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Repository;

namespace Elect.Data.EF.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}