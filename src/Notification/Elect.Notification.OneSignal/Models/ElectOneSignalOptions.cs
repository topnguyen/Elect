#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectOneSignalOptions.cs </Name>
//         <Created> 19/03/2018 9:27:39 PM </Created>
//         <Key> 9d8725d1-9372-425a-acaf-e8ad3138bc5f </Key>
//     </File>
//     <Summary>
//         ElectOneSignalOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Collections.Generic;

namespace Elect.Notification.OneSignal.Models
{
    public class ElectOneSignalOptions
    {
        public List<ElectOneSignalAppOption> Apps { get; set; } = new List<ElectOneSignalAppOption>();
    }
}