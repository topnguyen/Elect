﻿#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectOneSignalNotification.cs </Name>
//         <Created> 19/03/2018 9:14:21 PM </Created>
//         <Key> 2a50b62c-eb92-4f01-b2bf-90c999fd2b86 </Key>
//     </File>
//     <Summary>
//         IElectOneSignalNotification.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.Attributes;
using Elect.Notification.OneSignal.Models.Notifications;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Interfaces
{
    /// <summary>
    ///     Interface used to unify Notification Resource classes. 
    /// </summary>
    public interface IElectOneSignalNotification
    {
        #region Create

        /// <summary>
        ///     Creates a new notification. 
        /// </summary>
        /// <param name="model">
        ///     This parameter can contain variety of possible options used to create notification.
        /// </param>
        /// <param name="appId">The app id must exist in ElectOneSignalOptions.Apps</param>
        /// <returns> Returns result of notification create operation. </returns>
        Task<NotificationCreateResultModel>
            CreateAsync([NotNull] NotificationCreateModel model, [NotNull] string appId);

        /// <summary>
        ///     Creates a new notification. 
        /// </summary>
        /// <param name="model">
        ///     This parameter can contain variety of possible options used to create notification.
        /// </param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <returns> Returns result of notification create operation. </returns>
        Task<NotificationCreateResultModel> CreateAsync([NotNull] NotificationCreateModel model, [NotNull] string appId,
            [NotNull] string appKey);

        #endregion

        #region Cancel

        /// <summary>
        ///     Cancel notification Stop a scheduled or currently outgoing notification 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appId">The app id must exist in ElectOneSignalOptions.Apps</param>
        /// <returns> Returns result of notification cancel operation. </returns>
        Task<NotificationCancelResultModel> CancelAsync([NotNull] string id, [NotNull] string appId);

        /// <summary>
        ///     Cancel notification Stop a scheduled or currently outgoing notification 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <returns> Returns result of notification cancel operation. </returns>
        Task<NotificationCancelResultModel> CancelAsync([NotNull] string id, [NotNull] string appId,
            [NotNull] string appKey);

        #endregion

        #region Get

        /// <summary>
        ///     Get report about notification 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appId">The app id must exist in ElectOneSignalOptions.Apps</param>
        /// <returns> Returns result of notification create operation. </returns>
        Task<NotificationViewResultModel> GetAsync([NotNull] string id, [NotNull] string appId);

        /// <summary>
        ///     Get report about notification 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <returns> Returns result of notification create operation. </returns>
        Task<NotificationViewResultModel> GetAsync([NotNull] string id, [NotNull] string appId,
            [NotNull] string appKey);

        #endregion
    }
}