#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> AndroidVisibilityEnum.cs </Name>
//         <Created> 19/03/2018 9:40:58 PM </Created>
//         <Key> e5e32c56-9b51-4c9b-92de-4dbe8f437d13 </Key>
//     </File>
//     <Summary>
//         AndroidVisibilityEnum.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

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