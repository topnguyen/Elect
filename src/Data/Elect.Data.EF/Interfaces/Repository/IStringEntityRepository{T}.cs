#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IStringEntityRepository_T_.cs </Name>
//         <Created> 24/03/2018 10:52:31 PM </Created>
//         <Key> d6b339f4-b697-454e-b8c6-1169f268e57f </Key>
//     </File>
//     <Summary>
//         IStringEntityRepository_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Elect.Data.EF.Interfaces.Repository
{
    public interface IStringEntityRepository<T> : IBaseEntityRepository<T> where T : StringEntity, new()
    {
        void UpdateWhere(Expression<Func<T, bool>> predicate, T entityData, params Expression<Func<T, object>>[] changedProperties);

        void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData, params string[] changedProperties);

        void DeleteWhere(List<string> listId, bool isPhysicalDelete = false);
    }
}