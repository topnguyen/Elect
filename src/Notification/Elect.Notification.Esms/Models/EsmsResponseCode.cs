namespace Elect.Notification.Esms.Models
{
    public enum EsmsResponseCode
    {
        Success = 100,
        [Description("Unknown Error")]
        UnknownError = 99,
        [Description("Wrong API Key or API Secret")]
        UnAuthentication = 101,
        [Description("Account is banned")]
        AccountBanned = 102,
        [Description("Balance not enough to send SMS")]
        NotEnoughBalance = 103,
        [Description("Brand name code is wrong")]
        WrongBrandNameCode = 104,
        [Description("Invalid SMS Type")]
        InvalidSmsType = 118,
        [Description("Minimum phone quantity require for Brand Name advertising SMS")]
        MinimumPhoneQuantityRequire = 119,
        [Description("Minimum content length require for Brand Name advertising SMS")]
        MinimumContentLengthRequire = 131,
        [Description("Don't have permission send SMS to 8755")]
        UnAuthorizePhoneNumber = 132,
    }
}
