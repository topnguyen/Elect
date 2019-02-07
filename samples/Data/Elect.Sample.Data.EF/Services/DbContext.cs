using Elect.Core.ConfigUtils;
using Elect.Data.EF.Utils.ModelBuilderUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Elect.Sample.Data.EF.Services
{
    public sealed partial class DbContext : Elect.Data.EF.Services.DbContext.DbContext
    {
        public readonly int CommandTimeoutInSecond = 20 * 60;

        public DbContext()
        {
            Database.SetCommandTimeout(CommandTimeoutInSecond);
        }

        public DbContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(CommandTimeoutInSecond);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile(Core.Constants.ConfigurationFileName.ConnectionConfig, false, true).Build();

                var connectionString = config.GetValueByEnv<string>(Core.Constants.ConfigurationSectionName.ConnectionStrings);

                optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(DbContext).GetTypeInfo().Assembly.GetName().Name);

                    sqlServerOptionsAction.MigrationsHistoryTable("Migration");
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // [Important] Keep Under Base For Override And Make End Result

            // Scan and apply Config/Mapping for Tables/Entities (from folder "Maps")
            builder.AddConfigFromAssembly<DbContext>(typeof(DbContext).GetTypeInfo().Assembly);

            // Set Delete Behavior as Restrict in Relationship
            builder.DisableCascadingDelete();

            // Convention for Table name
            builder.RemovePluralizingTableNameConvention();

            builder.ReplaceTableNameConvention("Entity", string.Empty);
        }
    }
}