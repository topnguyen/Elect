#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> EsmsSmsType.cs </Name>
//         <Created> 17/03/2018 9:19:04 AM </Created>
//         <Key> 01d5d191-8480-4fe5-a7bc-52adef7c70c6 </Key>
//     </File>
//     <Summary>
//         EsmsSmsType.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel;

namespace Elect.Notification.Esms.Models
{
    public enum EsmsSmsType
    {
        /// <summary>
        ///     Display as Brand Name - 760-810 VND 
        /// </summary>
        [Description("Display as Brand Name - 760-810 VND ")]
        BrandName = 1,

        /// <summary>
        ///     Display as Verify - 530-570 VND 
        /// </summary>
        [Description("Display as Verify - 530-570 VND ")]
        Verify = 2,

        /// <summary>
        ///     Random phone - low speed - 750 VND 
        /// </summary>
        [Description("Random phone - low speed - 750 VND ")]
        Random = 3,

        /// <summary>
        ///     Display as Notify - 750 VND 
        /// </summary>
        [Description("Display as Notify - 750 VND ")]
        Notify = 4,

        /// <summary>
        ///     Display as 6788 - 750 VND 
        /// </summary>
        [Description("Display as 6788 - 750 VND ")]
        Number6788 = 6,

        /// <summary>
        ///     Display as Random - high speed - 300 VND 
        /// </summary>
        [Description("Display as Random - high speed - 300 VND ")]
        OTP = 7
    }
}