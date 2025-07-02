namespace Elect.Data.EF.Interfaces.Entity
{
    /// <summary>
    ///     Resolve concurrency issue. 
    /// </summary>
    public interface IVersionEntity
    {
        [Timestamp]
        byte[] Version { get; set; }
    }
}
