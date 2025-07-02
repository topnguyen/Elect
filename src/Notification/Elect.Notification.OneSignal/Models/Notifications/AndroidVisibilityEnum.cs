namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Types of visibility for apps targeting Android API level 21+ running on Android 5.0+ devices.
    /// </summary>
    public enum AndroidVisibilityEnum
    {
        /// <summary>
        ///     Public (default) (Shows the full message on the lock screen unless the user has
        ///     disabled all notifications from showing on the lock screen. Please consider the user
        ///     and mark private if the contents are.)
        /// </summary>
        Public = 1,
        /// <summary>
        ///     Private (Hides message contents on lock screen if the user set "Hide sensitive
        ///     notification content" in the system settings)
        /// </summary>
        Private = 0,
        /// <summary>
        ///     Secret (Notification does not show on the lock screen at all) 
        /// </summary>
        Secret = -1
    }
}
