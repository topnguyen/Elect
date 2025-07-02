namespace Elect.Notification.Esms.Models
{
    public class ElectEsmsConstants
    {
        public const string DefaultApiUrl = "https://restapi.esms.vn";
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
