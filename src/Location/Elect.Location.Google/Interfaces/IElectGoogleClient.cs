#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
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

using Elect.Core.Attributes;
using Elect.Location.Google.Models;
using Elect.Location.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elect.Location.Google.Interfaces
{
    public interface IElectGoogleClient
    {
        #region Matrix

        Task<DistanceDurationMatrixResultModel> GetDistanceDurationMatrixAsync([NotNull]Action<DistanceDurationMatrixRequestModel> model);

        Task<DistanceDurationMatrixResultModel> GetDistanceDurationMatrixAsync([NotNull]DistanceDurationMatrixRequestModel model);

        #endregion

        #region Direction

        Task<List<DirectionStepsResultModel>> GetDirectionsAsync([NotNull]Action<DirectionStepsRequestModel> model);

        Task<List<DirectionStepsResultModel>> GetDirectionsAsync([NotNull]DirectionStepsRequestModel model);

        #endregion

        #region Trip

        TripModel GetFastestAzTrip([NotNull]params CoordinateModel[] coordinates);

        TripModel GetFastestAzTrip([CanBeNull]string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates);

        TripModel GetFastestRoundTrip([NotNull]params CoordinateModel[] coordinates);

        TripModel GetFastestRoundTrip([CanBeNull]string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates);

        TripModel GetFastestTrip(TripType type, [NotNull]params CoordinateModel[] coordinates);

        TripModel GetFastestTrip(TripType type, [CanBeNull]string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates);

        #endregion
    }
}