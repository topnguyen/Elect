#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> VersionStringEntityTypeConfiguration_T_.cs </Name>
//         <Created> 27/03/2018 12:22:36 AM </Created>
//         <Key> 31098d46-b908-4a4f-b126-020710958085 </Key>
//     </File>
//     <Summary>
//         VersionStringEntityTypeConfiguration_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity;
using Elect.Data.EF.Interfaces.Map;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elect.Data.EF.Services.Map
{
    public abstract class VersionStringEntityTypeConfiguration<T> : ITypeConfiguration<T> where T : StringEntity, IVersionEntity
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Index
            builder.HasIndex(x => x.DeletedTime);

            // Version
            builder.Property(x => x.Version).IsRowVersion();
            
            // Filter
            builder.HasQueryFilter(x => x.DeletedTime == null);
        }
    }
}