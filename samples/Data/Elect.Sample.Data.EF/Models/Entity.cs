namespace Elect.Sample.Data.EF.Models
{
    public abstract class Entity : Elect.Data.EF.Models.Entity<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedTime = LastUpdatedTime = DateTimeOffset.UtcNow;
        }
    }
}
