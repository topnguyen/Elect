[TestClass]
public class ElectLocationConstantsUnitTest
{
    [TestMethod]
    public void MileToKilometer_ShouldHaveCorrectValue()
    {
        Assert.AreEqual(1.609344, ElectLocationConstants.MileToKilometer);
    }

    [TestMethod]
    public void MileToMeter_ShouldHaveCorrectValue()
    {
        Assert.AreEqual(1609.344, ElectLocationConstants.MileToMeter);
    }

    [TestMethod]
    public void NauticalMileToMile_ShouldHaveCorrectValue()
    {
        Assert.AreEqual(0.8684, ElectLocationConstants.NauticalMileToMile);
    }

    [TestMethod]
    public void DegreesToRadians_ShouldHaveCorrectValue()
    {
        var expected = Math.PI / 180.0;
        Assert.AreEqual(expected, ElectLocationConstants.DegreesToRadians);
    }

    [TestMethod]
    public void RadiansToDegrees_ShouldHaveCorrectValue()
    {
        var expected = 180.0 / Math.PI;
        Assert.AreEqual(expected, ElectLocationConstants.RadiansToDegrees);
    }

    [TestMethod]
    public void EarthRadiusMile_ShouldHaveCorrectValue()
    {
        var expected = ElectLocationConstants.RadiansToDegrees * 60 * 1.1515;
        Assert.AreEqual(expected, ElectLocationConstants.EarthRadiusMile);
    }

    [TestMethod]
    public void EarthRadiusKilometer_ShouldHaveCorrectValue()
    {
        var expected = ElectLocationConstants.EarthRadiusMile * ElectLocationConstants.MileToKilometer;
        Assert.AreEqual(expected, ElectLocationConstants.EarthRadiusKilometer);
    }

    [TestMethod]
    public void MileToMeter_ShouldBeConsistentWithMileToKilometer()
    {
        var expectedMeter = ElectLocationConstants.MileToKilometer * 1000;
        Assert.AreEqual(expectedMeter, ElectLocationConstants.MileToMeter);
    }

    [TestMethod]
    public void DegreesToRadians_AndRadiansToDegrees_ShouldBeInverse()
    {
        var degrees = 90.0;
        var radians = degrees * ElectLocationConstants.DegreesToRadians;
        var backToDegrees = radians * ElectLocationConstants.RadiansToDegrees;
        
        Assert.AreEqual(degrees, backToDegrees, 1e-10);
    }

    [TestMethod]
    public void Constants_ShouldBePositive()
    {
        Assert.IsTrue(ElectLocationConstants.MileToKilometer > 0);
        Assert.IsTrue(ElectLocationConstants.MileToMeter > 0);
        Assert.IsTrue(ElectLocationConstants.NauticalMileToMile > 0);
        Assert.IsTrue(ElectLocationConstants.DegreesToRadians > 0);
        Assert.IsTrue(ElectLocationConstants.RadiansToDegrees > 0);
        Assert.IsTrue(ElectLocationConstants.EarthRadiusMile > 0);
        Assert.IsTrue(ElectLocationConstants.EarthRadiusKilometer > 0);
    }

    [TestMethod]
    public void EarthRadius_ShouldBeReasonable()
    {
        Assert.IsTrue(ElectLocationConstants.EarthRadiusMile > 3000);
        Assert.IsTrue(ElectLocationConstants.EarthRadiusMile < 5000);
        Assert.IsTrue(ElectLocationConstants.EarthRadiusKilometer > 5000);
        Assert.IsTrue(ElectLocationConstants.EarthRadiusKilometer < 8000);
    }

    [TestMethod]
    public void ConversionFactors_ShouldBeReasonable()
    {
        Assert.IsTrue(ElectLocationConstants.MileToKilometer > 1.5);
        Assert.IsTrue(ElectLocationConstants.MileToKilometer < 2.0);
        Assert.IsTrue(ElectLocationConstants.MileToMeter > 1500);
        Assert.IsTrue(ElectLocationConstants.MileToMeter < 2000);
        Assert.IsTrue(ElectLocationConstants.NauticalMileToMile > 0.8);
        Assert.IsTrue(ElectLocationConstants.NauticalMileToMile < 1.0);
    }
}