#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EntityTypeConfiguration_T_.cs </Name>
//         <Created> 27/03/2018 12:18:17 AM </Created>
//         <Key> 7b890b3b-4535-4b77-adb0-48d54c2165de </Key>
//     </File>
//     <Summary>
//         EntityTypeConfiguration_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Map;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elect.Data.EF.Services.Map
{
    public abstract class EntityTypeConfiguration<T> : ITypeConfiguration<T> where T : Entity
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            // Key
            builder.HasKey(x => x.Id);

            // Index
            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.GlobalId);
            builder.HasIndex(x => x.DeletedTime);

            // Contract
            builder.Property(x => x.GlobalId).IsRequired();
        }
    }
}