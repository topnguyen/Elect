#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SendSmsModel.cs </Name>
//         <Created> 17/03/2018 9:22:18 AM </Created>
//         <Key> 4290fbcb-74a8-455b-91b8-0f3abcac1200 </Key>
//     </File>
//     <Summary>
//         SendSmsModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.ObjUtils;

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