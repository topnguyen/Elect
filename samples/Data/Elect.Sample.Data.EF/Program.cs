﻿using Elect.Sample.Data.EF.Interfaces;
using Elect.Sample.Data.EF.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using StringHelper = Elect.Core.StringUtils.StringHelper;

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

                _unitOfWork = serviceProvider.GetService<IUnitOfWork>();

                _userRepo = _unitOfWork.GetRepository<UserEntity>();
                _userProfileRepo = _unitOfWork.GetRepository<UserProfileEntity>();

                var user1Id = AddUser("User Name 1");

                AddRandomProfile(user1Id, false);
                AddRandomProfile(user1Id, false);
                AddRandomProfile(user1Id, false);
                AddRandomProfile(user1Id, true);
                AddRandomProfile(user1Id, true);
                
                var firstUserInfo = GetUser(user1Id);

                UpdateUser(user1Id, "User Name 2");

                var user2Id = AddUser("User Name 2");

                AddRandomProfile(user2Id, false);
                AddRandomProfile(user2Id, false);
                AddRandomProfile(user2Id, false);
                AddRandomProfile(user2Id, false);
                AddRandomProfile(user2Id, true);
                
                var secondUserInfo = GetUser(user2Id);

                var user3Id = AddUser("User Name 3");

                RemoveUser(user3Id);

                // Should be null
                var thirdUserInfo = GetUser(user3Id);

                TransactionRollback();

                TransactionCommit();
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
                Phone = StringHelper.Generate(10),
                DeletedTime = isDeleted ? (DateTimeOffset?) null : DateTimeOffset.UtcNow
            };

            _userProfileRepo.Add(userProfileEntity);

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