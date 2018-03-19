#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> CustomDateTimeConverter.cs </Name>
//         <Created> 19/03/2018 9:39:36 PM </Created>
//         <Key> ff9539a4-d1ba-4737-88f4-1d37f4795d81 </Key>
//     </File>
//     <Summary>
//         CustomDateTimeConverter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json.Converters;

namespace Elect.Notification.OneSignal.JsonConverter
{
    /// <summary>
    ///     Custom DateTime converter used to format date and time in order to comply with API requirement. 
    /// </summary>
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        ///     Default constructor. 
        /// </summary>
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss \"GMT\"zzz";
        }
    }
}