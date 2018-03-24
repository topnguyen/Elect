#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> VersionEntity_Tkey_.cs </Name>
//         <Created> 24/03/2018 10:16:25 PM </Created>
//         <Key> 84b10bb1-a4de-4aa3-9d7a-60ce2e079df4 </Key>
//     </File>
//     <Summary>
//         VersionEntity_Tkey_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity;

namespace Elect.Data.EF.Models
{
    public abstract class VersionEntity<TKey> : Entity<TKey>, IVersionEntity where TKey : struct
    {
        public byte[] Version { get; set; }
    }
}