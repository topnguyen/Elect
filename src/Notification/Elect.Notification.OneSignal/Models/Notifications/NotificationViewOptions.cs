#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationViewOptions.cs </Name>
//         <Created> 19/03/2018 9:45:16 PM </Created>
//         <Key> 015384af-d9d1-45f8-aee1-6ce9e512ad4b </Key>
//     </File>
//     <Summary>
//         NotificationViewOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using System;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Get delivery and convert report about single notification. See
    ///     <see cref="!:https://documentation.onesignal.com/reference#view-notification"> API
    ///     Documentation </see> for more info.
    /// </summary>
    public class NotificationViewOptions
    {
        /// <summary>
        ///     <br /> Your OneSignal application ID, which can be found on our dashboard at
        ///     onesignal.com under App Settings &gt; Keys &amp; IDs. <br /> It is a UUID and looks
        ///     similar to 8250eaf6-1a58-489e-b136-7c74a864b434. <br />
        /// </summary>
        [JsonProperty("app_id")]
        public Guid AppId { get; set; }

        /// <summary>
        ///     <br /> Notification ID 
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}