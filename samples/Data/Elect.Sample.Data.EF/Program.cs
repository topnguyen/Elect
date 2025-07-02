namespace Elect.Sample.Data.EF
{
    public class Program
    {
        private static IRepository<UserEntity> _userRepo;
        private static IRepository<UserProfileEntity> _userProfileRepo;
        private static IUnitOfWork _unitOfWork;
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
            using (var scope = webHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetService<IDbContext>();
                dbContext.Database.Migrate();
                _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
                _userRepo = _unitOfWork.GetRepository<UserEntity>();
                _userProfileRepo = _unitOfWork.GetRepository<UserProfileEntity>();
                if (!_userProfileRepo.Get().Any())
                {
                    // User 1
                    var user1Id = AddUser("User Name 1");
                    // 3 Active Profile, 2 Deleted Profile
                    AddRandomProfile(user1Id, false);
                    AddRandomProfile(user1Id, false);
                    AddRandomProfile(user1Id, false);
                    AddRandomProfile(user1Id, true);
                    AddRandomProfile(user1Id, true);
                    var firstUserInfo = GetUser(user1Id);
                    UpdateUser(user1Id, "User Name 1'");
                    // User 2
                    var user2Id = AddUser("User Name 2");
                    // 4 Active Profile, 1 Deleted Profile
                    AddRandomProfile(user2Id, false);
                    AddRandomProfile(user2Id, false);
                    AddRandomProfile(user2Id, false);
                    AddRandomProfile(user2Id, false);
                    AddRandomProfile(user2Id, true);
                    var secondUserInfo = GetUser(user2Id);
                    // User 3
                    var user3Id = AddUser("User Name 3");
                    // 2 Active Profile, 3 Deleted Profile
                    AddRandomProfile(user3Id, false);
                    AddRandomProfile(user3Id, false);
                    AddRandomProfile(user3Id, true);
                    AddRandomProfile(user3Id, true);
                    AddRandomProfile(user3Id, true);
                    RemoveUser(user3Id);
                    // Should be null
                    var thirdUserInfo = GetUser(user3Id);
                    // Transaction Sample
                    // 0 Account
                    TransactionRollback();
                    // 1 Account - 0 Profile
                    TransactionCommit();
                }
                // Filter Deleted Record Sample
                var users = _userRepo.Get(null, true).Include(x => x.Profiles).ToList();
                var totalProfileDeleted = users.SelectMany(x => x.Profiles).Count(x => x.DeletedTime != null);
                var isFilterSuccess = totalProfileDeleted == 6;
            }
        }
        private static Guid AddUser(string userName)
        {
            var userEntity = new UserEntity
            {
                UserName = userName
            };
            _userRepo.Add(userEntity);
            _unitOfWork.SaveChanges();
            return userEntity.Id;
        }
        private static void RemoveUser(Guid id)
        {
            _userRepo.Delete(new UserEntity
            {
                Id = id
            });
            _unitOfWork.SaveChanges();
        }
        private static void UpdateUser(Guid id, string newUserName)
        {
            _userRepo.Update(new UserEntity
            {
                Id = id,
                UserName = newUserName
            }, change => change.UserName);
            _unitOfWork.SaveChanges();
        }
        private static UserEntity GetUser(Guid id)
        {
            UserEntity userEntity = _userRepo.Get(user => user.Id == id).FirstOrDefault();
            return userEntity;
        }
        private static Guid AddRandomProfile(Guid userId, bool isDeleted)
        {
            var userProfileEntity = new UserProfileEntity
            {
                UserId = userId,
                Phone = StringHelper.Generate(10)
            };
            userProfileEntity = _userProfileRepo.Add(userProfileEntity);
            _unitOfWork.SaveChanges();
            if (!isDeleted)
            {
                return userProfileEntity.Id;
            }
            _userProfileRepo.Delete(userProfileEntity);
            _unitOfWork.SaveChanges();
            return userProfileEntity.Id;
        }
        private static void TransactionRollback()
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var userId = AddUser("Transaction - User Name 1");
                transaction.Rollback();
                // Should be null
                var userInfo = GetUser(userId);
            }
        }
        private static void TransactionCommit()
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var userId = AddUser("Transaction - User Name 1");
                UpdateUser(userId, "Transaction - User Name 2");
                transaction.Commit();
                var userInfo = GetUser(userId);
            }
        }
    }
}
