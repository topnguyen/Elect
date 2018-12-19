#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationCancelResult.cs </Name>
//         <Created> 19/03/2018 9:43:45 PM </Created>
//         <Key> 30627c0f-b7a0-4dac-b687-14bcbc30d2c4 </Key>
//     </File>
//     <Summary>
//         NotificationCancelResult.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Result of notification cancel operation. 
    /// </summary>
    public class NotificationCancelResultModel
    {
        /// <summary>
        ///     Returns whether the message was canceled or not {'success': "true"} 
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public string Success { get; set; }
    }
}