#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> WebButtonField.cs </Name>
//         <Created> 19/03/2018 9:45:45 PM </Created>
//         <Key> 71c7e71f-d9e8-46f3-8da8-5a307ec279fc </Key>
//     </File>
//     <Summary>
//         WebButtonField.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Class used to describe web button. 
    /// </summary>
    public class WebButtonFieldModel
    {
        /// <summary>
        ///     Web button ID. This is required field. 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        ///     Web button text. 
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        ///     Web button icon. 
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///     Web button url. 
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}