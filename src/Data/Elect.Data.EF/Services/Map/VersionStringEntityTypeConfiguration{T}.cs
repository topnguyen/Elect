namespace Elect.Data.EF.Services.Map
{
    public abstract class VersionStringEntityTypeConfiguration<T> : ITypeConfiguration<T> where T : StringEntity, IVersionEntity
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            // Key
            builder.HasKey(x => x.Id);
            // Index
            builder.HasIndex(x => x.DeletedTime);
            // Version
            builder.Property(x => x.Version).IsRowVersion();
            // Filter
            builder.HasQueryFilter(x => x.DeletedTime == null);
        }
    }
}
