namespace Elect.Test.Core.ByteUtils
{
    [TestClass]
    public class ByteHelperUnitTest
    {
        [TestMethod]
        public void ConvertToToBase64_ShouldReturnCorrectBase64String()
        {
            byte[] bytes = { 10, 20, 30, 40, 50 };
            string expected = Convert.ToBase64String(bytes);
            string actual = ByteHelper.ConvertToToBase64(bytes);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ConvertToToBase64_ShouldReturnEmptyStringForEmptyArray()
        {
            byte[] bytes = new byte[0];
            string expected = Convert.ToBase64String(bytes);
            string actual = ByteHelper.ConvertToToBase64(bytes);
            Assert.AreEqual(expected, actual);
        }
    }
}
