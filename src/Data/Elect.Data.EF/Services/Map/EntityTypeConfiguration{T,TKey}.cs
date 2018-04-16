#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EntityTypeConfiguration_T_TKey_.cs </Name>
//         <Created> 27/03/2018 12:14:18 AM </Created>
//         <Key> 020e7b8a-c4da-419d-b340-0336613563e5 </Key>
//     </File>
//     <Summary>
//         EntityTypeConfiguration_T_TKey_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Map;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elect.Data.EF.Services.Map
{
    public abstract class EntityTypeConfiguration<T, TKey> : ITypeConfiguration<T> where T : Entity<TKey> where TKey : struct
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Index
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.DeletedTime);
        }
    }
}