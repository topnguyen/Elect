namespace Elect.Data.EF.Interfaces.Map
{
    public interface ITypeConfiguration<T> where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }
}
