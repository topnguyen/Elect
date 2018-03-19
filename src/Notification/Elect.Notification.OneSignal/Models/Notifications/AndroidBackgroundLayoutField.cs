#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> AndroidBackgroundLayoutField.cs </Name>
//         <Created> 19/03/2018 9:38:23 PM </Created>
//         <Key> 444c9bce-8f06-4b74-ad43-d6b8eedeafe4 </Key>
//     </File>
//     <Summary>
//         AndroidBackgroundLayoutField.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;

namespace Elect.Notification.OneSignal.Models.Notifications
{
    public class AndroidBackgroundLayoutField
    {
        /// <summary>
        ///     Background image. 
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        ///     Background heading color. 
        /// </summary>
        [JsonProperty("headings_color")]
        public string HeadingsColor { get; set; }

        /// <summary>
        ///     Background content color. 
        /// </summary>
        [JsonProperty("contents_color")]
        public string ContentsColor { get; set; }
    }
}