namespace Elect.Test.Core.StreamUtils
{
    [TestClass]
    public class StreamExtensionsUnitTest
    {
        [TestMethod]
        public void ToBytes_ReturnsCorrectBytes()
        {
            var expected = new byte[] { 1, 2, 3, 4, 5 };
            using (var ms = new MemoryStream(expected))
            {
                var result = ms.ToBytes();
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}
