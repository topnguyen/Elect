#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TypeConfiguration_T_.cs </Name>
//         <Created> 27/03/2018 12:12:31 AM </Created>
//         <Key> 130d4192-055e-4730-ba02-b6e91aac572c </Key>
//     </File>
//     <Summary>
//         TypeConfiguration_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Map;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elect.Data.EF.Services.Map
{
    public abstract class TypeConfiguration<T> : ITypeConfiguration<T> where T : class
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
        }
    }
}