#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalClient.cs </Name>
//         <Created> 19/03/2018 9:55:12 PM </Created>
//         <Key> c69c7e2a-0564-4fc2-ac74-96242000bb02 </Key>
//     </File>
//     <Summary>
//         ElectOneSignalClient.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.Models;
using Microsoft.Extensions.Options;

namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalClient : IElectOneSignalClient
    {
        public ElectOneSignalOptions Options { get; }

        public IElectOneSignalDevicesResource Devices { get; }

        public IElectOneSignalNotificationsResource Notifications { get; }

        public ElectOneSignalClient(IElectOneSignalDevicesResource devices, IElectOneSignalNotificationsResource notifications, IOptions<ElectOneSignalOptions> configurations)
        {
            Devices = devices;

            Notifications = notifications;

            Options = configurations.Value;
        }
    }
}