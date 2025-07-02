namespace Elect.Location.Google.Models
{
    public class ElectLocationGoogleConstants
    {
        public const string DefaultGoogleMatrixApiEndpoint = "https://maps.googleapis.com/maps/api/distancematrix/json";
        public const string DefaultGoogleDirectionApiEndpoint = "https://maps.googleapis.com/maps/api/directions/xml";
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
