namespace Elect.Core.Constants
{
    public static class Formatting
    {
        /// <summary>
        ///     Isolate Datetime Format 
        /// </summary>
        public const string DateTimeOffSetFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Formatting = Newtonsoft.Json.Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateFormatString = DateTimeOffSetFormat,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
    }
}
