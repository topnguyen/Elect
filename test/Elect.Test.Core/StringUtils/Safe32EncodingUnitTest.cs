namespace Elect.Test.Core.StringUtils
{
    [TestClass]
    public class Safe32EncodingUnitTest
    {
        [TestMethod]
        public void EncodeDecodeBytes_RoundTrip()
        {
            var bytes = Encoding.UTF8.GetBytes("hello world");
            var encoded = Safe32Encoding.EncodeBytes(bytes);
            var decoded = Safe32Encoding.DecodeBytes(encoded);
            CollectionAssert.AreEqual(bytes, decoded);
        }
        [TestMethod]
        public void EncodeBytes_NullOrEmpty()
        {
            Assert.IsNull(Safe32Encoding.EncodeBytes(null));
            Assert.AreEqual(string.Empty, Safe32Encoding.EncodeBytes(new byte[0]));
        }
    }
}
