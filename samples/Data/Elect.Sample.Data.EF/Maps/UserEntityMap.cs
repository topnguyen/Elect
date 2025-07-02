namespace Elect.Sample.Data.EF.Maps
{
    public class UserEntityMap : EntityTypeConfiguration<UserEntity, Guid>
    {
        public override void Map(EntityTypeBuilder<UserEntity> builder)
        {
            base.Map(builder);
            builder.ToTable(nameof(UserEntity));
            builder.HasIndex(x => x.UserName);
        }
    }
}
