﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DeviceTypeEnum.cs </Name>
//         <Created> 19/03/2018 9:16:14 PM </Created>
//         <Key> 1e30b0dc-0756-4856-95e3-a8b1766e3655 </Key>
//     </File>
//     <Summary>
//         DeviceTypeEnum.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Notification.OneSignal.Models.Device
{
    /// <summary>
    ///     Device types enum. 
    /// </summary>
    public enum DeviceTypeEnum
    {
        /// <summary>
        ///     Apple operating system compatible devices such as: phones, tables, TV-s, watches and
        ///     other devices.
        /// </summary>
        iOS = 0,

        /// <summary>
        ///     Android operating system compatible devices such as: phones, tables, TV-s, watches
        ///     and other devices.
        /// </summary>
        Android = 1,

        /// <summary>
        ///     Amazon's android operating system compatible devices such as: phones, tables, TV-s,
        ///     watches and other devices.
        /// </summary>
        Amazon = 2,

        /// <summary>
        ///     Windows RT operating system compatible devices such as: phones, tables, TV-s, watches
        ///     and other devices.
        /// </summary>
        WindowsPhoneMPNS = 3,

        /// <summary>
        ///     Used for Chrome app. 
        /// </summary>
        ChromeApp = 4,

        /// <summary>
        ///     Used for Chrome web site. 
        /// </summary>
        ChromeWebsite = 5,

        /// <summary>
        ///     Windows 8.0 operating system generation used on phones. 
        /// </summary>
        WindowsPhoneWNS = 6,

        /// <summary>
        ///     Used for Safari. 
        /// </summary>
        Safari = 7,

        /// <summary>
        ///     Used for Firefox. 
        /// </summary>
        Firefox = 8,

        /// <summary>
        ///     Mac OS computers. 
        /// </summary>
        MacOSX = 9
    }
}