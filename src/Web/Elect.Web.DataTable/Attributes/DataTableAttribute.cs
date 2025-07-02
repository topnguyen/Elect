namespace Elect.Web.DataTable.Attributes
{
    public class DataTableAttribute : DataTableBaseAttribute
    {
        /// <summary>
        ///     Column Header Name.
        /// </summary>
        /// <remarks>Support Multi Language Translation by setup <see cref="ElectDataTableOptions.SharedResourceType"/>.</remarks>
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsSearchable { get; set; } = true;
        public bool IsSortable { get; set; } = true;
        public int Order { get; set; } = ConfigConstants.DefaultOrder;
        public Type DisplayNameResourceType { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.None;
        public string MRenderFunction { get; set; }
        public string CssClass { get; set; }
        public string CssClassHeader { get; set; }
        public string Width { get; set; }
        /// <summary>
        ///     Set place holder for the filter column as hint. If null will take default by the logic in _DataTableHTML.cshtml
        /// </summary>
        /// <remarks>The filter col in this context is below main column.</remarks>
        /// <remarks>Support Multi Language Translation by setup <see cref="ElectDataTableOptions.SharedResourceType"/>.</remarks>
        public string FilterColHint { get; set; }
        /// <summary>
        ///     Additional HTML Element attributes for filter column 
        /// </summary>
        /// <remarks> Ex: "data-toggle='tooltip' data-original-title='Tooltip Title'" </remarks>
        public string FilterColAdditionalAttribute { get; set; }
        /// <summary>
        ///     Additional HTML Element attributes for header column 
        /// </summary>
        /// <remarks> Ex: "data-toggle='tooltip' data-original-title='Tooltip Title'" </remarks>
        public string AdditionalAttributeHeader { get; set; }
        public override void ApplyTo(ColumnModel columnModel, PropertyInfo propertyInfo)
        {
            columnModel.DisplayName = this.GetDisplayName() ?? ElectDataTableTranslator.Get(columnModel.Name);
            columnModel.IsSortable = IsSortable;
            columnModel.IsVisible = IsVisible;
            columnModel.IsSearchable = IsSearchable;
            columnModel.SortDirection = SortDirection;
            columnModel.MRenderFunction = MRenderFunction;
            columnModel.CssClass = CssClass;
            columnModel.CssClassHeader = CssClassHeader;
            columnModel.CustomAttributes = propertyInfo.GetCustomAttributes().ToArray();
            columnModel.Width = Width;
            columnModel.FilterColHint = ElectDataTableTranslator.Get(FilterColHint);
            columnModel.FilterColAdditionalAttribute = FilterColAdditionalAttribute;
            columnModel.AdditionalAttributeHeader = AdditionalAttributeHeader;
        }
    }
}
