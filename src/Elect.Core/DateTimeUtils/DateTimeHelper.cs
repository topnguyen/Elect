#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DateTimeHelper.cs </Name>
//         <Created> 15/03/2018 5:22:38 PM </Created>
//         <Key> 70e34273-714b-4a8a-84b6-2ad3289cdf71 </Key>
//     </File>
//     <Summary>
//         DateTimeHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Core.DateTimeUtils
{
    public class DateTimeHelper
    {
        public static DateTime Epoch { get; } = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromTimestamp(long value) => Epoch.AddSeconds(value);

        public static long ToTimestamp(DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;

            return (long)elapsedTime.TotalSeconds;
        }

        public static DateTime TruncateTo(DateTime dateTime, TruncateToType truncateTo)
        {
            switch (truncateTo)
            {
                case TruncateToType.Year:
                    return new DateTime(dateTime.Year, 0, 0, 0, 0, 0);

                case TruncateToType.Month:
                    return new DateTime(dateTime.Year, dateTime.Month, 0, 0, 0, 0);

                case TruncateToType.Day:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

                case TruncateToType.Hour:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);

                case TruncateToType.Minute:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);

                default:
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0);
            }
        }

        /// <param name="dateTime">  </param>
        /// <param name="timeZoneId"> Time Zone ID, See more: https://msdn.microsoft.com/en-us/library/gg154758.aspx </param>
        public static DateTimeOffset WithTimeZone(DateTime dateTime, string timeZoneId)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            return WithTimeZone(dateTime, timeZoneInfo);
        }

        public static DateTimeOffset WithTimeZone(DateTime dateTime, TimeZoneInfo timeZoneInfo)
        {
            var dateTimeWithTimeZone = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, timeZoneInfo.BaseUtcOffset);

            return dateTimeWithTimeZone;
        }
    }
}