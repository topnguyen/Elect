#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> NotificationFilterOperator.cs </Name>
//         <Created> 19/03/2018 9:44:55 PM </Created>
//         <Key> 4e739ebf-18fa-4517-92dc-cd549d1646e7 </Key>
//     </File>
//     <Summary>
//         NotificationFilterOperator.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.OneSignal.Interfaces;
using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Notification filter operator is used to define logical AND, OR 
    /// </summary>
    public class NotificationFilterOperatorModel : INotificationFilter
    {
        /// <summary>
        ///     Can be AND or OR operator 
        /// </summary>
        [JsonProperty("operator")]
        public string Operator { get; set; }
    }
}