#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IStringEntityUnitOfWork.cs </Name>
//         <Created> 25/03/2018 10:12:51 PM </Created>
//         <Key> 9e26f986-9ce3-4bfb-a507-19420cd1fb0a </Key>
//     </File>
//     <Summary>
//         IStringEntityUnitOfWork.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Repository;

namespace Elect.Data.EF.Interfaces.UnitOfWork
{
    public interface IStringEntityUnitOfWork : IBaseUnitOfWork
    {
        IStringEntityRepository<T> GetRepository<T>() where T : Models.StringEntity, new();
    }
}