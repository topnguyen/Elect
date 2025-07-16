[TestClass]
public class ClusterModelUnitTest
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        var cluster = new ClusterModel();

        Assert.IsNull(cluster.CenterCoordinate);
        Assert.IsNotNull(cluster.Coordinates);
        Assert.AreEqual(0, cluster.Coordinates.Count);
    }

    [TestMethod]
    public void CenterCoordinate_ShouldBeSettable()
    {
        var cluster = new ClusterModel();
        var centerCoordinate = new CoordinateModel { Latitude = 10.5, Longitude = 20.3 };

        cluster.CenterCoordinate = centerCoordinate;

        Assert.AreEqual(centerCoordinate, cluster.CenterCoordinate);
        Assert.AreEqual(10.5, cluster.CenterCoordinate.Latitude);
        Assert.AreEqual(20.3, cluster.CenterCoordinate.Longitude);
    }

    [TestMethod]
    public void Coordinates_ShouldBeInitializedAsEmptyList()
    {
        var cluster = new ClusterModel();

        Assert.IsNotNull(cluster.Coordinates);
        Assert.AreEqual(0, cluster.Coordinates.Count);
        Assert.IsInstanceOfType(cluster.Coordinates, typeof(List<CoordinateModel>));
    }

    [TestMethod]
    public void Coordinates_ShouldAllowAddingItems()
    {
        var cluster = new ClusterModel();
        var coordinate1 = new CoordinateModel { Latitude = 1.0, Longitude = 2.0 };
        var coordinate2 = new CoordinateModel { Latitude = 3.0, Longitude = 4.0 };

        cluster.Coordinates.Add(coordinate1);
        cluster.Coordinates.Add(coordinate2);

        Assert.AreEqual(2, cluster.Coordinates.Count);
        Assert.AreEqual(coordinate1, cluster.Coordinates[0]);
        Assert.AreEqual(coordinate2, cluster.Coordinates[1]);
    }

    [TestMethod]
    public void Coordinates_ShouldBeReplaceable()
    {
        var cluster = new ClusterModel();
        var newCoordinates = new List<CoordinateModel>
        {
            new CoordinateModel { Latitude = 5.0, Longitude = 6.0 },
            new CoordinateModel { Latitude = 7.0, Longitude = 8.0 }
        };

        cluster.Coordinates = newCoordinates;

        Assert.AreEqual(newCoordinates, cluster.Coordinates);
        Assert.AreEqual(2, cluster.Coordinates.Count);
        Assert.AreEqual(5.0, cluster.Coordinates[0].Latitude);
        Assert.AreEqual(8.0, cluster.Coordinates[1].Longitude);
    }

    [TestMethod]
    public void ClusterModel_ShouldAllowNullCoordinates()
    {
        var cluster = new ClusterModel { Coordinates = null };

        Assert.IsNull(cluster.Coordinates);
    }
}