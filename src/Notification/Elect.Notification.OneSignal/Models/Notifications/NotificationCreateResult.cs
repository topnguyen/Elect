#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationCreateResult.cs </Name>
//         <Created> 19/03/2018 9:44:20 PM </Created>
//         <Key> 34ed38d4-4550-43a1-a255-0c16b2b75d94 </Key>
//     </File>
//     <Summary>
//         NotificationCreateResult.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Result of notification create operation. 
    /// </summary>
    public class NotificationCreateResult
    {
        /// <summary>
        ///     Returns the number of recipients who received the message. 
        /// </summary>
        [JsonProperty("recipients")]
        public int Recipients { get; set; }

        /// <summary>
        ///     Returns the id of the result. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}