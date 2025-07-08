namespace Elect.Test.Core.StringUtils
{
    [TestClass]
    public class Safe64EncodingUnitTest
    {
        [TestMethod]
        public void EncodeDecodeBytes_RoundTrip()
        {
            var bytes = Encoding.UTF8.GetBytes("hello world");
            var encoded = Safe64Encoding.EncodeBytes(bytes);
            var decoded = Safe64Encoding.DecodeBytes(encoded);
            CollectionAssert.AreEqual(bytes, decoded);
        }
        [TestMethod]
        public void EncodeBytes_NullOrEmpty()
        {
            Assert.AreEqual(string.Empty, Safe64Encoding.EncodeBytes(null));
            Assert.AreEqual(string.Empty, Safe64Encoding.EncodeBytes(new byte[0]));
        }
    }
}
