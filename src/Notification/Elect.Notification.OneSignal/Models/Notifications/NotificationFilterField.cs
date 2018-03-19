#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationFilterField.cs </Name>
//         <Created> 19/03/2018 9:44:34 PM </Created>
//         <Key> 88af95a3-29bc-48ff-833b-5f722ab3e4f4 </Key>
//     </File>
//     <Summary>
//         NotificationFilterField.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Interfaces;
using Elect.Notification.OneSignal.JsonConverter;
using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Complex type used to describe filter field. 
    /// </summary>
    public class NotificationFilterField : INotificationFilter
    {
        /// <summary>
        ///     The type of the filter field. 
        /// </summary>
        [JsonProperty("field")]
        [JsonConverter(typeof(NotificationFilterFieldTypeConverter))]
        public NotificationFilterFieldTypeEnum Field { get; set; }

        /// <summary>
        ///     The key used for comparison. 
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        ///     The relation between key and value. 
        /// </summary>
        [JsonProperty("relation")]
        public string Relation { get; set; }

        /// <summary>
        ///     The value. 
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}