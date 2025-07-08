namespace Elect.Test.Core.EnvUtils
{
    [TestClass]
    public class EnvHelperUnitTest
    {
        [TestMethod]
        public void AspNetCoreEnvironmentVariable_IsCorrect()
        {
            Assert.AreEqual("ASPNETCORE_ENVIRONMENT", EnvHelper.AspNetCoreEnvironmentVariable);
        }
        [TestMethod]
        public void EnvNames_AreCorrect()
        {
            Assert.AreEqual("Development", EnvHelper.EnvDevelopmentName);
            Assert.AreEqual("Staging", EnvHelper.EnvStagingName);
            Assert.AreEqual("Production", EnvHelper.EnvProductionName);
        }
        [TestMethod]
        public void MachineName_IsNotNullOrEmpty()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(EnvHelper.MachineName));
        }
        [TestMethod]
        public void CurrentEnvironment_DefaultsToDevelopmentIfUnset()
        {
            var original = Environment.GetEnvironmentVariable(EnvHelper.AspNetCoreEnvironmentVariable);
            Environment.SetEnvironmentVariable(EnvHelper.AspNetCoreEnvironmentVariable, null);
            var field = typeof(EnvHelper).GetField("_currentEnvironment", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            if (field != null) field.SetValue(null, null);
            Assert.AreEqual("Development", EnvHelper.CurrentEnvironment);
            if (original != null)
                Environment.SetEnvironmentVariable(EnvHelper.AspNetCoreEnvironmentVariable, original);
        }
        [TestMethod]
        public void IsEnvironmentChecks_WorkCorrectly()
        {
            var original = Environment.GetEnvironmentVariable(EnvHelper.AspNetCoreEnvironmentVariable);
            Environment.SetEnvironmentVariable(EnvHelper.AspNetCoreEnvironmentVariable, "Staging");
            var field2 = typeof(EnvHelper).GetField("_currentEnvironment", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            if (field2 != null) field2.SetValue(null, null);
            Assert.IsTrue(EnvHelper.IsStaging());
            Assert.IsFalse(EnvHelper.IsDevelopment());
            Assert.IsFalse(EnvHelper.IsProduction());
            Assert.IsTrue(EnvHelper.Is("Staging"));
            if (original != null)
                Environment.SetEnvironmentVariable(EnvHelper.AspNetCoreEnvironmentVariable, original);
        }
    }
}
