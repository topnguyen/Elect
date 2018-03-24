#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> VersionStringEntity.cs </Name>
//         <Created> 24/03/2018 10:19:26 PM </Created>
//         <Key> 4ce91933-b7e0-43df-925c-ab5e09669745 </Key>
//     </File>
//     <Summary>
//         VersionStringEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity;

namespace Elect.Data.EF.Models
{
    public class VersionStringEntity : StringEntity, IVersionEntity
    {
        public byte[] Version { get; set; }
    }
}