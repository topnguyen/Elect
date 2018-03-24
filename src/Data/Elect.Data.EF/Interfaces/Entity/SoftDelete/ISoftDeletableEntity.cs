#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ISoftDeletableEntity.cs </Name>
//         <Created> 24/03/2018 9:35:46 PM </Created>
//         <Key> d902ae94-ff22-4caa-a959-b5f97fef2f91 </Key>
//     </File>
//     <Summary>
//         ISoftDeletableEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Data.EF.Interfaces.Entity.SoftDelete
{
    public interface ISoftDeletableEntity
    {
        DateTimeOffset? DeletedTime { get; set; }
    }
}