#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IVersionEntity.cs </Name>
//         <Created> 24/03/2018 9:44:34 PM </Created>
//         <Key> d6fc8f19-807a-488b-bbdc-f36ecb4bcc0b </Key>
//     </File>
//     <Summary>
//         IVersionEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel.DataAnnotations;

namespace Elect.Data.EF.Interfaces.Entity
{
    /// <summary>
    ///     Resolve concurrency issue. 
    /// </summary>
    public interface IVersionEntity
    {
        [Timestamp]
        byte[] Version { get; set; }
    }
}