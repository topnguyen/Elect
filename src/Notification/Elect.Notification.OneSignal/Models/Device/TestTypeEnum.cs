#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TestTypeEnum.cs </Name>
//         <Created> 19/03/2018 9:15:33 PM </Created>
//         <Key> 2aedafff-6642-4229-a584-fca1247763f6 </Key>
//     </File>
//     <Summary>
//         TestTypeEnum.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Notification.OneSignal.Models.Device
{
    /// <summary>
    ///     Test type enumeration. 
    /// </summary>
    public enum TestTypeEnum
    {
        /// <summary>
        ///     Used during development phase. 
        /// </summary>
        Development = 1,

        /// <summary>
        ///     Used in production, when trying to track down undelivered messages for example. 
        /// </summary>
        AdHoc = 2
    }
}