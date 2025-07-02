namespace Elect.Data.EF.Models
{
    public abstract class Entity<TKey> : BaseEntity where TKey : struct
    {
        public TKey Id { get; set; }
        public TKey? CreatedBy { get; set; }
        public TKey? LastUpdatedBy { get; set; }
        public TKey? DeletedBy { get; set; }
    }
}
