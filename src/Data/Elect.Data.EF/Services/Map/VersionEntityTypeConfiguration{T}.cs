#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> VersionEntityTypeConfiguration_T_.cs </Name>
//         <Created> 27/03/2018 12:19:50 AM </Created>
//         <Key> 78872de6-5ebe-424f-bccb-dac60a8bf585 </Key>
//     </File>
//     <Summary>
//         VersionEntityTypeConfiguration_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity;
using Elect.Data.EF.Models;

namespace Elect.Data.EF.Services.Map
{
    public abstract class VersionEntityTypeConfiguration<T> : VersionEntityTypeConfiguration<T, int> where T : Entity, IVersionEntity
    {
    }
}