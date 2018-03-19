#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectEsmsClient.cs </Name>
//         <Created> 19/03/2018 8:07:58 PM </Created>
//         <Key> 65fd69fb-9f48-4972-9d91-7a5205af925c </Key>
//     </File>
//     <Summary>
//         IElectEsmsClient.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Notification.Esms.Models;
using System.Threading.Tasks;

namespace Elect.Notification.Esms.Interfaces
{
    public interface IElectEsmsClient
    {
        ElectEsmsOptions Options { get; }

        Task<SendSmsResponseModel> SendAsync(SendSmsModel model);

        Task<BalanceModel> GetBalanceAsync();
    }
}