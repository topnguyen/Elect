namespace Elect.Data.EF.Services.Map
{
    public abstract class VersionEntityTypeConfiguration<T> : VersionEntityTypeConfiguration<T, int> where T : Entity, IVersionEntity
    {
    }
}
