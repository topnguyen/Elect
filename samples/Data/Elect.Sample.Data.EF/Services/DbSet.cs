namespace Elect.Sample.Data.EF.Services
{
    public sealed partial class DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserProfileEntity> UserProfiles { get; set; }
    }
}
