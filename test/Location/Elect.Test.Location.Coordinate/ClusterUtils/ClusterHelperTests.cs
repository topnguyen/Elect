using Microsoft.VisualStudio.TestTools.UnitTesting;
using Elect.Location.Coordinate.ClusterUtils;
using Elect.Location.Models;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Test.Location.Coordinate.ClusterUtils
{
    [TestClass]
    public class ClusterHelperTests
    {
        [TestMethod]
        public void GetCluster_ShouldReturnCorrectNumberOfClusters()
        {
            var coordinates = new[]
            {
                new CoordinateModel(0, 0),
                new CoordinateModel(10, 0),
                new CoordinateModel(0, 10),
                new CoordinateModel(10, 10)
            };
            // Reset GroupNo for each coordinate to ensure clean state
            foreach (var coord in coordinates)
            {
                coord.GroupNo = 0;
            }
            var helper = new ClusterHelper();
            var clusters = helper.GetCluster(2, coordinates);
            Assert.AreEqual(2, clusters.Count);
            Assert.AreEqual(4, clusters.Sum(c => c.Coordinates.Count));
        }

        [TestMethod]
        public void GetCluster_ThrowsIfTotalGroupInvalid()
        {
            var coordinates = new[]
            {
                new CoordinateModel(0, 0),
                new CoordinateModel(10, 0)
            };
            var helper = new ClusterHelper();
            Assert.ThrowsException<System.NotSupportedException>(() => helper.GetCluster(1, coordinates));
            Assert.ThrowsException<System.NotSupportedException>(() => helper.GetCluster(2, coordinates));
        }
    }
}
