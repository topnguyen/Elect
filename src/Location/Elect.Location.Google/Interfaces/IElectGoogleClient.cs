#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IElectGoogleClient.cs </Name>
//         <Created> 20/03/2018 3:56:40 PM </Created>
//         <Key> 80829fe6-40fc-4973-ada0-c7a5c621544e </Key>
//     </File>
//     <Summary>
//         IElectGoogleClient.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Location.Google.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elect.Location.Google.Interfaces
{
    public interface IElectGoogleClient
    {
        Task<DistanceDurationMatrixResultModel> GetDistanceDurationMatrixAsync(Action<DistanceDurationMatrixRequestModel> model);

        Task<DistanceDurationMatrixResultModel> GetDistanceDurationMatrixAsync(DistanceDurationMatrixRequestModel model);

        Task<List<DirectionStepsResultModel>> GetDirectionsAsync(Action<DirectionStepsRequestModel> model);

        Task<List<DirectionStepsResultModel>> GetDirectionsAsync(DirectionStepsRequestModel model);
    }
}