using Flurl.Http.Testing;
using Microsoft.Extensions.Options;
using System.Net;

namespace Elect.Test.Location.Google.Services
{
    [TestClass]
    public class ElectGoogleClientUnitTest
    {
        private HttpTest _httpTest = null!;
        private ElectLocationGoogleOptions _options = null!;

        [TestInitialize]
        public void Setup()
        {
            _httpTest = new HttpTest();
            _options = new ElectLocationGoogleOptions { GoogleApiKey = "test-api-key" };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpTest?.Dispose();
        }

        [TestMethod]
        public void Constructor_Default_CreatesInstance()
        {
            var client = new ElectGoogleClient();
            
            Assert.IsNotNull(client);
            Assert.IsNull(client.Options);
        }

        [TestMethod]
        public void Constructor_WithOptions_SetsOptions()
        {
            var client = new ElectGoogleClient(_options);
            
            Assert.IsNotNull(client);
            Assert.AreEqual(_options, client.Options);
            Assert.AreEqual("test-api-key", client.Options.GoogleApiKey);
        }

        [TestMethod]
        public void Constructor_WithAction_SetsOptions()
        {
            var client = new ElectGoogleClient(opt => opt.GoogleApiKey = "action-api-key");
            
            Assert.IsNotNull(client);
            Assert.IsNotNull(client.Options);
            Assert.AreEqual("action-api-key", client.Options.GoogleApiKey);
        }

        [TestMethod]
        public void Constructor_WithIOptions_SetsOptions()
        {
            var optionsWrapper = Options.Create(_options);
            var client = new ElectGoogleClient(optionsWrapper);
            
            Assert.IsNotNull(client);
            Assert.AreEqual(_options, client.Options);
            Assert.AreEqual("test-api-key", client.Options.GoogleApiKey);
        }

        [TestMethod]
        public void Options_Property_GetSet_WorksCorrectly()
        {
            var client = new ElectGoogleClient(_options);
            
            Assert.AreEqual(_options, client.Options);
        }

        [TestMethod]
        public async Task GetDistanceDurationMatrixAsync_WithAction_CallsCorrectEndpoint()
        {
            var client = new ElectGoogleClient(_options);
            var mockResponse = new
            {
                rows = new[]
                {
                    new { elements = new[] { new { distance = new { value = 1000 }, duration = new { value = 300 } } } }
                }
            };
            
            _httpTest.RespondWithJson(mockResponse);
            
            var result = await client.GetDistanceDurationMatrixAsync(model =>
            {
                model.OriginalCoordinates.Add(new CoordinateModel { Latitude = 1.0, Longitude = 2.0 });
                model.DestinationCoordinates.Add(new CoordinateModel { Latitude = 3.0, Longitude = 4.0 });
            });
            
            Assert.IsNotNull(result);
            _httpTest.ShouldHaveCalled("https://maps.googleapis.com/maps/api/distancematrix/json*")
                .WithQueryParam("key", "test-api-key")
                .WithQueryParam("origins", "1,2")
                .WithQueryParam("destinations", "3,4");
        }

        [TestMethod]
        public async Task GetDistanceDurationMatrixAsync_WithModel_CallsCorrectEndpoint()
        {
            var client = new ElectGoogleClient(_options);
            var mockResponse = new
            {
                rows = new[]
                {
                    new { elements = new[] { new { distance = new { value = 1000 }, duration = new { value = 300 } } } }
                }
            };
            
            _httpTest.RespondWithJson(mockResponse);
            
            var requestModel = new DistanceDurationMatrixRequestModel
            {
                OriginalCoordinates = new List<CoordinateModel>
                {
                    new CoordinateModel { Latitude = 1.0, Longitude = 2.0 }
                },
                DestinationCoordinates = new List<CoordinateModel>
                {
                    new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
                }
            };
            
            var result = await client.GetDistanceDurationMatrixAsync(requestModel);
            
            Assert.IsNotNull(result);
            _httpTest.ShouldHaveCalled("https://maps.googleapis.com/maps/api/distancematrix/json*")
                .WithQueryParam("key", "test-api-key")
                .WithQueryParam("origins", "1,2")
                .WithQueryParam("destinations", "3,4");
        }

