#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectOneSignalDevice.cs </Name>
//         <Created> 19/03/2018 9:13:37 PM </Created>
//         <Key> b3e0be6d-784d-4696-bdac-01f8eea866e8 </Key>
//     </File>
//     <Summary>
//         IElectOneSignalDevice.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Attributes;
using Elect.Notification.OneSignal.Models;
using Elect.Notification.OneSignal.Models.Device;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Interfaces
{
    /// <summary>
    ///     Interface used to unify creation of classes used to help client add or edit device. 
    /// </summary>
    public interface IElectOneSignalDevice
    {
        #region Add

        /// <summary>
        ///     Adds new device into OneSignal App. 
        /// </summary>
        /// <param name="model"> Here you can specify options used to add new device. </param>
        /// <param name="appId">The app id must exist in ElectOneSignalOptions.Apps</param>
        /// <returns> Result of device add operation. </returns>
        Task<DeviceAddResultModel> AddAsync([NotNull]DeviceAddModel model, [NotNull]string appId);
        
        /// <summary>
        ///     Adds new device into OneSignal App. 
        /// </summary>
        /// <param name="model"> Here you can specify options used to add new device. </param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <returns> Result of device add operation. </returns>
        Task<DeviceAddResultModel> AddAsync([NotNull]DeviceAddModel model, [NotNull]string appId, [NotNull]string appKey);

        #endregion

        #region Edit

        /// <summary>
        ///     Edits existing device defined in OneSignal App. 
        /// </summary>
        /// <param name="id">      Id of the device </param>
        /// <param name="model"> Options used to modify attributes of the device. </param>
        /// <param name="appId">The app id must exist in ElectOneSignalOptions.Apps</param>
        /// <exception cref="System.Exception"></exception>
        Task EditAsync([NotNull]string id, [NotNull]DeviceEditModel model, [NotNull]string appId);
        
        /// <summary>
        ///     Edits existing device defined in OneSignal App. 
        /// </summary>
        /// <param name="id">      Id of the device </param>
        /// <param name="model"> Options used to modify attributes of the device. </param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <exception cref="System.Exception"></exception>
        Task EditAsync([NotNull]string id, [NotNull]DeviceEditModel model, [NotNull]string appId, [NotNull]string appKey);

        #endregion

        #region Get

        /// <summary>
        ///     Get device info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appId">The app id must exist in ElectOneSignalOptions.Apps</param>
        /// <returns></returns>
        Task<DeviceInfoModel> GetAsync([NotNull]string id, [NotNull]string appId);
        
        /// <summary>
        ///     Get device info 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appId"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        Task<DeviceInfoModel> GetAsync([NotNull]string id, [NotNull]string appId, [NotNull]string appKey);
        
        #endregion
    }
}