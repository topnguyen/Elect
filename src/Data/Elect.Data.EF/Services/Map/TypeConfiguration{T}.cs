namespace Elect.Data.EF.Services.Map
{
    public abstract class TypeConfiguration<T> : ITypeConfiguration<T> where T : class
    {
        public virtual void Map(EntityTypeBuilder<T> builder)
        {
        }
    }
}
