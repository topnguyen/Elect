using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elect.Location.Coordinate.DistanceUtils;
using Elect.Location.Models;

namespace Elect.Test.Location.Coordinate.DistanceUtils
{
    [TestClass]
    public class CoordinateDistanceExtensionsTests
    {
        [TestMethod]
        public void DistanceTo_ReturnsCorrectDistance()
        {
            var origin = new CoordinateModel(10, 20);
            var dest = new CoordinateModel(11, 21);
            var result = origin.DistanceTo(dest, UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void FlatDistanceTo_ReturnsCorrectFlatDistance()
        {
            var origin = new CoordinateModel(10, 20);
            var dest = new CoordinateModel(11, 21);
            var result = origin.FlatDistanceTo(dest);
            Assert.AreEqual(2, result, 0.0001);
        }

        [TestMethod]
        public void HaversineDistanceTo_ReturnsCorrectDistance()
        {
            var origin = new CoordinateModel(10, 20);
            var dest = new CoordinateModel(11, 21);
            var result = origin.HaversineDistanceTo(dest, UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GeoDistanceTo_ReturnsCorrectDistance()
        {
            var origin = new CoordinateModel(10, 20);
            var dest = new CoordinateModel(11, 21);
            var result = origin.GeoDistanceTo(dest, UnitOfLengthModel.Kilometer);
            Assert.IsTrue(result > 0);
        }
    }
}
