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
            Assert.AreEqual(new DateTimeOffset(2020, 5, 15, 13, 45, 0, TimeSpan.Zero), DateTimeHelper.TruncateTo(dto, TruncateToType.Minute));
        }

        [TestMethod]
        public void TruncateTo_DateTime_DefaultCase_TruncatesToSecond()
        {
            var dt = new DateTime(2020, 5, 15, 13, 45, 30, 500);
            var result = DateTimeHelper.TruncateTo(dt, (TruncateToType)999); // Invalid enum value to test default case
            var expected = new DateTime(2020, 5, 15, 13, 45, 30);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TruncateTo_DateTimeOffset_DefaultCase_TruncatesToSecond()
        {
            var dto = new DateTimeOffset(2020, 5, 15, 13, 45, 30, 500, TimeSpan.Zero);
            var result = DateTimeHelper.TruncateTo(dto, (TruncateToType)999); // Invalid enum value to test default case
            var expected = new DateTimeOffset(2020, 5, 15, 13, 45, 30, 0, TimeSpan.Zero);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithTimeZone_WithTimeZoneId_ReturnsCorrectDateTimeOffset()
        {
            var dt = new DateTime(2020, 5, 15, 13, 45, 30);
            var result = DateTimeHelper.WithTimeZone(dt, "UTC");
            
            Assert.AreEqual(dt.Year, result.Year);
            Assert.AreEqual(dt.Month, result.Month);
            Assert.AreEqual(dt.Day, result.Day);
            Assert.AreEqual(dt.Hour, result.Hour);
            Assert.AreEqual(dt.Minute, result.Minute);
            Assert.AreEqual(dt.Second, result.Second);
        }

        [TestMethod]
        public void WithTimeZone_WithTimeZoneInfo_ReturnsCorrectDateTimeOffset()
        {
            var dt = new DateTime(2020, 5, 15, 13, 45, 30, 123);
            var timeZoneInfo = TimeZoneInfo.Utc;
            var result = DateTimeHelper.WithTimeZone(dt, timeZoneInfo);
            
            Assert.AreEqual(dt.Year, result.Year);
            Assert.AreEqual(dt.Month, result.Month);
            Assert.AreEqual(dt.Day, result.Day);
            Assert.AreEqual(dt.Hour, result.Hour);
            Assert.AreEqual(dt.Minute, result.Minute);
            Assert.AreEqual(dt.Second, result.Second);
            Assert.AreEqual(dt.Millisecond, result.Millisecond);
            Assert.AreEqual(timeZoneInfo.BaseUtcOffset, result.Offset);
        }

        [TestMethod]
        public void GetTimeZoneInfo_WithValidTimeZoneId_ReturnsTimeZoneInfo()
        {
            var result = DateTimeHelper.GetTimeZoneInfo("UTC");
            Assert.IsNotNull(result);
            Assert.AreEqual(TimeSpan.Zero, result.BaseUtcOffset);
        }

        [TestMethod]
        public void TryGetTimeZoneInfo_WithValidTimeZoneId_ReturnsTrue()
        {
            var success = DateTimeHelper.TryGetTimeZoneInfo("UTC", out var timeZoneInfo);
            Assert.IsTrue(success);
            Assert.IsNotNull(timeZoneInfo);
            Assert.AreEqual(TimeSpan.Zero, timeZoneInfo.BaseUtcOffset);
        }

        [TestMethod]
        public void TryGetTimeZoneInfo_WithInvalidTimeZoneId_ReturnsFalse()
        {
            var success = DateTimeHelper.TryGetTimeZoneInfo("InvalidTimeZone", out var timeZoneInfo);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void GetEndOfTheMonth_DateTime_ReturnsLastDayOfMonth()
        {
            var date = new DateTime(2020, 2, 15); // February 2020 (leap year)
            var result = DateTimeHelper.GetEndOfTheMonth(date);
            var expected = new DateTime(2020, 2, 29); // Last day of February 2020
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetEndOfTheMonth_DateTimeOffset_ReturnsLastDayOfMonth()
        {
            var date = new DateTimeOffset(2020, 2, 15, 10, 30, 0, TimeSpan.FromHours(5));
            var result = DateTimeHelper.GetEndOfTheMonth(date);
            var expected = new DateTimeOffset(2020, 2, 29, 0, 0, 0, TimeSpan.FromHours(5));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetEndOfTheMonth_January_Returns31st()
        {
            var date = new DateTime(2020, 1, 15);
            var result = DateTimeHelper.GetEndOfTheMonth(date);
            var expected = new DateTime(2020, 1, 31);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetEndOfTheMonth_December_Returns31st()
        {
            var date = new DateTime(2020, 12, 1);
            var result = DateTimeHelper.GetEndOfTheMonth(date);
            var expected = new DateTime(2020, 12, 31);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetEndOfTheMonth_NonLeapYearFebruary_Returns28th()
        {
            var date = new DateTime(2021, 2, 15); // February 2021 (non-leap year)
            var result = DateTimeHelper.GetEndOfTheMonth(date);
            var expected = new DateTime(2021, 2, 28);
            Assert.AreEqual(expected, result);
        }
    }
}
