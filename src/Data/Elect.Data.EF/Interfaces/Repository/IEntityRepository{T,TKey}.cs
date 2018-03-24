#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IEntityRepository_T_TKey_.cs </Name>
//         <Created> 24/03/2018 10:50:28 PM </Created>
//         <Key> a0b2f99f-7e48-4482-8dc2-7dec9575fee1 </Key>
//     </File>
//     <Summary>
//         IEntityRepository_T_TKey_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Elect.Data.EF.Interfaces.Repository
{
    public interface IEntityRepository<T, TKey> : IBaseEntityRepository<T> where T : Models.Entity<TKey>, new() where TKey : struct
    {
        void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData, params Expression<Func<T, object>>[] changedProperties);

        void UpdateWhere(Expression<Func<T, bool>> predicate, T entityNewData, params string[] changedProperties);

        void DeleteWhere(List<TKey> listId, bool isPhysicalDelete = false);
    }
}