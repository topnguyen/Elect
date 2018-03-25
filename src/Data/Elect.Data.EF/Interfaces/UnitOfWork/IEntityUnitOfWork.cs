#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IEntityUnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:09:31 PM </Created>
//         <Key> 3ec3a7e6-64fe-4d66-8b92-8f18be747516 </Key>
//     </File>
//     <Summary>
//         IEntityUnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Repository;

namespace Elect.Data.EF.Interfaces.UnitOfWork
{
    public interface IEntityUnitOfWork : IBaseUnitOfWork
    {
        IEntityRepository<T, TKey> GetRepository<T, TKey>() where T : Models.Entity<TKey>, new() where TKey : struct;
    }
}