#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EntityRepository_T_.cs </Name>
//         <Created> 24/03/2018 10:56:25 PM </Created>
//         <Key> d789072d-4aae-4919-8698-0b80302a9c58 </Key>
//     </File>
//     <Summary>
//         EntityRepository_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.DbContext;
using Elect.Data.EF.Models;

namespace Elect.Data.EF.Services.Repository
{
    public abstract class EntityRepository<T> : EntityRepository<T, int> where T : Entity, new()
    {
        protected EntityRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}