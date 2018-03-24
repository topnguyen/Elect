#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DbContext.cs </Name>
//         <Created> 24/03/2018 10:59:50 PM </Created>
//         <Key> d8b22897-5b92-46a8-8404-f6450474b50b </Key>
//     </File>
//     <Summary>
//         DbContext.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.EF.Interfaces.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Elect.Data.EF.Services.DbContext
{
    public abstract class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
    {
        protected DbContext()
        {
        }

        protected DbContext(DbContextOptions options) : base(options)
        {
        }
    }
}