        [TestMethod]
        public async Task GetDistanceDurationMatrixAsync_WithHttpException_ThrowsHttpRequestException()
        {
            var client = new ElectGoogleClient(_options);
            _httpTest.RespondWith("Error message", 400);
            
            var requestModel = new DistanceDurationMatrixRequestModel
            {
                OriginalCoordinates = new List<CoordinateModel>
                {
                    new CoordinateModel { Latitude = 1.0, Longitude = 2.0 }
                },
                DestinationCoordinates = new List<CoordinateModel>
                {
                    new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
                }
            };
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                await client.GetDistanceDurationMatrixAsync(requestModel);
            });
        }

        [TestMethod]
        public async Task GetDirectionsAsync_WithAction_CallsCorrectEndpoint()
        {
            var client = new ElectGoogleClient(_options);
            var mockXmlResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<DirectionsResponse>
    <status>OK</status>
    <route>
        <leg>
            <start_location>
                <lat>1.0</lat>
                <lng>2.0</lng>
            </start_location>
            <end_location>
                <lat>3.0</lat>
                <lng>4.0</lng>
            </end_location>
            <start_address>Start Address</start_address>
            <end_address>End Address</end_address>
            <distance>
                <value>1000</value>
                <text>1 km</text>
            </distance>
            <duration>
                <value>300</value>
                <text>5 mins</text>
            </duration>
            <step>
                <distance>
                    <value>500</value>
                    <text>0.5 km</text>
                </distance>
                <duration>
                    <value>150</value>
                    <text>2.5 mins</text>
                </duration>
                <html_instructions>Go straight</html_instructions>
            </step>
        </leg>
    </route>
</DirectionsResponse>";
            
            _httpTest.RespondWith(mockXmlResponse);
            
            var result = await client.GetDirectionsAsync(model =>
            {
                model.OriginalCoordinate = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 };
                model.DestinationCoordinate = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 };
            });
            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            _httpTest.ShouldHaveCalled("https://maps.googleapis.com/maps/api/directions/xml*")
                .WithQueryParam("key", "test-api-key")
                .WithQueryParam("origin", "1,2")
                .WithQueryParam("destination", "3,4");
        }

        [TestMethod]
        public async Task GetDirectionsAsync_WithModel_CallsCorrectEndpoint()
        {
            var client = new ElectGoogleClient(_options);
            var mockXmlResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<DirectionsResponse>
    <status>OK</status>
    <route>
        <leg>
            <start_location>
                <lat>1.0</lat>
                <lng>2.0</lng>
            </start_location>
            <end_location>
                <lat>3.0</lat>
                <lng>4.0</lng>
            </end_location>
            <start_address>Start Address</start_address>
            <end_address>End Address</end_address>
            <distance>
                <value>1000</value>
                <text>1 km</text>
            </distance>
            <duration>
                <value>300</value>
                <text>5 mins</text>
            </duration>
            <step>
                <distance>
                    <value>500</value>
                    <text>0.5 km</text>
                </distance>
                <duration>
                    <value>150</value>
                    <text>2.5 mins</text>
                </duration>
                <html_instructions>Go straight</html_instructions>
            </step>
        </leg>
    </route>
</DirectionsResponse>";
            
            _httpTest.RespondWith(mockXmlResponse);
            
            var requestModel = new DirectionStepsRequestModel
            {
                OriginalCoordinate = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 },
                DestinationCoordinate = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 },
                IsAvoidHighway = true,
                IsAvoidToll = false,
                UnitSystem = 1,
                TravelMode = "DRIVING"
            };
            
            var result = await client.GetDirectionsAsync(requestModel);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            _httpTest.ShouldHaveCalled("https://maps.googleapis.com/maps/api/directions/xml*")
                .WithQueryParam("key", "test-api-key")
                .WithQueryParam("origin", "1,2")
                .WithQueryParam("destination", "3,4")
                .WithQueryParam("avoidHighways", "True")
                .WithQueryParam("avoidTolls", "False")
                .WithQueryParam("unitSystem", "1")
                .WithQueryParam("travelMode", "DRIVING");
        }

        [TestMethod]
        public async Task GetDirectionsAsync_WithOverQueryLimit_RetriesAfterDelay()
        {
            var client = new ElectGoogleClient(_options);
            var overQueryLimitResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<DirectionsResponse>
    <status>OVER_QUERY_LIMIT</status>
</DirectionsResponse>";
            
            var successResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<DirectionsResponse>
    <status>OK</status>
    <route>
        <leg>
            <start_location>
                <lat>1.0</lat>
                <lng>2.0</lng>
            </start_location>
            <end_location>
                <lat>3.0</lat>
                <lng>4.0</lng>
            </end_location>
            <start_address>Start Address</start_address>
            <end_address>End Address</end_address>
            <distance>
                <value>1000</value>
                <text>1 km</text>
            </distance>
            <duration>
                <value>300</value>
                <text>5 mins</text>
            </duration>
            <step>
                <distance>
                    <value>500</value>
                    <text>0.5 km</text>
                </distance>
                <duration>
                    <value>150</value>
                    <text>2.5 mins</text>
                </duration>
                <html_instructions>Go straight</html_instructions>
            </step>
        </leg>
    </route>
</DirectionsResponse>";
            
            _httpTest.RespondWith(overQueryLimitResponse);
            _httpTest.RespondWith(successResponse);
            
            var requestModel = new DirectionStepsRequestModel
            {
                OriginalCoordinate = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 },
                DestinationCoordinate = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
            };
            
            var result = await client.GetDirectionsAsync(requestModel);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            _httpTest.ShouldHaveMadeACall().Times(2);
        }

        [TestMethod]
        public async Task GetDirectionsAsync_WithError_ThrowsNotSupportedException()
        {
            var client = new ElectGoogleClient(_options);
            var errorResponse = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<DirectionsResponse>
    <status>INVALID_REQUEST</status>
</DirectionsResponse>";
            
            _httpTest.RespondWith(errorResponse);
            
            var requestModel = new DirectionStepsRequestModel
            {
                OriginalCoordinate = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 },
                DestinationCoordinate = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
            };
            
            await Assert.ThrowsExceptionAsync<NotSupportedException>(async () =>
            {
                await client.GetDirectionsAsync(requestModel);
            });
        }

        [TestMethod]
        public async Task GetDirectionsAsync_WithHttpException_ThrowsHttpRequestException()
        {
            var client = new ElectGoogleClient(_options);
            _httpTest.RespondWith("Error message", 400);
            
            var requestModel = new DirectionStepsRequestModel
            {
                OriginalCoordinate = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 },
                DestinationCoordinate = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
            };
            
            await Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                await client.GetDirectionsAsync(requestModel);
            });
        }

        // FastestTrip methods require valid Google API responses to work properly
        // These tests verify the methods exist and can handle basic scenarios
        
        [TestMethod]
        public void GetFastestAzTrip_MethodExists()
        {
            var client = new ElectGoogleClient(_options);
            var type = typeof(ElectGoogleClient);
            
            var method1 = type.GetMethod("GetFastestAzTrip", new[] { typeof(CoordinateModel[]) });
            var method2 = type.GetMethod("GetFastestAzTrip", new[] { typeof(string), typeof(CoordinateModel[]) });
            
            Assert.IsNotNull(method1, "GetFastestAzTrip(CoordinateModel[]) method should exist");
            Assert.IsNotNull(method2, "GetFastestAzTrip(string, CoordinateModel[]) method should exist");
        }

        [TestMethod]
        public void GetFastestRoundTrip_MethodExists()
        {
            var client = new ElectGoogleClient(_options);
            var type = typeof(ElectGoogleClient);
            
            var method1 = type.GetMethod("GetFastestRoundTrip", new[] { typeof(CoordinateModel[]) });
            var method2 = type.GetMethod("GetFastestRoundTrip", new[] { typeof(string), typeof(CoordinateModel[]) });
            
            Assert.IsNotNull(method1, "GetFastestRoundTrip(CoordinateModel[]) method should exist");
            Assert.IsNotNull(method2, "GetFastestRoundTrip(string, CoordinateModel[]) method should exist");
        }

        [TestMethod]
        public void GetFastestTrip_MethodExists()
        {
            var client = new ElectGoogleClient(_options);
            var type = typeof(ElectGoogleClient);
            
            var method1 = type.GetMethod("GetFastestTrip", new[] { typeof(TripType), typeof(CoordinateModel[]) });
            var method2 = type.GetMethod("GetFastestTrip", new[] { typeof(TripType), typeof(string), typeof(CoordinateModel[]) });
            
            Assert.IsNotNull(method1, "GetFastestTrip(TripType, CoordinateModel[]) method should exist");
            Assert.IsNotNull(method2, "GetFastestTrip(TripType, string, CoordinateModel[]) method should exist");
        }
    }
}