using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elect.Location.Coordinate.PolygonUtils;
using Elect.Location.Models;
using System.Collections.Generic;

namespace Elect.Test.Location.Coordinate
{
    [TestClass]
    public class PolygonUtilsTests
    {
        [TestMethod]
        public void IsInPolygon_PointInsidePolygon_ReturnsTrue()
        {
            var polygon = new List<CoordinateModel>
            {
                new CoordinateModel(0, 0),
                new CoordinateModel(0, 10),
                new CoordinateModel(10, 10),
                new CoordinateModel(10, 0)
            };
            var point = new CoordinateModel(5, 5);
            Assert.IsTrue(PolygonUtils.IsInPolygon(point, polygon));
        }

        [TestMethod]
        public void IsInPolygon_PointOutsidePolygon_ReturnsFalse()
        {
            var polygon = new List<CoordinateModel>
            {
                new CoordinateModel(0, 0),
                new CoordinateModel(0, 10),
                new CoordinateModel(10, 10),
                new CoordinateModel(10, 0)
            };
            var point = new CoordinateModel(15, 5);
            Assert.IsFalse(PolygonUtils.IsInPolygon(point, polygon));
        }

        [TestMethod]
        public void IsInPolygon_PointOnVertex_ReturnsTrue()
        {
            var polygon = new List<CoordinateModel>
            {
                new CoordinateModel(0, 0),
                new CoordinateModel(0, 10),
                new CoordinateModel(10, 10),
                new CoordinateModel(10, 0)
            };
            var point = new CoordinateModel(0, 0);
            Assert.IsTrue(PolygonUtils.IsInPolygon(point, polygon));
        }

        [TestMethod]
        public void IsInPolygon_LessThanThreeVertices_ReturnsFalse()
        {
            var polygon = new List<CoordinateModel>
            {
                new CoordinateModel(0, 0),
                new CoordinateModel(0, 10)
            };
            var point = new CoordinateModel(0, 0);
            Assert.IsFalse(PolygonUtils.IsInPolygon(point, polygon));
        }
    }
}
