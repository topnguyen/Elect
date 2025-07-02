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
                var configBuilder =
                    new ConfigurationBuilder()
                        .AddJsonFile("connectionconfig.json", false, false);
                var config = configBuilder.Build();
                var commandTimeoutInSecond = config.GetValueByEnv<int>("CommandTimeoutInSecond");
                var connectionString = config.GetValueByEnv<string>("ConnectionString");
                optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction =>
                {
                    optionsBuilder
                        .UseSqlServer(connectionString, optionsBuilder =>
                        {
                            sqlServerOptionsAction
                                .CommandTimeout(commandTimeoutInSecond)
                                .MigrationsAssembly(typeof(DbContext).GetTypeInfo().Assembly.GetName().Name)
                                .MigrationsHistoryTable("Migration");
                        })
                        .EnableSensitiveDataLogging(EnvHelper.IsDevelopment())
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
