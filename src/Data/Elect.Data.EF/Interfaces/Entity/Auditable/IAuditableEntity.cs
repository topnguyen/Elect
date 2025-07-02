namespace Elect.Data.EF.Interfaces.Entity.Auditable
{
    public interface IAuditableEntity
    {
        DateTimeOffset CreatedTime { get; set; }
        DateTimeOffset LastUpdatedTime { get; set; }
    }
}
