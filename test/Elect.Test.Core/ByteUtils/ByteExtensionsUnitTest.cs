namespace Elect.Test.Core.ByteUtils
{
    [TestClass]
    public class ByteExtensionsUnitTest
    {
        [TestMethod]
        public void ToBase64_ShouldReturnCorrectBase64String()
        {
            byte[] bytes = { 1, 2, 3, 4, 5 };
            string expected = Convert.ToBase64String(bytes);
            string actual = bytes.ToBase64();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToBase64_ShouldReturnEmptyStringForEmptyArray()
        {
            byte[] bytes = new byte[0];
            string expected = Convert.ToBase64String(bytes);
            string actual = bytes.ToBase64();
            Assert.AreEqual(expected, actual);
        }
    }
}
