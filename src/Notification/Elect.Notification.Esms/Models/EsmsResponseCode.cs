#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EsmsResponseCode.cs </Name>
//         <Created> 17/03/2018 9:16:01 AM </Created>
//         <Key> 1676101e-2499-4be2-8ce0-6f1fe2a2b85d </Key>
//     </File>
//     <Summary>
//         EsmsResponseCode.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel;

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