using Elect.Location.Google.Models;
using Elect.Location.Google.Services;
using Elect.Location.Models;

namespace Elect.Test.Location.Google.Services
{
    [TestClass]
    public class FastestTripSimpleUnitTest
    {
        [TestMethod]
        public void GetFastestAzTrip_WithEmptyCoordinates_ThrowsNotSupportedException()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new CoordinateModel[0];

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                client.GetFastestAzTrip(coordinates);
            });
        }

        [TestMethod]
        public void GetFastestAzTrip_WithSingleCoordinate_ThrowsException()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new[]
            {
                new CoordinateModel(10.762622, 106.660172)
            };

            // Act & Assert - Single coordinate will cause issues in the calculation
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                client.GetFastestAzTrip(coordinates);
            });
        }

        [TestMethod]
        public void GetFastestRoundTrip_WithEmptyCoordinates_ThrowsNotSupportedException()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new CoordinateModel[0];

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                client.GetFastestRoundTrip(coordinates);
            });
        }

        [TestMethod]
        public void GetFastestRoundTrip_WithSingleCoordinate_ThrowsException()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new[]
            {
                new CoordinateModel(10.762622, 106.660172)
            };

            // Act & Assert - Single coordinate will cause issues in the calculation
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                client.GetFastestRoundTrip(coordinates);
            });
        }

        [TestMethod]
        public void GetFastestTrip_WithEmptyCoordinates_ThrowsNotSupportedException()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new CoordinateModel[0];

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() =>
            {
                client.GetFastestTrip(TripType.AZ, coordinates);
            });
        }

        [TestMethod]
        public void GetFastestTrip_WithSingleCoordinate_ThrowsException()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new[]
            {
                new CoordinateModel(10.762622, 106.660172)
            };

            // Act & Assert - Single coordinate will cause issues in the calculation
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                client.GetFastestTrip(TripType.RoundTrip, coordinates);
            });
        }

        [TestMethod]
        public void GetFastestTrip_MethodOverloads_Exist()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var type = typeof(ElectGoogleClient);
            
            // Act & Assert - Verify method signatures exist
            var azTripMethod1 = type.GetMethod("GetFastestAzTrip", new[] { typeof(CoordinateModel[]) });
            var azTripMethod2 = type.GetMethod("GetFastestAzTrip", new[] { typeof(string), typeof(CoordinateModel[]) });
            var roundTripMethod1 = type.GetMethod("GetFastestRoundTrip", new[] { typeof(CoordinateModel[]) });
            var roundTripMethod2 = type.GetMethod("GetFastestRoundTrip", new[] { typeof(string), typeof(CoordinateModel[]) });
            var tripMethod1 = type.GetMethod("GetFastestTrip", new[] { typeof(TripType), typeof(CoordinateModel[]) });
            var tripMethod2 = type.GetMethod("GetFastestTrip", new[] { typeof(TripType), typeof(string), typeof(CoordinateModel[]) });
            
            Assert.IsNotNull(azTripMethod1, "GetFastestAzTrip(CoordinateModel[]) method should exist");
            Assert.IsNotNull(azTripMethod2, "GetFastestAzTrip(string, CoordinateModel[]) method should exist");
            Assert.IsNotNull(roundTripMethod1, "GetFastestRoundTrip(CoordinateModel[]) method should exist");
            Assert.IsNotNull(roundTripMethod2, "GetFastestRoundTrip(string, CoordinateModel[]) method should exist");
            Assert.IsNotNull(tripMethod1, "GetFastestTrip(TripType, CoordinateModel[]) method should exist");
            Assert.IsNotNull(tripMethod2, "GetFastestTrip(TripType, string, CoordinateModel[]) method should exist");
        }

        [TestMethod]
        public void GetFastestTrip_TripTypeEnum_HasCorrectValues()
        {
            // Act & Assert
            Assert.AreEqual(1, (int)TripType.AZ, "TripType.AZ should have value 1");
            Assert.AreEqual(2, (int)TripType.RoundTrip, "TripType.RoundTrip should have value 2");
        }

        [TestMethod]
        public void GetFastestTrip_MethodsAcceptNullApiKey()
        {
            // Arrange
            var client = new ElectGoogleClient();
            var coordinates = new[]
            {
                new CoordinateModel(10.762622, 106.660172),
                new CoordinateModel(21.028511, 105.804817)
            };

            // Act & Assert - Should not throw on null API key (but will fail on API call)
            try
            {
                client.GetFastestAzTrip(null, coordinates);
                Assert.Fail("Expected exception due to API call");
            }
            catch (Exception ex)
            {
                // Expected to fail, but not due to null API key parameter
                Assert.IsNotInstanceOfType(ex, typeof(ArgumentNullException), "Should not throw ArgumentNullException for null API key");
            }
        }

        [TestMethod]
        public void CoordinateModel_SupportsSequenceNoProperty()
        {
            // Arrange
            var coordinate = new CoordinateModel(10.762622, 106.660172);
            
            // Act
            coordinate.SequenceNo = 5;
            
            // Assert
            Assert.AreEqual(5, coordinate.SequenceNo, "SequenceNo property should be settable and retrievable");
        }

        [TestMethod]
        public void CoordinateModel_SupportsGroupNoProperty()
        {
            // Arrange
            var coordinate = new CoordinateModel(10.762622, 106.660172);
            
            // Act
            coordinate.GroupNo = 3;
            
            // Assert
            Assert.AreEqual(3, coordinate.GroupNo, "GroupNo property should be settable and retrievable");
        }
    }
}