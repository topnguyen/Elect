using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elect.Notification.OneSignal;
using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Models.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.Notification.Onesignal
{
    [TestClass]
    public class ModelUnitTest
    {
        public string AppId = "<your onesignal APP ID>";
        
        public IElectOneSignalClient GetClient()
        {
            var services = new ServiceCollection();

            // One Signal
            services.AddElectOneSignal(x =>
            {
                x.AuthKey = "<your onesignal auth key>"; // the Auth key you can setting via appsettings.json
                x.Apps =
                    new List<ElectOneSignalAppOption> // the list of application you have and want to put notification
                    {
                        new ElectOneSignalAppOption
                        {
                            AppId = AppId,
                            ApiKey = "<your onesignal API KEY>",
                            AppName = "Sample App" // Just the identity name for easier debuging
                        }
                    };
            });

            var provider = services.BuildServiceProvider();

            var client = provider.GetService<IElectOneSignalClient>();

            return client;
        }

        [TestMethod]
        public async Task CreateNotification()
        {
            var notifyCreateOptions = new NotificationCreateModel
            {
                Url = "https://www.google.com", // url to redirect when user click on it,
                Data = new Dictionary<string, string> // additional data as dictionary
                {
                    {"Key1", "Data1"}
                },
                IncludedSegments = new List<string> {"All"} // send to all player
            };

            // Title + Message
            // English is mandatory, If you want to have localization still need English version
            notifyCreateOptions.Contents.Add(LanguageCodes.English, $"Sample Message: {DateTime.Now:hh:mm:ss t z}");

            var client = GetClient();

            var notificationResult = await client.Notifications.CreateAsync(notifyCreateOptions, AppId);

            Assert.AreNotEqual(notificationResult.Recipients, 0);
        }
        
        [TestMethod]
        public async Task CancelNotification()
        {
            var notifyCreateOptions = new NotificationCreateModel
            {
                Url = "https://www.google.com", // url to redirect when user click on it,
                Data = new Dictionary<string, string> // additional data as dictionary
                {
                    {"Key1", "Data1"}
                },
                IncludedSegments = new List<string> {"All"}, // send to all player
                SendAfter = DateTime.Now.AddDays(1) // [IMPORTANT] Only can cancel the Schedule Notification
            };

            // Title + Message
            // English is mandatory, If you want to have localization still need English version
            notifyCreateOptions.Contents.Add(LanguageCodes.English, $"Sample Message: {DateTime.Now:hh:mm:ss t z}");

            var client = GetClient();

            var notificationResult = await client.Notifications.CreateAsync(notifyCreateOptions, AppId);

            Assert.AreNotEqual(notificationResult.Recipients, 0);

            var cancelResult = await client.Notifications.CancelAsync(notificationResult.Id, AppId);
            
            Assert.AreEqual(cancelResult.Success, "success");
        }
    }
}