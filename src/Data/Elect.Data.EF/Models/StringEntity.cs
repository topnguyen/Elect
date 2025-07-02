namespace Elect.Data.EF.Models
{
    public abstract class StringEntity : BaseEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public string DeletedBy { get; set; }
    }
}
