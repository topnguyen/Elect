namespace Elect.Data.EF.Interfaces.Entity.Auditable
{
    public interface IAuditableStringEntity : IAuditableEntity
    {
        string CreatedBy { get; set; }
        string LastUpdatedBy { get; set; }
    }
}
