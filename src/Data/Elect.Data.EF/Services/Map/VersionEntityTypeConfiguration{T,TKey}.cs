#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> VersionEntityTypeConfiguration_T_TKey_.cs </Name>
//         <Created> 27/03/2018 12:15:16 AM </Created>
//         <Key> e8428702-b62d-4dc5-be4e-fdf6e729247c </Key>
//     </File>
//     <Summary>
//         VersionEntityTypeConfiguration_T_TKey_.cs is a part of Elect
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
    public abstract class VersionEntityTypeConfiguration<T, TKey> : ITypeConfiguration<T> where T : Entity<TKey>, IVersionEntity where TKey : struct
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