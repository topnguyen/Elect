#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DelayedOptionEnum.cs </Name>
//         <Created> 19/03/2018 9:41:46 PM </Created>
//         <Key> 8aa9246e-3baf-479b-8c3c-135cc0fefe34 </Key>
//     </File>
//     <Summary>
//         DelayedOptionEnum.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

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