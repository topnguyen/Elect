using Elect.Data.EF.Interfaces.DbContext;
using Elect.DI.Attributes;
using Elect.Test.AspNetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Elect.Test.AspNetCore.Data
{
    [ScopedDependency(ServiceType = typeof(IDbContext))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed. For example,
            // you can rename the ASP.NET Identity table names and more. Add your customizations
            // after calling base.OnModelCreating(builder);
        }
    }
}