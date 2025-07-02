namespace Elect.Sample.Data.EF.Models
{
    public class UserEntity : Entity
    {
        public string UserName { get; set; }
        public virtual ICollection<UserProfileEntity> Profiles { get; set; }
    }
}
