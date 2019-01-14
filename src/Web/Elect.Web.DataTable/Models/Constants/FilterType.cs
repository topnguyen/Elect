#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> FilterType.cs </Name>
//         <Created> 23/03/2018 4:08:12 PM </Created>
//         <Key> 3a41993e-eadc-4d0d-9d50-b2b1672e81ca </Key>
//     </File>
//     <Summary>
//         FilterType.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using System.ComponentModel.DataAnnotations;

namespace Elect.Web.DataTable.Models.Constants
{
    public enum FilterType
    {
        /// <summary>
        ///     Not show any input in Column Filter 
        /// </summary>
        [Display(Name = FilterConstants.None)] 
        None,

        /// <summary>
        ///     Display as Drop-down list in Column Filter 
        /// </summary>
        [Display(Name = FilterConstants.Select)]
        Select,

        /// <summary>
        ///     Display as Free Text input in Column Filter 
        /// </summary>
        [Display(Name = FilterConstants.Text)] 
        Text,

        [Obsolete("You need self implement UI for checkbox type in the jquery.dataTables.columnFilter.js")]
        [Display(Name = FilterConstants.Checkbox)]
        Checkbox,

        [Obsolete("You need self implement UI for number-range type in the jquery.dataTables.columnFilter.js")]
        [Display(Name = FilterConstants.NumberRange)]
        NumberRange,

        [Obsolete("You need self implement UI for date-range type in the jquery.dataTables.columnFilter.js")]
        [Display(Name = FilterConstants.DateRange)]
        DateRange,

        [Obsolete("You need self implement UI for datetime-range type in the jquery.dataTables.columnFilter.js")]
        [Display(Name = FilterConstants.DateTimeRange)]
        DateTimeRange
    }
}