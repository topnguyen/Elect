#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectDataTableOptions.cs </Name>
//         <Created> 23/03/2018 4:48:18 PM </Created>
//         <Key> 56038f99-7208-4341-b252-7ed1dc55c6e7 </Key>
//     </File>
//     <Summary>
//         ElectDataTableOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Core.Interfaces;
using Elect.Web.DataTable.Models.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Elect.Web.DataTable.Models.Options
{
    public class ElectDataTableOptions : IElectOptions
    {
        public static ElectDataTableOptions Instance { get; set; }

        /// <summary>
        ///     Config use datetime with TimeZone. Default is "UTC", See more: https://msdn.microsoft.com/en-us/library/gg154758.aspx 
        /// </summary>
        public string DateTimeTimeZone { get; set; } = "UTC";

        /// <summary>
        ///     All response will apply the format by default. If
        ///     <see cref="RequestDateTimeFormatType" /> is
        ///     <see cref="DateTimeFormatType.Specific" />, every request will use the format to
        ///     parse string to DateTime. Else will try parse string to DateTime by any format.
        ///     <para> Format "dd/MM/yyyy" by default </para>
        /// </summary>
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        /// <summary>
        ///     All response will apply the format by default. If
        ///     <see cref="RequestDateTimeFormatType" /> is
        ///     <see cref="DateTimeFormatType.Specific" />, every request will use the format to
        ///     parse string to DateTime. Else will try parse string to DateTime by any format.
        ///     <para> Format "dd/MM/yyyy hh:mm tt" by default </para>
        /// </summary>
        public string DateTimeFormat { get; set; } = "dd/MM/yyyy hh:mm:ss tt";

        private ElectDataTableDefaultDisplayTextModel _defaultDisplayText = new ElectDataTableDefaultDisplayTextModel();

        /// <summary>
        ///     Set default display text for common text in DataTable.
        /// </summary>
        /// <remarks>Set null will use as default value.</remarks>
        public ElectDataTableDefaultDisplayTextModel DefaultDisplayText
        {
            get => _defaultDisplayText;
            set => _defaultDisplayText = value ?? new ElectDataTableDefaultDisplayTextModel();
        }

        /// <summary>
        ///     Control the way to parse string to DateTime every request. If value is
        ///     <see cref="DateTimeFormatType.Specific" />, every request will use the
        ///     <see cref="DateTimeFormatType" /> to parse string to DateTime. Else, will try parse
        ///     string to DateTime by any format.
        ///     <para> Value is "Auto" by default </para>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DateTimeFormatType RequestDateTimeFormatType { get; set; } = DateTimeFormatType.Auto;

        /// <summary>
        ///     Shared resource type to localize all
        ///     <see cref="Attributes.DataTableAttribute.DisplayName" /> and will be override by
        ///     <see cref="Attributes.DataTableAttribute.DisplayNameResourceType" /> if set
        /// </summary>
        public Type SharedResourceType { get; set; }
    }

    public class ElectDataTableDefaultDisplayTextModel
    {
        /// <summary>
        ///     Default is "Yes"
        /// </summary>
        public string Yes { get; set; } = "Yes";
        
        /// <summary>
        ///     Default is "No"
        /// </summary>
        public string No { get; set; } = "No";
        
        /// <summary>
        ///     Default is "Filter"
        /// </summary>
        public string Filter { get; set; } = "Filter";
        
        /// <summary>
        ///     Default is "Filter by"
        /// </summary>
        public string FilterBy { get; set; } = "Filter by";
        
        /// <summary>
        ///     Default is "All"
        /// </summary>
        public string All { get; set; } = "All";
    }
}