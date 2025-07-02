namespace Elect.Notification.Esms.Models
{
    public class SendSmsResponseModel : ElectDisposableModel
    {
        [JsonProperty("SMSID")]
        public string Id { get; set; }
        [JsonProperty("CodeResult")]
        public EsmsResponseCode ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => ResponseCode == EsmsResponseCode.Success;
    }
}
