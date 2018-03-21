#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectSendSmsUnitTest.cs </Name>
//         <Created> 21/03/2018 9:09:49 AM </Created>
//         <Key> 56292894-87c2-40c8-a045-0eb58c559112 </Key>
//     </File>
//     <Summary>
//         ElectSendSmsUnitTest.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Threading.Tasks;
using Elect.Notification.Esms;
using Elect.Notification.Esms.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Elect.Test.UnitTest.Elect.Notification.Esms
{
    [TestClass]
    public class ElectSendSmsUnitTest
    {
        [TestMethod]
        public async Task TestSendSmsAsync()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            var serviceProvider = serviceCollection
                .AddOptions()
                .AddElectNotificationEsms(_ =>
                {
                    _.ApiKey = "";
                    _.ApiSecret = "";
                })
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var esmsClient = scope.ServiceProvider.GetService<IElectEsmsClient>();

                var balance = await esmsClient.GetBalanceAsync().ConfigureAwait(true);
            }
        }
    }
}