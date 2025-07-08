namespace Elect.Test.Core.StreamUtils
{
    [TestClass]
    public class StreamHelperUnitTest
    {
        [TestMethod]
        public void ConvertToBytes_ReturnsCorrectBytes()
        {
            var expected = new byte[] { 10, 20, 30, 40 };
            using (var ms = new MemoryStream(expected))
            {
                var result = StreamHelper.ConvertToBytes(ms);
                CollectionAssert.AreEqual(expected, result);
            }
        }
        [TestMethod]
        public void ConvertToBytes_EmptyStream_ReturnsEmptyArray()
        {
            using (var ms = new MemoryStream())
            {
                var result = StreamHelper.ConvertToBytes(ms);
                Assert.AreEqual(0, result.Length);
            }
        }
    }
}
