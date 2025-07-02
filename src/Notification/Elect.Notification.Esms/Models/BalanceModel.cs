namespace Elect.Notification.Esms.Models
{
    public class BalanceModel: ElectDisposableModel
    {
        /// <summary>
        ///     Balance in VND 
        /// </summary>
        public double Balance { get; set; }
        [JsonProperty("UserID")]
        public int UserId { get; set; }
        [JsonProperty("CodeResponse")]
        public EsmsResponseCode ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => ResponseCode == EsmsResponseCode.Success;
    }
}
