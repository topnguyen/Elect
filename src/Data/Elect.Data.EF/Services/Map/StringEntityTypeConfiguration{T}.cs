#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> StringEntityTypeConfiguration_T_.cs </Name>
//         <Created> 27/03/2018 12:20:30 AM </Created>
//         <Key> 8771a4cb-a981-4f66-9f84-84db00a5c677 </Key>
//     </File>
//     <Summary>
//         StringEntityTypeConfiguration_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Map;
using Elect.Data.EF.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elect.Data.EF.Services.Map
{
    public abstract class StringEntityTypeConfiguration<T> : ITypeConfiguration<T> where T : StringEntity
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