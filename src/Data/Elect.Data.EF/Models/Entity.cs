#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> Entity.cs </Name>
//         <Created> 24/03/2018 10:14:09 PM </Created>
//         <Key> 7bb7b76b-25f1-42aa-92de-dcbf8279ac35 </Key>
//     </File>
//     <Summary>
//         Entity.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.Entity;
using System;

namespace Elect.Data.EF.Models
{
    public abstract class Entity : Entity<int>, IGlobalIdentityEntity
    {
        public Guid GlobalId { get; set; }
    }
}