namespace Elect.Data.EF.Services.Map
{
    public abstract class StringEntityTypeConfiguration<T> : ITypeConfiguration<T> where T : StringEntity
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
            // Key
            builder.HasKey(x => x.Id);
            // Index
            builder.HasIndex(x => x.DeletedTime);
            // Filter
            builder.HasQueryFilter(x => x.DeletedTime == null);
        }
    }
}
