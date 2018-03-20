#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectOneSignalDevicesResource.cs </Name>
//         <Created> 19/03/2018 9:13:37 PM </Created>
//         <Key> b3e0be6d-784d-4696-bdac-01f8eea866e8 </Key>
//     </File>
//     <Summary>
//         IElectOneSignalDevicesResource.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Models.Device;
using System.Threading.Tasks;

namespace Elect.Notification.OneSignal.Interfaces
{
    /// <summary>
    ///     Interface used to unify creation of classes used to help client add or edit device. 
    /// </summary>
    public interface IElectOneSignalDevicesResource
    {
        /// <summary>
        ///     Adds new device into OneSignal App. 
        /// </summary>
        /// <param name="options"> Here you can specify options used to add new device. </param>
        /// <param name="appName">
        ///     App name - optional if you have only 1 app in configuration, default is <see cref="ElectOneSignalConstants.DefaultApiUrl" />.
        /// </param>
        /// <returns> Result of device add operation. </returns>
        Task<DeviceAddResult> AddAsync(DeviceAddOptions options, string appName = ElectOneSignalConstants.DefaultAppName);

        /// <summary>
        ///     Edits existing device defined in OneSignal App. 
        /// </summary>
        /// <param name="id">      Id of the device </param>
        /// <param name="options"> Options used to modify attributes of the device. </param>
        /// <param name="appName">
        ///     App name - optional if you have only 1 app in configuration, default is <see cref="ElectOneSignalConstants.DefaultApiUrl" />.
        /// </param>
        /// <exception cref="System.Exception"></exception>
        Task EditAsync(string id, DeviceEditOptions options, string appName = ElectOneSignalConstants.DefaultAppName);

        /// <summary>
        ///     Get device info 
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="appName"> 
        ///     App name - optional if you have only 1 app in configuration, default is <see cref="ElectOneSignalConstants.DefaultApiUrl" />.
        /// </param>
        /// <returns></returns>
        Task<DeviceInfo> GetAsync(string playerId, string appName = ElectOneSignalConstants.DefaultAppName);
    }
}