namespace Elect.Test.Core.DateTimeUtils
{
    [TestClass]
    public class TruncateToTypeUnitTest
    {
        [TestMethod]
        public void TruncateToType_Enum_ValuesAreCorrect()
        {
            Assert.AreEqual(0, (int)TruncateToType.Year);
            Assert.AreEqual(1, (int)TruncateToType.Month);
            Assert.AreEqual(2, (int)TruncateToType.Day);
            Assert.AreEqual(3, (int)TruncateToType.Hour);
            Assert.AreEqual(4, (int)TruncateToType.Minute);
            Assert.AreEqual(5, (int)TruncateToType.Second);
        }
    }
}
