namespace Elect.Data.EF.Services.Map
{
    public abstract class EntityTypeConfiguration<T, TKey> : ITypeConfiguration<T> where T : Entity<TKey> where TKey : struct
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
