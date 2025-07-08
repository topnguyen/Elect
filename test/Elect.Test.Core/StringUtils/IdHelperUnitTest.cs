namespace Elect.Test.Core.StringUtils
{
    [TestClass]
    public class IdHelperUnitTest
    {
        [TestMethod]
        public void ToShortString_And_FromShortString_RoundTrip()
        {
            uint id = 1234567890;
            var shortStr = IdHelper.ToShortString(id);
            var result = IdHelper.FromShortString(shortStr);
            Assert.AreEqual(id, result);
        }
        [TestMethod]
        public void TryFromShortString_ValidAndInvalid()
        {
            uint id = 42;
            var shortStr = IdHelper.ToShortString(id);
            Assert.IsTrue(IdHelper.TryFromShortString(shortStr, out var result));
            Assert.AreEqual(id, result);
            // Test with a string that is not a valid Safe32Encoding string of the correct length and not decodable
            string invalid = "!@#INVALID!@#";
            Assert.IsFalse(IdHelper.TryFromShortString(invalid, out _));
        }
    }
}
