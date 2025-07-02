namespace Elect.Data.EF.Interfaces.Entity.SoftDelete
{
    public interface ISoftDeletableEntity
    {
        DateTimeOffset? DeletedTime { get; set; }
    }
}
