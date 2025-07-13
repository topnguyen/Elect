using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elect.Location.Coordinate.DistanceUtils;

namespace Elect.Test.Location.Coordinate.DistanceUtils
{
    [TestClass]
    public class DistanceHelperTests
    {
        [TestMethod]
        public void GetDistance_WithCoordinates_ReturnsDistance()
        {
            double lon1 = 10, lat1 = 20, lon2 = 11, lat2 = 21;
            var result = DistanceHelper.GetDistance(lon1, lat1, lon2, lat2);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void GetDistance_WithCoordinatesAndUnit_ReturnsDistance()
        {
            double lon1 = 10, lat1 = 20, lon2 = 11, lat2 = 21;
            var result = DistanceHelper.GetDistance(lon1, lat1, lon2, lat2, Elect.Location.Models.UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void GetDistance_WithCoordinateModels_ReturnsDistance()
        {
            var origin = new Elect.Location.Models.CoordinateModel(10, 20);
            var dest = new Elect.Location.Models.CoordinateModel(11, 21);
            var result = DistanceHelper.GetDistance(origin, dest, Elect.Location.Models.UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void GetDistanceByFlat_ReturnsFlatDistance()
        {
            var origin = new Elect.Location.Models.CoordinateModel(10, 20);
            var dest = new Elect.Location.Models.CoordinateModel(11, 21);
            var result = DistanceHelper.GetDistanceByFlat(origin, dest);
            Assert.AreEqual(2, result, 0.0001);
        }

        [TestMethod]
        public void GetDistanceByHaversine_ReturnsDistance()
        {
            var origin = new Elect.Location.Models.CoordinateModel(10, 20);
            var dest = new Elect.Location.Models.CoordinateModel(11, 21);
            var result = DistanceHelper.GetDistanceByHaversine(origin, dest, Elect.Location.Models.UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetDistanceByGeo_ReturnsDistance()
        {
            var origin = new Elect.Location.Models.CoordinateModel(10, 20);
            var dest = new Elect.Location.Models.CoordinateModel(11, 21);
            var result = DistanceHelper.GetDistanceByGeo(origin, dest, Elect.Location.Models.UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetDistance_SamePoint_ReturnsZero()
        {
            var origin = new Elect.Location.Models.CoordinateModel(10, 20);
            var result = DistanceHelper.GetDistance(origin, origin, Elect.Location.Models.UnitOfLengthModel.Kilometer);
            Assert.AreEqual(0, result, 0.0001);
        }
    }
}
