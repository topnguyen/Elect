#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ITypeConfiguration_T_.cs </Name>
//         <Created> 27/03/2018 12:11:24 AM </Created>
//         <Key> 9f3a5f74-e0ad-4dfe-9157-08cb50a6d48f </Key>
//     </File>
//     <Summary>
//         ITypeConfiguration_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Elect.Data.EF.Interfaces.Map
{
    public interface ITypeConfiguration<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}