#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectOneSignalApp.cs </Name>
//         <Created> 19/03/2018 9:13:37 PM </Created>
//         <Key> b3e0be6d-784d-4696-bdac-01f8eea866e8 </Key>
//     </File>
//     <Summary>
//         IElectOneSignalApp.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elect.Notification.OneSignal.Models.App;

namespace Elect.Notification.OneSignal.Interfaces
{
    /// <summary>
    ///     Interface used to unify creation of classes used to help client add or edit app. 
    /// </summary>
    public interface IElectOneSignalApp
    {
        Task<AppInfoModel> GetAsync(string id, CancellationToken cancellationToken = default);

        Task<List<AppInfoModel>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<AppInfoModel> CreateAsync(AppAddModel model, CancellationToken cancellationToken = default);

        Task<AppInfoModel> EditAsync(string id, AppEditModel model, CancellationToken cancellationToken = default);
    }
}