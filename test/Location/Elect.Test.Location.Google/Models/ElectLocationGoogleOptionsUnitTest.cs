namespace Elect.Test.Location.Google.Models
{
    [TestClass]
    public class ElectLocationGoogleOptionsUnitTest
    {
        [TestMethod]
        public void GoogleApiKey_GetSet_WorksCorrectly()
        {
            var options = new ElectLocationGoogleOptions();
            var testApiKey = "test-api-key-123";
            
            options.GoogleApiKey = testApiKey;
            
            Assert.AreEqual(testApiKey, options.GoogleApiKey);
        }

        [TestMethod]
        public void GoogleApiKey_DefaultValue_IsNull()
        {
            var options = new ElectLocationGoogleOptions();
            
            Assert.IsNull(options.GoogleApiKey);
        }

        [TestMethod]
        public void GoogleApiKey_CanBeEmpty()
        {
            var options = new ElectLocationGoogleOptions();
            
            options.GoogleApiKey = string.Empty;
            
            Assert.AreEqual(string.Empty, options.GoogleApiKey);
        }

        [TestMethod]
        public void GoogleApiKey_CanBeWhitespace()
        {
            var options = new ElectLocationGoogleOptions();
            var whitespaceKey = "   ";
            
            options.GoogleApiKey = whitespaceKey;
            
            Assert.AreEqual(whitespaceKey, options.GoogleApiKey);
        }
    }
}