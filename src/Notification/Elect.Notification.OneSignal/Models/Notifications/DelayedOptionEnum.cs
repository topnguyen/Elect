namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Possible options for delaying notification. 
    /// </summary>
    public enum DelayedOptionEnum
    {
        /// <summary>
        ///     Deliver at a specific time-of-day in each users own timezone 
        /// </summary>
        TimeZone,
        /// <summary>
        ///     Deliver at the same time of day as each user last used your app. 
        /// </summary>
        LastActive,
        /// <summary>
        ///     If send_after is used, this takes effect after the send_after time has elapsed. 
        /// </summary>
        SendAfter
    }
}
