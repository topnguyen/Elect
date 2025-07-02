namespace Elect.Notification.OneSignal.Services
{
    public class ElectOneSignalClient : IElectOneSignalClient
    {
        public IElectOneSignalApp Apps { get; }
        public IElectOneSignalDevice Devices { get; }
        public IElectOneSignalNotification Notifications { get; }
        public ElectOneSignalClient([NotNull] IElectOneSignalApp apps, [NotNull] IElectOneSignalDevice devices,
            [NotNull] IElectOneSignalNotification notifications)
        {
            Apps = apps;
            Devices = devices;
            Notifications = notifications;
        }
    }
}
