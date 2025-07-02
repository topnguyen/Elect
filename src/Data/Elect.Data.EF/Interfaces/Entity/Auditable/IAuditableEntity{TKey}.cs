namespace Elect.Data.EF.Interfaces.Entity.Auditable
{
    public interface IAuditableEntity<TKey> : IAuditableEntity where TKey : struct
    {
        TKey? CreatedBy { get; set; }
        TKey? LastUpdatedBy { get; set; }
    }
}
