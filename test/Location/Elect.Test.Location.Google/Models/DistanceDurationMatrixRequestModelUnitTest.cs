namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class DistanceDurationMatrixRequestModelUnitTest
    {
        [TestMethod]
        public void OriginalCoordinates_GetSet_WorksCorrectly()
        {
            var request = new DistanceDurationMatrixRequestModel();
            var coordinates = new List<CoordinateModel>
            {
                new CoordinateModel { Latitude = 1.0, Longitude = 2.0 },
                new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
            };
            
            request.OriginalCoordinates = coordinates;
            
            Assert.AreEqual(coordinates, request.OriginalCoordinates);
            Assert.AreEqual(2, request.OriginalCoordinates.Count);
        }

        [TestMethod]
        public void DestinationCoordinates_GetSet_WorksCorrectly()
        {
            var request = new DistanceDurationMatrixRequestModel();
            var coordinates = new List<CoordinateModel>
            {
                new CoordinateModel { Latitude = 5.0, Longitude = 6.0 },
                new CoordinateModel { Latitude = 7.0, Longitude = 8.0 }
            };
            
            request.DestinationCoordinates = coordinates;
            
            Assert.AreEqual(coordinates, request.DestinationCoordinates);
            Assert.AreEqual(2, request.DestinationCoordinates.Count);
        }

        [TestMethod]
        public void AdditionalValues_GetSet_WorksCorrectly()
        {
            var request = new DistanceDurationMatrixRequestModel();
            var additionalValues = new Dictionary<string, string>
            {
                { "mode", "driving" },
                { "language", "en-US" }
            };
            
            request.AdditionalValues = additionalValues;
            
            Assert.AreEqual(additionalValues, request.AdditionalValues);
            Assert.AreEqual(2, request.AdditionalValues.Count);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var request = new DistanceDurationMatrixRequestModel();
            
            Assert.IsNotNull(request.OriginalCoordinates);
            Assert.AreEqual(0, request.OriginalCoordinates.Count);
            Assert.IsNotNull(request.DestinationCoordinates);
            Assert.AreEqual(0, request.DestinationCoordinates.Count);
            Assert.IsNotNull(request.AdditionalValues);
            Assert.AreEqual(0, request.AdditionalValues.Count);
        }
    }
}