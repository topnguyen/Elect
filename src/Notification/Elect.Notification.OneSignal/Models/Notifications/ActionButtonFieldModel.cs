#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ActionButtonField.cs </Name>
//         <Created> 19/03/2018 9:38:07 PM </Created>
//         <Key> 754235c5-a533-4c4b-aed3-fc8bbee56040 </Key>
//     </File>
//     <Summary>
//         ActionButtonField.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    public class ActionButtonFieldModel
    {
        /// <summary>
        ///     Action button ID. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Action button text. 
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     Action button icon. 
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}