[TestClass]
public class HangfireProviderUnitTest
{
    [TestMethod]
    public void HangfireProvider_ShouldHaveMemoryValue()
    {
        var provider = HangfireProvider.Memory;
        Assert.AreEqual(0, (int)provider);
    }

    [TestMethod]
    public void HangfireProvider_ShouldHaveSqlServerValue()
    {
        var provider = HangfireProvider.SqlServer;
        Assert.AreEqual(1, (int)provider);
    }

    [TestMethod]
    public void HangfireProvider_ShouldSupportAllValues()
    {
        var allValues = Enum.GetValues<HangfireProvider>();
        Assert.AreEqual(2, allValues.Length);
        Assert.IsTrue(allValues.Contains(HangfireProvider.Memory));
        Assert.IsTrue(allValues.Contains(HangfireProvider.SqlServer));
    }

    [TestMethod]
    public void HangfireProvider_ToString_ShouldReturnCorrectNames()
    {
        Assert.AreEqual("Memory", HangfireProvider.Memory.ToString());
        Assert.AreEqual("SqlServer", HangfireProvider.SqlServer.ToString());
    }

    [TestMethod]
    public void HangfireProvider_Parse_ShouldWorkCorrectly()
    {
        Assert.AreEqual(HangfireProvider.Memory, Enum.Parse<HangfireProvider>("Memory"));
        Assert.AreEqual(HangfireProvider.SqlServer, Enum.Parse<HangfireProvider>("SqlServer"));
    }
}