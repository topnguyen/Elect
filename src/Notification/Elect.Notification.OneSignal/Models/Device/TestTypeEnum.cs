namespace Elect.Notification.OneSignal.Models.Device
{
    /// <summary>
    ///     Test type enumeration. 
    /// </summary>
    public enum TestTypeEnum
    {
        /// <summary>
        ///     Used during development phase. 
        /// </summary>
        Development = 1,
        /// <summary>
        ///     Used in production, when trying to track down undelivered messages for example. 
        /// </summary>
        AdHoc = 2
    }
}
