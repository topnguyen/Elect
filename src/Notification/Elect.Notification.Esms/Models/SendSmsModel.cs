namespace Elect.Notification.Esms.Models
{
    public class SendSmsModel: ElectDisposableModel
    {
        public string Phone { get; set; }
        public string Content { get; set; }
        public int Type { get; set; } = 8;
        public int Sandbox { get; set; } = 0;
        /// <summary>
        ///     May need pre-register with eSMS.vn before use 
        /// </summary>
        public string BrandName { get; set; }
    }
}
