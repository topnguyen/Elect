namespace Elect.Sample.Data.EF.Models
{
    public class UserProfileEntity : Entity
    {
        public string Phone { get; set; }
        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
