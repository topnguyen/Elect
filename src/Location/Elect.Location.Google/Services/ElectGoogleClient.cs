namespace Elect.Location.Google.Services
{
    public class ElectGoogleClient : IElectGoogleClient
    {
        public ElectLocationGoogleOptions Options { get; }
        public ElectGoogleClient()
        {
        }
        public ElectGoogleClient([NotNull]ElectLocationGoogleOptions configuration) : this()
        {
            Options = configuration;
        }
        public ElectGoogleClient([NotNull]Action<ElectLocationGoogleOptions> configuration) : this(configuration.GetValue())
        {
        }
        public ElectGoogleClient([NotNull]IOptions<ElectLocationGoogleOptions> configuration) : this(configuration.Value)
        {
        }
        #region Matrix
        public Task<DistanceDurationMatrixResultModel> GetDistanceDurationMatrixAsync([NotNull]Action<DistanceDurationMatrixRequestModel> model)
        {
            return GetDistanceDurationMatrixAsync(model.GetValue());
        }
        public async Task<DistanceDurationMatrixResultModel> GetDistanceDurationMatrixAsync([NotNull]DistanceDurationMatrixRequestModel model)
        {
            var origins = string.Join("|", model.OriginalCoordinates.Select(x => $"{x.Latitude},{x.Longitude}"));
            var destinations = string.Join("|", model.DestinationCoordinates.Select(x => $"{x.Latitude},{x.Longitude}"));
            var requestUrl =
                ElectLocationGoogleConstants.DefaultGoogleMatrixApiEndpoint
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectLocationGoogleConstants.NewtonsoftJsonSerializer;
                    })
                    .SetQueryParam("origins", origins)
                    .SetQueryParam("destinations", destinations)
                    .SetQueryParam("key", Options.GoogleApiKey)
                    .SetQueryParams(model.AdditionalValues);
            try
            {
                var result = await requestUrl.GetJsonAsync<DistanceDurationMatrixResultModel>().ConfigureAwait(true);
                return result;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);
                throw new HttpRequestException(response);
            }
        }
        #endregion
        #region Direction
        public Task<List<DirectionStepsResultModel>> GetDirectionsAsync([NotNull]Action<DirectionStepsRequestModel> model)
        {
            return GetDirectionsAsync(model.GetValue());
        }
        public async Task<List<DirectionStepsResultModel>> GetDirectionsAsync([NotNull]DirectionStepsRequestModel model)
        {
            string origin = $"{model.OriginalCoordinate.Latitude},{model.OriginalCoordinate.Longitude}";
            string destination = $"{model.DestinationCoordinate.Latitude},{model.DestinationCoordinate.Longitude}";
            string waypoints = string.Join("|", model.WaypointCoodinates.Select(x => $"{x.Latitude},{x.Longitude}"));
            var requestUrl =
                ElectLocationGoogleConstants.DefaultGoogleDirectionApiEndpoint
                    .ConfigureRequest(config =>
                    {
                        config.JsonSerializer = ElectLocationGoogleConstants.NewtonsoftJsonSerializer;
                    })
                    .SetQueryParam("origin", origin)
                    .SetQueryParam("destination", destination)
                    .SetQueryParam("waypoints", waypoints)
                    .SetQueryParam("avoidHighways", model.IsAvoidHighway)
                    .SetQueryParam("avoidTolls", model.IsAvoidToll)
                    .SetQueryParam("unitSystem", model.UnitSystem)
                    .SetQueryParam("travelMode", model.TravelMode)
                    .SetQueryParam("key", Options.GoogleApiKey)
                    .SetQueryParams(model.AdditionalValues);
            try
            {
                var xmlResult = await requestUrl.GetStringAsync().ConfigureAwait(true);
                var listDirectionStepsResult = new List<DirectionStepsResultModel>();
                var xmlDoc = new XmlDocument { InnerXml = xmlResult };
                if (!xmlDoc.HasChildNodes)
                {
                    return listDirectionStepsResult;
                }
                var directionsResponseNode = xmlDoc.SelectSingleNode("DirectionsResponse");
                var statusNode = directionsResponseNode?.SelectSingleNode("status");
                if (statusNode != null && statusNode.InnerText.Equals("OK"))
                {
                    var legs = directionsResponseNode.SelectNodes("route/leg");
                    if (legs == null) return listDirectionStepsResult;
                    foreach (XmlNode leg in legs)
                    {
                        int stepCount = 1;
                        var stepNodes = leg.SelectNodes("step");
                        var steps = (from XmlNode stepNode in stepNodes
                                     select new DirectionStepModel
                                     {
                                         Index = stepCount++,
                                         Distance = Convert.ToDouble(stepNode.SelectSingleNode("distance/value").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                                         DistanceText = stepNode.SelectSingleNode("distance/text").InnerText,
                                         Duration = Convert.ToDouble(stepNode.SelectSingleNode("duration/value").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                                         DurationText = stepNode.SelectSingleNode("duration/text").InnerText,
                                         Description = WebUtility.HtmlDecode(stepNode.SelectSingleNode("html_instructions").InnerText)
                                     }).ToList();
                        var directionSteps = new DirectionStepsResultModel
                        {
                            OriginPoint = new CoordinateModel
                            {
                                Latitude = Convert.ToDouble(leg.SelectSingleNode("start_location/lat").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                                Longitude = Convert.ToDouble(leg.SelectSingleNode("start_location/lng").InnerText, System.Globalization.CultureInfo.InvariantCulture)
                            },
                            OriginAddress = leg.SelectSingleNode("start_address").InnerText,
                            DestinationPoint = new CoordinateModel
                            {
                                Latitude = Convert.ToDouble(leg.SelectSingleNode("end_location/lat").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                                Longitude = Convert.ToDouble(leg.SelectSingleNode("end_location/lng").InnerText, System.Globalization.CultureInfo.InvariantCulture)
                            },
                            DestinationAddress = leg.SelectSingleNode("end_address").InnerText,
                            TotalDistance = Convert.ToDouble(leg.SelectSingleNode("distance/value").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                            TotalDistanceText = leg.SelectSingleNode("distance/text").InnerText,
                            TotalDuration = Convert.ToDouble(leg.SelectSingleNode("duration/value").InnerText, System.Globalization.CultureInfo.InvariantCulture),
                            TotalDurationText = leg.SelectSingleNode("duration/text").InnerText,
                            Steps = steps
                        };
                        listDirectionStepsResult.Add(directionSteps);
                    }
                }
                else if (statusNode != null && statusNode.InnerText.Equals("OVER_QUERY_LIMIT"))
                {
                    Thread.Sleep(1000);
                    listDirectionStepsResult = await GetDirectionsAsync(model).ConfigureAwait(true);
                }
                else
                {
                    throw new NotSupportedException(statusNode?.InnerText);
                }
                return listDirectionStepsResult;
            }
            catch (FlurlHttpException e)
            {
                var response = await e.GetResponseStringAsync().ConfigureAwait(true);
                throw new HttpRequestException(response);
            }
        }
        #endregion
        #region Trip
        public TripModel GetFastestAzTrip([NotNull]params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.AZ, null, coordinates);
        }
        public TripModel GetFastestAzTrip([CanBeNull]string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.AZ, googleApiKey, coordinates);
        }
        public TripModel GetFastestRoundTrip([NotNull]params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.RoundTrip, null, coordinates);
        }
        public TripModel GetFastestRoundTrip([CanBeNull]string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(TripType.RoundTrip, googleApiKey, coordinates);
        }
        public TripModel GetFastestTrip(TripType type, [NotNull]params CoordinateModel[] coordinates)
        {
            return GetFastestTrip(type, null, coordinates);
        }
        public TripModel GetFastestTrip(TripType type, [CanBeNull]string googleApiKey = null, [NotNull]params CoordinateModel[] coordinates)
        {
            FastestTrip fastestTrip = new FastestTrip(type, googleApiKey, coordinates);
            TripModel tripModel = new TripModel
            {
                CoordinateSequences = fastestTrip.GetTrip(),
                TotalDistanceInMeter = fastestTrip.GetTotalDistanceInMeter(),
                TotalDurationInSecond = fastestTrip.GetTotalDurationInSecond(),
                DistanceDurationMatrix = fastestTrip.DistanceDurationMatrix
            };
            return tripModel;
        }
        #endregion
    }
}
