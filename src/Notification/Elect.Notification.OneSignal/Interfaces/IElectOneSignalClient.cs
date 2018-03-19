#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectOneSignalClient.cs </Name>
//         <Created> 19/03/2018 9:12:54 PM </Created>
//         <Key> 47e5dbe1-04c9-445c-8a5b-b235d04a484e </Key>
//     </File>
//     <Summary>
//         IElectOneSignalClient.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Notification.OneSignal.Interfaces
{
    public interface IElectOneSignalClient
    {
        IElectOneSignalDevicesResource Devices { get; }

        IElectOneSignalNotificationsResource Notifications { get; }
    }
}