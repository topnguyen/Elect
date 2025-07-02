namespace Elect.Core.DateTimeUtils
{
    public static class DateTimeExtensions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            return DateTimeHelper.GetTimestamp(dateTime);
        }
        public static long ToTimestamp(this DateTimeOffset dateTimeOffset)
        {
            return DateTimeHelper.GetTimestamp(dateTimeOffset);
        }
        public static DateTime TruncateTo(this DateTime dateTime, TruncateToType truncateTo)
        {
            return DateTimeHelper.TruncateTo(dateTime, truncateTo);
        }
        public static DateTimeOffset TruncateTo(this DateTimeOffset dateTimeOffset, TruncateToType truncateTo)
        {
            return DateTimeHelper.TruncateTo(dateTimeOffset, truncateTo);
        }
        /// <param name="dateTime">  </param>
        /// <param name="timeZoneId"> Time Zone ID, See more: https://msdn.microsoft.com/en-us/library/gg154758.aspx </param>
        public static DateTimeOffset WithTimeZone(this DateTime dateTime, string timeZoneId)
        {
            return DateTimeHelper.WithTimeZone(dateTime, timeZoneId);
        }
        public static DateTimeOffset WithTimeZone(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            return DateTimeHelper.WithTimeZone(dateTime, timeZoneInfo);
        }
        /// <summary>
        ///     Calculate different months between 2 dates following exactly number of days in month
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static double DiffMonth(this DateTime date1, DateTime date2)
        {
            double diffMonth = 0;
            // 1. Calculate total middle month
            var totalMidMonth = (date2.Year * 12 + date2.Month) - (date1.Year * 12 + date1.Month) - 1;
            totalMidMonth = totalMidMonth < 0 ? 0 : totalMidMonth;
            diffMonth += totalMidMonth;
            // 2. Find last day of the first month and last month
            var endOfFirstMonth = new DateTime(date1.Year, date1.Month, 1).AddMonths(1).AddDays(-1);
            var endOfLastMonth = new DateTime(date2.Year, date2.Month, 1).AddMonths(1).AddDays(-1);
            if (endOfFirstMonth == endOfLastMonth)
            {
                // Result
                return (double)(date2.Day - date1.Day + 1) / endOfLastMonth.Day;
            }
            // 3. Calculate total month for first month (is it full month or not)
            var totalFirstMonth = (endOfFirstMonth.Subtract(date1).TotalDays + 1) / endOfFirstMonth.Day;
            diffMonth += totalFirstMonth;
            // 4. Calculate total month for last month (is it full month or not)
            var totalLastMonth = (double)date2.Day / endOfLastMonth.Day;
            diffMonth += totalLastMonth;
            // Result
            return diffMonth;
        }
    }
}
