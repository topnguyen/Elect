namespace Elect.Sample.Data.EF.Maps
{
    public class UserProfileEntityMap : EntityTypeConfiguration<UserProfileEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserProfileEntity> builder)
        {
            base.Map(builder);
            builder.ToTable(nameof(UserProfileEntity));
            builder.HasOne(x => x.User).WithMany(x => x.Profiles).HasForeignKey(x => x.UserId);
        }
    }
}
