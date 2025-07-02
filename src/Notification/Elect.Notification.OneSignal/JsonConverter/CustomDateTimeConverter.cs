namespace Elect.Notification.OneSignal.JsonConverter
{
    /// <summary>
    ///     Custom DateTime converter used to format date and time in order to comply with API requirement. 
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        ///     Default constructor. 
        /// </summary>
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss \"GMT\"zzz";
        }
    }
}
