namespace Elect.Sample.Mapper.AutoMapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = BuildWebHost(args);
            OnAppStart(webHost);
            webHost.Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);
            webHostBuilder.UseStartup<Startup>();
            var webHost = webHostBuilder.Build();
            return webHost;
        }
        private static void OnAppStart(IWebHost webHost)
        {
            // Map from UserEntity to UserModel
            var userEntity = new UserEntity
            {
                Id = 1,
                UserName = "User 1",
                Password = "Password",
                Profile = new ProfileEntity { FullName = "User 1 Full Name" }
            };
            // Create new instance of UserModel with data from UserEntity.
            var userModel = userEntity.MapTo<UserModel>();
            var userEntity2 = new UserEntity
            {
                Id = 2,
                UserName = "User 2",
                Password = "Password",
                Profile = new ProfileEntity { FullName = "User 2 Full Name" }
            };
            // Update userModel by userEntity data.
            userEntity2.MapTo(userModel);
            // IQueryable
            IQueryable<UserEntity> userEntities = new List<UserEntity> { userEntity, userEntity2 }.AsQueryable();
            List<UserModel> userModels = userEntities.QueryTo<UserModel>().ToList();
        }
    }
}
