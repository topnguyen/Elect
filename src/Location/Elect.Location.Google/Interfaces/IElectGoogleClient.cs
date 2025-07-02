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
