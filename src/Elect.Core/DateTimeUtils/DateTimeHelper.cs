namespace Elect.Core.DateTimeUtils
{
    public class DateTimeHelper
    {
        public static DateTime Epoch { get; } = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static DateTime FromTimestamp(long value) => Epoch.AddSeconds(value);
        public static long GetTimestamp(DateTime dateTime)
        {
            TimeSpan elapsedTime = dateTime - Epoch;
            return (long) elapsedTime.TotalSeconds;
        }
        public static long GetTimestamp(DateTimeOffset dateTimeOffset)
        {
            TimeSpan elapsedTime = dateTimeOffset - Epoch;
            return (long) elapsedTime.TotalSeconds;
        }
        public static DateTime TruncateTo(DateTime dateTime, TruncateToType truncateTo)
        {
            switch (truncateTo)
            {
                case TruncateToType.Year:
                    return new DateTime(dateTime.Year, 1, 1);
                case TruncateToType.Month:
                    return new DateTime(dateTime.Year, dateTime.Month, 1);
                case TruncateToType.Day:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
                case TruncateToType.Hour:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);
                case TruncateToType.Minute:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
                default:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                        dateTime.Second, 0);
            }
        }
        public static DateTimeOffset TruncateTo(DateTimeOffset dateTimeOffset, TruncateToType truncateTo)
        {
            switch (truncateTo)
            {
                case TruncateToType.Year:
                    return new DateTimeOffset(dateTimeOffset.Year, 1, 1, 0, 0, 0, dateTimeOffset.Offset);
                case TruncateToType.Month:
                    return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0, 0, 0,
                        dateTimeOffset.Offset);
                case TruncateToType.Day:
                    return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 0, 0, 0,
                        dateTimeOffset.Offset);
                case TruncateToType.Hour:
                    return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day,
                        dateTimeOffset.Hour, 0, 0, dateTimeOffset.Offset);
                case TruncateToType.Minute:
                    return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day,
                        dateTimeOffset.Hour, dateTimeOffset.Minute, 0, dateTimeOffset.Offset);
                default:
                    return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day,
                        dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, 0, dateTimeOffset.Offset);
            }
        }
        /// <param name="dateTime">  </param>
        /// <param name="timeZoneId"> Time Zone ID, See more: https://msdn.microsoft.com/en-us/library/gg154758.aspx </param>
        public static DateTimeOffset WithTimeZone(DateTime dateTime, string timeZoneId)
        {
            var timeZoneInfo = GetTimeZoneInfo(timeZoneId);
            return WithTimeZone(dateTime, timeZoneInfo);
        }
        public static DateTimeOffset WithTimeZone(DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            var dateTimeWithTimeZone = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
                dateTime.Minute, dateTime.Second, dateTime.Millisecond, timeZoneInfo.BaseUtcOffset);
            return dateTimeWithTimeZone;
        }
        /// <summary>
        ///     Support find time zone id by difference platform: Windows, Mac, Linux. 
        /// </summary>
        /// <param name="timeZoneId"></param>
        /// <returns></returns>
        public static TimeZoneInfo GetTimeZoneInfo(string timeZoneId)
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(timeZoneId);
            return timeZoneInfo;
        }
        /// <summary>
        ///     Support find time zone id by difference platform: Windows, Mac, Linux. 
        /// </summary>
        /// <param name="timeZoneId">  </param>
        /// <param name="timeZoneInfo"></param>
        /// <returns></returns>
        public static bool TryGetTimeZoneInfo(string timeZoneId, out TimeZoneInfo timeZoneInfo)
        {
            return TZConvert.TryGetTimeZoneInfo(timeZoneId, out timeZoneInfo);
        }
        public static DateTime GetEndOfTheMonth(DateTime date)
        {
            var endOfTheMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            return endOfTheMonth;
        }
        public static DateTimeOffset GetEndOfTheMonth(DateTimeOffset date)
        {
            var endOfTheMonth = new DateTimeOffset(GetEndOfTheMonth(date.DateTime), date.Offset);
            return endOfTheMonth;
        }
    }
}
