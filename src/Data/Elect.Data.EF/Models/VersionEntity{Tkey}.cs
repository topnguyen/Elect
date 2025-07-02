namespace Elect.Data.EF.Models
{
    public abstract class VersionEntity<TKey> : Entity<TKey>, IVersionEntity where TKey : struct
    {
        public byte[] Version { get; set; }
    }
}
