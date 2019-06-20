#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalAppOption.cs </Name>
//         <Created> 20/03/2018 9:55:43 AM </Created>
//         <Key> 35b2cdd2-d242-4ec0-b017-91153224802c </Key>
//     </File>
//     <Summary>
//         ElectOneSignalAppOption.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Notification.OneSignal.Models
{
    public class ElectOneSignalAppOption
    {
        public ElectOneSignalAppOption()
        {
        }

        public ElectOneSignalAppOption(string appId, string apiKey)
        {
            AppId = appId;
            ApiKey = apiKey;
        }

        public ElectOneSignalAppOption(string appId, string apiKey, string appName)
        {
            AppId = appId;
            ApiKey = apiKey;
            AppName = appName;
        }

        /// <summary>
        ///     App Id use to identity an app
        /// </summary>
        public string AppId { get; set; }

        public string ApiKey { get; set; }

        /// <summary>
        ///     App Name to you can look easier
        /// </summary>
        public string AppName { get; set; }
    }
}