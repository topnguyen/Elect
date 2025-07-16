namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class DirectionStepsRequestModelUnitTest
    {
        [TestMethod]
        public void OriginalCoordinate_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            var coordinate = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 };
            
            request.OriginalCoordinate = coordinate;
            
            Assert.AreEqual(coordinate, request.OriginalCoordinate);
        }

        [TestMethod]
        public void DestinationCoordinate_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            var coordinate = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 };
            
            request.DestinationCoordinate = coordinate;
            
            Assert.AreEqual(coordinate, request.DestinationCoordinate);
        }

        [TestMethod]
        public void WaypointCoodinates_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            var waypoints = new List<CoordinateModel>
            {
                new CoordinateModel { Latitude = 5.0, Longitude = 6.0 },
                new CoordinateModel { Latitude = 7.0, Longitude = 8.0 }
            };
            
            request.WaypointCoodinates = waypoints;
            
            Assert.AreEqual(waypoints, request.WaypointCoodinates);
            Assert.AreEqual(2, request.WaypointCoodinates.Count);
        }

        [TestMethod]
        public void IsAvoidHighway_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            
            request.IsAvoidHighway = true;
            
            Assert.IsTrue(request.IsAvoidHighway);
        }

        [TestMethod]
        public void IsAvoidToll_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            
            request.IsAvoidToll = true;
            
            Assert.IsTrue(request.IsAvoidToll);
        }

        [TestMethod]
        public void UnitSystem_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            var unitSystem = 2;
            
            request.UnitSystem = unitSystem;
            
            Assert.AreEqual(unitSystem, request.UnitSystem);
        }

        [TestMethod]
        public void TravelMode_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            var travelMode = "WALKING";
            
            request.TravelMode = travelMode;
            
            Assert.AreEqual(travelMode, request.TravelMode);
        }

        [TestMethod]
        public void AdditionalValues_GetSet_WorksCorrectly()
        {
            var request = new DirectionStepsRequestModel();
            var additionalValues = new Dictionary<string, string>
            {
                { "language", "en-US" },
                { "region", "us" }
            };
            
            request.AdditionalValues = additionalValues;
            
            Assert.AreEqual(additionalValues, request.AdditionalValues);
            Assert.AreEqual(2, request.AdditionalValues.Count);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var request = new DirectionStepsRequestModel();
            
            Assert.IsNull(request.OriginalCoordinate);
            Assert.IsNull(request.DestinationCoordinate);
            Assert.IsNotNull(request.WaypointCoodinates);
            Assert.AreEqual(0, request.WaypointCoodinates.Count);
            Assert.IsFalse(request.IsAvoidHighway);
            Assert.IsFalse(request.IsAvoidToll);
            Assert.AreEqual(1, request.UnitSystem);
            Assert.AreEqual("DRIVING", request.TravelMode);
            Assert.IsNotNull(request.AdditionalValues);
            Assert.AreEqual(0, request.AdditionalValues.Count);
        }
    }
}