namespace Elect.Logger.Models.Event
{
    public class RefererModel
    {
        /// <summary>
        ///     Referer root domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        ///     Referer full URL
        /// </summary>
        public string Url { get; set; }
    }
}