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
