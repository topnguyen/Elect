[TestClass]
public class CoordinateModelUnitTest
{
    [TestMethod]
    public void DefaultConstructor_ShouldInitializeWithDefaultValues()
    {
        var coordinate = new CoordinateModel();

        Assert.AreEqual(0.0, coordinate.Longitude);
        Assert.AreEqual(0.0, coordinate.Latitude);
        Assert.AreEqual(-1, coordinate.GroupNo);
        Assert.AreEqual(-1, coordinate.SequenceNo);
    }

    [TestMethod]
    public void ParameterizedConstructor_ShouldInitializeCorrectly()
    {
        var longitude = 123.456;
        var latitude = 78.901;

        var coordinate = new CoordinateModel(longitude, latitude);

        Assert.AreEqual(longitude, coordinate.Longitude);
        Assert.AreEqual(latitude, coordinate.Latitude);
        Assert.AreEqual(-1, coordinate.GroupNo);
        Assert.AreEqual(-1, coordinate.SequenceNo);
    }

    [TestMethod]
    public void Longitude_ShouldBeSettable()
    {
        var coordinate = new CoordinateModel();
        var longitude = 45.678;

        coordinate.Longitude = longitude;

        Assert.AreEqual(longitude, coordinate.Longitude);
    }

    [TestMethod]
    public void Latitude_ShouldBeSettable()
    {
        var coordinate = new CoordinateModel();
        var latitude = 12.345;

        coordinate.Latitude = latitude;

        Assert.AreEqual(latitude, coordinate.Latitude);
    }

    [TestMethod]
    public void GroupNo_ShouldBeSettable()
    {
        var coordinate = new CoordinateModel();
        var groupNo = 5;

        coordinate.GroupNo = groupNo;

        Assert.AreEqual(groupNo, coordinate.GroupNo);
    }

    [TestMethod]
    public void SequenceNo_ShouldBeSettable()
    {
        var coordinate = new CoordinateModel();
        var sequenceNo = 10;

        coordinate.SequenceNo = sequenceNo;

        Assert.AreEqual(sequenceNo, coordinate.SequenceNo);
    }

    [TestMethod]
    public void GroupNo_DefaultValue_ShouldBeNegativeOne()
    {
        var coordinate = new CoordinateModel();

        Assert.AreEqual(-1, coordinate.GroupNo);
    }

    [TestMethod]
    public void SequenceNo_DefaultValue_ShouldBeNegativeOne()
    {
        var coordinate = new CoordinateModel();

        Assert.AreEqual(-1, coordinate.SequenceNo);
    }

    [TestMethod]
    public void CoordinateModel_ShouldAllowNegativeCoordinates()
    {
        var coordinate = new CoordinateModel(-123.456, -78.901);

        Assert.AreEqual(-123.456, coordinate.Longitude);
        Assert.AreEqual(-78.901, coordinate.Latitude);
    }

    [TestMethod]
    public void CoordinateModel_ShouldAllowZeroCoordinates()
    {
        var coordinate = new CoordinateModel(0.0, 0.0);

        Assert.AreEqual(0.0, coordinate.Longitude);
        Assert.AreEqual(0.0, coordinate.Latitude);
    }

    [TestMethod]
    public void CoordinateModel_ShouldAllowLargeCoordinates()
    {
        var longitude = 180.0;
        var latitude = 90.0;
        var coordinate = new CoordinateModel(longitude, latitude);

        Assert.AreEqual(longitude, coordinate.Longitude);
        Assert.AreEqual(latitude, coordinate.Latitude);
    }
}