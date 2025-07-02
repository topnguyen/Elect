namespace Elect.Notification.Esms.Interfaces
{
    public interface IElectEsmsClient
    {
        Task<SendSmsResponseModel> SendAsync([NotNull]SendSmsModel model);
        Task<BalanceModel> GetBalanceAsync();
    }
}
