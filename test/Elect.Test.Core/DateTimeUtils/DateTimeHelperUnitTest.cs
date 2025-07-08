namespace Elect.Test.Core.DateTimeUtils
{
    [TestClass]
    public class DateTimeHelperUnitTest
    {
        [TestMethod]
        public void Epoch_IsCorrect()
        {
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), DateTimeHelper.Epoch);
        }
        [TestMethod]
        public void FromTimestamp_ReturnsCorrectDateTime()
        {
            var dt = DateTimeHelper.FromTimestamp(0);
            Assert.AreEqual(DateTimeHelper.Epoch, dt);
        }
        [TestMethod]
        public void GetTimestamp_DateTime_ReturnsCorrectValue()
        {
            var dt = new DateTime(1970, 1, 2, 0, 0, 0, DateTimeKind.Utc);
            var ts = DateTimeHelper.GetTimestamp(dt);
            Assert.AreEqual(86400, ts);
        }
        [TestMethod]
        public void GetTimestamp_DateTimeOffset_ReturnsCorrectValue()
        {
            var dto = new DateTimeOffset(1970, 1, 2, 0, 0, 0, TimeSpan.Zero);
            var ts = DateTimeHelper.GetTimestamp(dto);
            Assert.AreEqual(86400, ts);
        }
        [TestMethod]
        public void TruncateTo_DateTime_AllTypes()
        {
            var dt = new DateTime(2020, 5, 15, 13, 45, 30);
            Assert.AreEqual(new DateTime(2020, 1, 1), DateTimeHelper.TruncateTo(dt, TruncateToType.Year));
            Assert.AreEqual(new DateTime(2020, 5, 1), DateTimeHelper.TruncateTo(dt, TruncateToType.Month));
            Assert.AreEqual(new DateTime(2020, 5, 15), DateTimeHelper.TruncateTo(dt, TruncateToType.Day));
            Assert.AreEqual(new DateTime(2020, 5, 15, 13, 0, 0), DateTimeHelper.TruncateTo(dt, TruncateToType.Hour));
            Assert.AreEqual(new DateTime(2020, 5, 15, 13, 45, 0), DateTimeHelper.TruncateTo(dt, TruncateToType.Minute));
        }
        [TestMethod]
        public void TruncateTo_DateTimeOffset_AllTypes()
        {
            var dto = new DateTimeOffset(2020, 5, 15, 13, 45, 30, TimeSpan.Zero);
            Assert.AreEqual(new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero), DateTimeHelper.TruncateTo(dto, TruncateToType.Year));
            Assert.AreEqual(new DateTimeOffset(2020, 5, 1, 0, 0, 0, TimeSpan.Zero), DateTimeHelper.TruncateTo(dto, TruncateToType.Month));
            Assert.AreEqual(new DateTimeOffset(2020, 5, 15, 0, 0, 0, TimeSpan.Zero), DateTimeHelper.TruncateTo(dto, TruncateToType.Day));
            Assert.AreEqual(new DateTimeOffset(2020, 5, 15, 13, 0, 0, TimeSpan.Zero), DateTimeHelper.TruncateTo(dto, TruncateToType.Hour));
        }
    }
}
