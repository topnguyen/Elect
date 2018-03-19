#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectOneSignalNotificationsResource.cs </Name>
//         <Created> 19/03/2018 9:14:21 PM </Created>
//         <Key> 2a50b62c-eb92-4f01-b2bf-90c999fd2b86 </Key>
//     </File>
//     <Summary>
//         IElectOneSignalNotificationsResource.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Models.Notifications;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Interfaces
{
    /// <summary>
    ///     Interface used to unify Notification Resource classes. 
    /// </summary>
    public interface IElectOneSignalNotificationsResource
    {
        /// <summary>
        ///     Creates a new notification. 
        /// </summary>
        /// <param name="options">
        ///     This parameter can contain variety of possible options used to create notification.
        /// </param>
        /// <returns> Returns result of notification create operation. </returns>
        Task<NotificationCreateResult> CreateAsync(NotificationCreateOptions options);

        /// <summary>
        ///     Cancel notification Stop a scheduled or currently outgoing notification 
        /// </summary>
        /// <param name="options">
        ///     This parameter contains the information required to cancel a scheduled notification
        /// </param>
        /// <returns> Returns result of notification cancel operation. </returns>
        Task<NotificationCancelResult> CancelAsync(NotificationCancelOptions options);

        /// <summary>
        ///     Get report about notification 
        /// </summary>
        /// <param name="options">
        ///     This parameter can contain variety of possible options used to create notification.
        /// </param>
        /// <returns> Returns result of notification create operation. </returns>
        Task<NotificationViewResult> ViewAsync(NotificationViewOptions options);
    }
}