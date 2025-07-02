namespace Elect.Data.EF.Interfaces.Entity.SoftDelete
{
    public interface ISoftDeletableEntity<TKey> : ISoftDeletableEntity where TKey : struct
    {
        TKey? DeletedBy { get; set; }
    }
}
