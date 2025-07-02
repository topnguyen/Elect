namespace Elect.Data.EF.Models
{
    public class VersionStringEntity : StringEntity, IVersionEntity
    {
        public byte[] Version { get; set; }
    }
}
