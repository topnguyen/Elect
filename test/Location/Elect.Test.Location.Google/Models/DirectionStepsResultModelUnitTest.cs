namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class DirectionStepsResultModelUnitTest
    {
        [TestMethod]
        public void OriginPoint_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var originPoint = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 };
            
            result.OriginPoint = originPoint;
            
            Assert.AreEqual(originPoint, result.OriginPoint);
        }

        [TestMethod]
        public void DestinationPoint_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var destinationPoint = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 };
            
            result.DestinationPoint = destinationPoint;
            
            Assert.AreEqual(destinationPoint, result.DestinationPoint);
        }

        [TestMethod]
        public void TotalDuration_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var totalDuration = 3600.5;
            
            result.TotalDuration = totalDuration;
            
            Assert.AreEqual(totalDuration, result.TotalDuration);
        }

        [TestMethod]
        public void TotalDurationText_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var totalDurationText = "1 hour";
            
            result.TotalDurationText = totalDurationText;
            
            Assert.AreEqual(totalDurationText, result.TotalDurationText);
        }

        [TestMethod]
        public void TotalDistance_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var totalDistance = 50000.75;
            
            result.TotalDistance = totalDistance;
            
            Assert.AreEqual(totalDistance, result.TotalDistance);
        }

        [TestMethod]
        public void TotalDistanceText_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var totalDistanceText = "50 km";
            
            result.TotalDistanceText = totalDistanceText;
            
            Assert.AreEqual(totalDistanceText, result.TotalDistanceText);
        }

        [TestMethod]
        public void OriginAddress_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var originAddress = "123 Main St, New York, NY";
            
            result.OriginAddress = originAddress;
            
            Assert.AreEqual(originAddress, result.OriginAddress);
        }

        [TestMethod]
        public void DestinationAddress_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var destinationAddress = "456 Oak Ave, Los Angeles, CA";
            
            result.DestinationAddress = destinationAddress;
            
            Assert.AreEqual(destinationAddress, result.DestinationAddress);
        }

        [TestMethod]
        public void Steps_GetSet_WorksCorrectly()
        {
            var result = new DirectionStepsResultModel();
            var steps = new List<DirectionStepModel>
            {
                new DirectionStepModel { Index = 1, Description = "Turn left" },
                new DirectionStepModel { Index = 2, Description = "Go straight" }
            };
            
            result.Steps = steps;
            
            Assert.AreEqual(steps, result.Steps);
            Assert.AreEqual(2, result.Steps.Count);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var result = new DirectionStepsResultModel();
            
            Assert.IsNull(result.OriginPoint);
            Assert.IsNull(result.DestinationPoint);
            Assert.AreEqual(0.0, result.TotalDuration);
            Assert.IsNull(result.TotalDurationText);
            Assert.AreEqual(0.0, result.TotalDistance);
            Assert.IsNull(result.TotalDistanceText);
            Assert.IsNull(result.OriginAddress);
            Assert.IsNull(result.DestinationAddress);
            Assert.IsNull(result.Steps);
        }
    }
}