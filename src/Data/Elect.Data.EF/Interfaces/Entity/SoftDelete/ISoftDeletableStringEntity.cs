namespace Elect.Data.EF.Interfaces.Entity.SoftDelete
{
    public interface ISoftDeletableStringEntity : ISoftDeletableEntity
    {
        string DeletedBy { get; set; }
    }
}
