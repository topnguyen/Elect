namespace Elect.Test.Core.DateTimeUtils
{
    [TestClass]
    public class DateTimeExtensionsUnitTest
    {
        [TestMethod]
        public void ToTimestamp_DateTime_ReturnsCorrectValue()
        {
            var dt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expected = DateTimeHelper.GetTimestamp(dt);
            var actual = dt.ToTimestamp();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ToTimestamp_DateTimeOffset_ReturnsCorrectValue()
        {
            var dto = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var expected = DateTimeHelper.GetTimestamp(dto);
            var actual = dto.ToTimestamp();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TruncateTo_DateTime_ReturnsCorrectValue()
        {
            var dt = new DateTime(2020, 5, 15, 13, 45, 30);
            var truncated = dt.TruncateTo(TruncateToType.Day);
            Assert.AreEqual(new DateTime(2020, 5, 15), truncated);
        }
        [TestMethod]
        public void TruncateTo_DateTimeOffset_ReturnsCorrectValue()
        {
            var dto = new DateTimeOffset(2020, 5, 15, 13, 45, 30, TimeSpan.Zero);
            var truncated = dto.TruncateTo(TruncateToType.Day);
            Assert.AreEqual(new DateTimeOffset(2020, 5, 15, 0, 0, 0, TimeSpan.Zero), truncated);
        }
        [TestMethod]
        public void WithTimeZone_ById_ReturnsCorrectOffset()
        {
            var dt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var offset = dt.WithTimeZone("UTC");
            Assert.AreEqual(TimeSpan.Zero, offset.Offset);
        }
        [TestMethod]
        public void WithTimeZone_ByInfo_ReturnsCorrectOffset()
        {
            var dt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var info = TimeZoneInfo.Utc;
            var offset = dt.WithTimeZone(info);
            Assert.AreEqual(TimeSpan.Zero, offset.Offset);
        }
        [TestMethod]
        public void DiffMonth_SameMonth_ReturnsFraction()
        {
            var d1 = new DateTime(2020, 1, 1);
            var d2 = new DateTime(2020, 1, 16);
            var result = d1.DiffMonth(d2);
            Assert.IsTrue(result > 0 && result < 1);
        }
        [TestMethod]
        public void DiffMonth_DifferentMonths_ReturnsCorrectValue()
        {
            var d1 = new DateTime(2020, 1, 1);
            var d2 = new DateTime(2020, 3, 1);
            var result = d1.DiffMonth(d2);
            Assert.IsTrue(result > 1 && result < 3);
        }
    }
}
