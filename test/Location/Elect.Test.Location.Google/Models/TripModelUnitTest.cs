namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class TripModelUnitTest
    {
        [TestMethod]
        public void CoordinateSequences_GetSet_WorksCorrectly()
        {
            var trip = new TripModel();
            var coordinates = new List<CoordinateModel>
            {
                new CoordinateModel { Latitude = 1.0, Longitude = 2.0 },
                new CoordinateModel { Latitude = 3.0, Longitude = 4.0 }
            };
            
            trip.CoordinateSequences = coordinates;
            
            Assert.AreEqual(coordinates, trip.CoordinateSequences);
            Assert.AreEqual(2, trip.CoordinateSequences.Count);
        }

        [TestMethod]
        public void TotalDistanceInMeter_GetSet_WorksCorrectly()
        {
            var trip = new TripModel();
            var distance = 1500.75;
            
            trip.TotalDistanceInMeter = distance;
            
            Assert.AreEqual(distance, trip.TotalDistanceInMeter);
        }

        [TestMethod]
        public void TotalDurationInSecond_GetSet_WorksCorrectly()
        {
            var trip = new TripModel();
            var duration = 3600.5;
            
            trip.TotalDurationInSecond = duration;
            
            Assert.AreEqual(duration, trip.TotalDurationInSecond);
        }

        [TestMethod]
        public void DistanceDurationMatrix_GetSet_WorksCorrectly()
        {
            var trip = new TripModel();
            var matrix = new DistanceDurationMatrixResultModel();
            
            trip.DistanceDurationMatrix = matrix;
            
            Assert.AreEqual(matrix, trip.DistanceDurationMatrix);
        }

        [TestMethod]
        public void DefaultValues_AreCorrect()
        {
            var trip = new TripModel();
            
            Assert.IsNull(trip.CoordinateSequences);
            Assert.AreEqual(0.0, trip.TotalDistanceInMeter);
            Assert.AreEqual(0.0, trip.TotalDurationInSecond);
            Assert.IsNull(trip.DistanceDurationMatrix);
        }
    }
}