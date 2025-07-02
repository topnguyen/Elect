namespace Elect.Data.EF.Models
{
    public abstract class BaseEntity : ElectDisposableModel, ISoftDeletableEntity, IAuditableEntity
    {
        protected BaseEntity()
        {
            CreatedTime = LastUpdatedTime = DateTimeOffset.UtcNow;
        }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
    }
}
