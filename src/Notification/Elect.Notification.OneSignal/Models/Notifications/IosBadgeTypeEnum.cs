namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Describes whether to set or increase/decrease your app's iOS badge count by the
    ///     ios_badgeCount specified count. Can specify None, SetTo, or Increase.
    /// </summary>
    public enum IosBadgeTypeEnum
    {
        /// <summary>
        ///     Leaves the count unaffected. 
        /// </summary>
        None,
        /// <summary>
        ///     Directly sets the badge count to the number specified in ios_badgeCount. 
        /// </summary>
        SetTo,
        /// <summary>
        ///     Adds the number specified in ios_badgeCount to the total. Use a negative number to
        ///     decrease the badge count.
        /// </summary>
        Increase
    }
}
