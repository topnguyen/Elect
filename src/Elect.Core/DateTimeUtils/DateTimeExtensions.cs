#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DateTimeExtensions.cs </Name>
//         <Created> 15/03/2018 7:06:54 PM </Created>
//         <Key> 2cb4f335-7dca-4415-876d-ca7d5fbfa7f7 </Key>
//     </File>
//     <Summary>
//         DateTimeExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

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
    }
}