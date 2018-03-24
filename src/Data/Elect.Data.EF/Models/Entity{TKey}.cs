#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> Entity_TKey_.cs </Name>
//         <Created> 24/03/2018 10:06:55 PM </Created>
//         <Key> 1b42d9b8-313f-486e-b645-7e84108bbd50 </Key>
//     </File>
//     <Summary>
//         Entity_TKey_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity.Auditable;
using Elect.Data.EF.Interfaces.Entity.SoftDelete;

namespace Elect.Data.EF.Models
{
    public abstract class Entity<TKey> : BaseEntity, ISoftDeletableEntity<TKey>, IAuditableEntity<TKey> where TKey : struct
    {
        public TKey Id { get; set; }

        public TKey? CreatedBy { get; set; }

        public TKey? LastUpdatedBy { get; set; }

        public TKey? DeletedBy { get; set; }
    }
}