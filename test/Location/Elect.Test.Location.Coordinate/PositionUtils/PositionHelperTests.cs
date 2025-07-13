using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elect.Location.Coordinate.PositionUtils;
using System;
using Elect.Location.Models;

namespace Elect.Test.Location.Coordinate.PositionUtils
{
    [TestClass]
    public class PositionHelperTests
    {
        [TestMethod]
        public void GetTopLeftOfSquare_ShouldReturnCoordinate()
        {
            var origin = new CoordinateModel(10, 20);
            double radius = 5;
            var result = PositionHelper.GetTopLeftOfSquare(origin, radius);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetBotRightOfSquare_ShouldReturnCoordinate()
        {
            var origin = new CoordinateModel(10, 20);
            double radius = 5;
            var result = PositionHelper.GetBotRightOfSquare(origin, radius);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDerivedPosition_ShouldReturnCoordinate()
        {
            var origin = new CoordinateModel(10, 20);
            double radius = 5;
            double bearing = 90;
            var result = PositionHelper.GetDerivedPosition(origin, radius, bearing);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTopLeftOfSquare_Correctness()
        {
            var origin = new CoordinateModel(0, 0);
            double radius = 10;
            var result = PositionHelper.GetTopLeftOfSquare(origin, radius);
            Assert.IsNotNull(result);
            // Should not be the same as origin
            Assert.IsFalse(result.Latitude == 0 && result.Longitude == 0);
        }

        [TestMethod]
        public void GetBotRightOfSquare_Correctness()
        {
            var origin = new CoordinateModel(0, 0);
            double radius = 10;
            var result = PositionHelper.GetBotRightOfSquare(origin, radius);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Latitude == 0 && result.Longitude == 0);
        }

        [TestMethod]
        public void GetDerivedPosition_ZeroRadius_ReturnsOrigin()
        {
            var origin = new CoordinateModel(10, 20);
            double radius = 0;
            double bearing = 90;
            var result = PositionHelper.GetDerivedPosition(origin, radius, bearing);
            Assert.AreEqual(origin.Latitude, result.Latitude, 0.0001);
            Assert.AreEqual(origin.Longitude, result.Longitude, 0.0001);
        }

        [TestMethod]
        public void GetHypotenuseLength_CorrectCalculation()
        {
            double length = 5;
            var hypotenuse = PositionHelper.GetHypotenuseLength(length);
            Assert.AreEqual(Math.Sqrt(50), hypotenuse, 0.0001);
        }
    }
}
