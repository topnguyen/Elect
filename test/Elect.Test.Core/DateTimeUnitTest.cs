namespace Elect.Test.Core
{
    [TestClass]
    public class DateTimeUnitTest
    {
        [TestMethod]
        public void WithTimeZone()
        {
            DateTime dateTime = DateTime.UtcNow;
            DateTimeOffset dateTimeOffSet = DateTimeHelper.WithTimeZone(dateTime, "SE Asia Standard Time");
            Assert.AreEqual(dateTimeOffSet.Offset, new TimeSpan(7, 0, 0));
        }
        [TestMethod]
        public void DiffMonth()
        {
            var results = new StringBuilder();
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 01), new DateTime(2019, 01, 31)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 01), new DateTime(2019, 03, 31)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 10), new DateTime(2019, 01, 31)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 01), new DateTime(2019, 01, 05)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 05), new DateTime(2019, 01, 22)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 15), new DateTime(2019, 02, 28)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 01), new DateTime(2019, 02, 12)));
            results.AppendLine(DiffMonth(new DateTime(2019, 01, 02), new DateTime(2019, 05, 03)));
            results.AppendLine(DiffMonth(new DateTime(2019, 02, 27), new DateTime(2020, 04, 25)));
            var resultJson = results.ToString();
        }
        public string DiffMonth(DateTime date1, DateTime date2)
        {
            return $"{date1: dd/MM/yyyy} - {date2: dd/MM/yyyy} is {date1.DiffMonth(date2)}";
        }
    }
}
