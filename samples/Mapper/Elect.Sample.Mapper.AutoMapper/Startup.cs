namespace Elect.Sample.Mapper.AutoMapper
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Auto Mapper Services and Auto Register Auto Mapper Profiles
            services.AddElectAutoMapper();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual ProfileEntity Profile { get; set; }
    }
    public class ProfileEntity
    {
        public string FullName { get; set; }
    }
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
    public class UserProfile : global::AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .IgnoreAllNonExisting()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.Profile.FullName));
        }
    }
}
