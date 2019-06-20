#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IAuditableEntity.cs </Name>
//         <Created> 24/03/2018 9:11:03 PM </Created>
//         <Key> 856d2ade-6552-4355-abae-924221e50ab3 </Key>
//     </File>
//     <Summary>
//         IAuditableEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Data.EF.Interfaces.Entity.Auditable
{
    public interface IAuditableEntity
    {
        DateTimeOffset CreatedTime { get; set; }

        DateTimeOffset LastUpdatedTime { get; set; }
    }
}