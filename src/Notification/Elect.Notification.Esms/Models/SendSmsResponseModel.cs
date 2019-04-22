#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SendSmsResponseModel.cs </Name>
//         <Created> 17/03/2018 9:25:20 AM </Created>
//         <Key> 07908947-13a0-4ea5-9b66-05617426bb19 </Key>
//     </File>
//     <Summary>
//         SendSmsResponseModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;
using Newtonsoft.Json;

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