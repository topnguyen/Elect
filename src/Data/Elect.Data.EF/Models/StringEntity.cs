#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringEntity.cs </Name>
//         <Created> 24/03/2018 10:14:58 PM </Created>
//         <Key> e565f35a-b376-4684-b371-dbada4a200a0 </Key>
//     </File>
//     <Summary>
//         StringEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity.Auditable;
using Elect.Data.EF.Interfaces.Entity.SoftDelete;

namespace Elect.Data.EF.Models
{
    public abstract class StringEntity : BaseEntity, ISoftDeletableStringEntity, IAuditableStringEntity
    {
        public virtual string Id { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual string LastUpdatedBy { get; set; }

        public virtual string DeletedBy { get; set; }
    }
}