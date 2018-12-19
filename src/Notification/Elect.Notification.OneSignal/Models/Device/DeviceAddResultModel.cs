#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DeviceAddResult.cs </Name>
//         <Created> 19/03/2018 9:17:06 PM </Created>
//         <Key> 5c9ce3c4-6cf0-4483-b593-735927f6b047 </Key>
//     </File>
//     <Summary>
//         DeviceAddResult.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Device
{
    /// <summary>
    ///     Class used to keep result of device add operation. 
    /// </summary>
    public class DeviceAddResultModel
    {
        /// <summary>
        ///     Returns true if operation is successful. 
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     Returns id of the result operation. 
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }
}