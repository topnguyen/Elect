namespace Elect.Data.EF.Models
{
    public abstract class Entity : Entity<int>, IGlobalIdentityEntity
    {
        public Guid GlobalId { get; set; }
    }
}
