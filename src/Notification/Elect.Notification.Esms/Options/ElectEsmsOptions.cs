#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectEsmsOptions.cs </Name>
//         <Created> 17/03/2018 9:30:34 AM </Created>
//         <Key> 38a344d2-0e9d-434b-840b-8ed6cbd87184 </Key>
//     </File>
//     <Summary>
//         ElectEsmsOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.Interfaces;

namespace Elect.Notification.Esms.Options
{
    public class ElectEsmsOptions : IElectOptions
    {
        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }

        public string ApiUri { get; set; } = " https://restapi.esms.vn";
    }
}