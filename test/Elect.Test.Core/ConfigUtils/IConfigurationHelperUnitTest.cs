namespace Elect.Test.Core.ConfigUtils
{
    [TestClass]
    public class IConfigurationHelperUnitTest
    {
        private IConfiguration GetConfig(Dictionary<string, string> dict)
        {
            return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
        }
        [TestMethod]
        public void GetSection_ReturnsBoundObject()
        {
            var dict = new Dictionary<string, string> { { "TestSection:Value", "456" } };
            var config = GetConfig(dict);
            var result = IConfigurationHelper.GetSection<TestSection>(config, "TestSection");
            Assert.AreEqual("456", result.Value);
        }
        [TestMethod]
        public void GetValueByEnv_ReturnsValue()
        {
            var dict = new Dictionary<string, string> { { "Key:TestMachine", "abc" }, { "Key:Development", "def" }, { "Key", "ghi" } };
            var config = GetConfig(dict);
            var original = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            typeof(Elect.Core.EnvUtils.EnvHelper).GetField("_currentEnvironment", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)?.SetValue(null, null);
            var result = IConfigurationHelper.GetValueByEnv<string>(config, "Key");
            Assert.AreEqual("def", result);
            if (original != null)
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", original);
        }
        [TestMethod]
        public void GetListValueByEnv_ReturnsList()
        {
            var dict = new Dictionary<string, string> { { "ListKey:0", "a" }, { "ListKey:1", "b" } };
            var config = GetConfig(dict);
            var result = IConfigurationHelper.GetListValueByEnv<string>(config, "ListKey");
            Assert.IsInstanceOfType(result, typeof(List<string>));
        }
        public class TestSection { public string Value { get; set; } }
    }
}
