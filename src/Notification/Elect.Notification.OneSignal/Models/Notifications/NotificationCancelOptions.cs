#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationCancelOptions.cs </Name>
//         <Created> 19/03/2018 9:43:29 PM </Created>
//         <Key> 33761333-3c76-4bdc-9001-2332edd3e978 </Key>
//     </File>
//     <Summary>
//         NotificationCancelOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     API Documentation: https://documentation.onesignal.com/docs/notifications-cancel-notification 
    /// </summary>
    public class NotificationCancelOptions
    {
        /// <summary>
        ///     id String Required Notification id 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     app_id String Required App id 
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }
    }
}