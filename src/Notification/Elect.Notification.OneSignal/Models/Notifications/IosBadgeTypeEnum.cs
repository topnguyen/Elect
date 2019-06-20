#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IosBadgeTypeEnum.cs </Name>
//         <Created> 19/03/2018 9:41:15 PM </Created>
//         <Key> 9330aa9f-5603-44c4-ae32-4f4b0621869c </Key>
//     </File>
//     <Summary>
//         IosBadgeTypeEnum.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Notification.OneSignal.Models.Notifications
{
    /// <summary>
    ///     Describes whether to set or increase/decrease your app's iOS badge count by the
    ///     ios_badgeCount specified count. Can specify None, SetTo, or Increase.
    /// </summary>
    public enum IosBadgeTypeEnum
    {
        /// <summary>
        ///     Leaves the count unaffected. 
        /// </summary>
        None,

        /// <summary>
        ///     Directly sets the badge count to the number specified in ios_badgeCount. 
        /// </summary>
        SetTo,

        /// <summary>
        ///     Adds the number specified in ios_badgeCount to the total. Use a negative number to
        ///     decrease the badge count.
        /// </summary>
        Increase
    }
}