namespace Elect.Notification.OneSignal.Models
{
    public class ElectOneSignalConstants
    {
        public List<string> IncludedAllSegments = new List<string>
        {
            "All"
        };
        public const string DefaultApiUrl = "https://onesignal.com/api/v1";
        internal static readonly NewtonsoftJsonSerializer NewtonsoftJsonSerializer =
            new NewtonsoftJsonSerializer(
                new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include
                }
            );
    }
}
