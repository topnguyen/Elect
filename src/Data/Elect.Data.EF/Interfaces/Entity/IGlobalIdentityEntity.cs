#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IGlobalIdentityEntity.cs </Name>
//         <Created> 24/03/2018 9:32:59 PM </Created>
//         <Key> 9ccefafa-70ac-4071-9d7f-56992c2c8751 </Key>
//     </File>
//     <Summary>
//         IGlobalIdentityEntity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Data.EF.Interfaces.Entity
{
    public interface IGlobalIdentityEntity
    {
        Guid GlobalId { get; set; }
    }
}