namespace Elect.Test.Core.CheckUtils
{
    [TestClass]
    public class CheckHelperUnitTest
    {
        [TestMethod]
        public void CheckNullOrWhiteSpace_ShouldNotThrow_WhenValidString()
        {
            CheckHelper.CheckNullOrWhiteSpace("valid", "TestProperty");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckNullOrWhiteSpace_ShouldThrow_WhenNull()
        {
            CheckHelper.CheckNullOrWhiteSpace(null, "TestProperty");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckNullOrWhiteSpace_ShouldThrow_WhenEmpty()
        {
            CheckHelper.CheckNullOrWhiteSpace("", "TestProperty");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CheckNullOrWhiteSpace_ShouldThrow_WhenWhitespace()
        {
            CheckHelper.CheckNullOrWhiteSpace("   ", "TestProperty");
        }
    }
}
