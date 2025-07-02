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
