[TestClass]
public class UnitOfLengthModelUnitTest
{
    [TestMethod]
    public void Meter_StaticProperty_ShouldNotBeNull()
    {
        Assert.IsNotNull(UnitOfLengthModel.Meter);
    }

    [TestMethod]
    public void Kilometer_StaticProperty_ShouldNotBeNull()
    {
        Assert.IsNotNull(UnitOfLengthModel.Kilometer);
    }

    [TestMethod]
    public void NauticalMile_StaticProperty_ShouldNotBeNull()
    {
        Assert.IsNotNull(UnitOfLengthModel.NauticalMile);
    }

    [TestMethod]
    public void Mile_StaticProperty_ShouldNotBeNull()
    {
        Assert.IsNotNull(UnitOfLengthModel.Mile);
    }

    [TestMethod]
    public void Mile_ConvertFromMiles_ShouldReturnSameValue()
    {
        var input = 5.0;

        var result = UnitOfLengthModel.Mile.ConvertFromMiles(input);

        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void Mile_ConvertFromMiles_WithZero_ShouldReturnZero()
    {
        var result = UnitOfLengthModel.Mile.ConvertFromMiles(0.0);

        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void Mile_ConvertFromMiles_WithNegative_ShouldReturnNegative()
    {
        var input = -3.0;

        var result = UnitOfLengthModel.Mile.ConvertFromMiles(input);

        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void Meter_ConvertFromMiles_ShouldUseCorrectConversionFactor()
    {
        var input = 1.0;

        var result = UnitOfLengthModel.Meter.ConvertFromMiles(input);

        Assert.AreEqual(ElectLocationConstants.MileToMeter, result);
    }

    [TestMethod]
    public void Kilometer_ConvertFromMiles_ShouldUseCorrectConversionFactor()
    {
        var input = 1.0;

        var result = UnitOfLengthModel.Kilometer.ConvertFromMiles(input);

        Assert.AreEqual(ElectLocationConstants.MileToKilometer, result);
    }

    [TestMethod]
    public void NauticalMile_ConvertFromMiles_ShouldUseCorrectConversionFactor()
    {
        var input = 1.0;

        var result = UnitOfLengthModel.NauticalMile.ConvertFromMiles(input);

        Assert.AreEqual(ElectLocationConstants.NauticalMileToMile, result);
    }

    [TestMethod]
    public void ConvertFromMiles_WithMultipleValues_ShouldCalculateCorrectly()
    {
        var testCases = new[] { 0.0, 1.0, 2.5, 10.0, 100.0 };

        foreach (var testCase in testCases)
        {
            var meterResult = UnitOfLengthModel.Meter.ConvertFromMiles(testCase);
            var kilometerResult = UnitOfLengthModel.Kilometer.ConvertFromMiles(testCase);
            var nauticalResult = UnitOfLengthModel.NauticalMile.ConvertFromMiles(testCase);
            var mileResult = UnitOfLengthModel.Mile.ConvertFromMiles(testCase);

            Assert.AreEqual(testCase * ElectLocationConstants.MileToMeter, meterResult);
            Assert.AreEqual(testCase * ElectLocationConstants.MileToKilometer, kilometerResult);
            Assert.AreEqual(testCase * ElectLocationConstants.NauticalMileToMile, nauticalResult);
            Assert.AreEqual(testCase, mileResult);
        }
    }

    [TestMethod]
    public void StaticInstances_ShouldBeSameReference()
    {
        var meter1 = UnitOfLengthModel.Meter;
        var meter2 = UnitOfLengthModel.Meter;

        Assert.AreSame(meter1, meter2);
    }

    [TestMethod]
    public void ConvertFromMiles_WithDecimalValues_ShouldHandleCorrectly()
    {
        var input = 0.5;
        
        var meterResult = UnitOfLengthModel.Meter.ConvertFromMiles(input);
        var kilometerResult = UnitOfLengthModel.Kilometer.ConvertFromMiles(input);

        Assert.AreEqual(input * ElectLocationConstants.MileToMeter, meterResult);
        Assert.AreEqual(input * ElectLocationConstants.MileToKilometer, kilometerResult);
    }
}