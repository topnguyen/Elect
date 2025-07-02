namespace Elect.Notification.OneSignal.Interfaces
{
    public interface IElectOneSignalClient
    {
        IElectOneSignalApp Apps { get; }
        IElectOneSignalDevice Devices { get; }
        IElectOneSignalNotification Notifications { get; }
    }
}
