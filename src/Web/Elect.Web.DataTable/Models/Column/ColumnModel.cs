namespace Elect.Web.DataTable.Models.Column
{
    public class ColumnModel
    {
        public ColumnModel(string name, Type type)
        {
            Name = name;
            DisplayName = name;
            Type = type;
            ColumnFilter = new ColumnFilterModel(Type);
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsSortable { get; set; } = true;
        public Type Type { get; set; }
        public bool IsSearchable { get; set; } = true;
        public string CssClass { get; set; } = string.Empty;
        public string CssClassHeader { get; set; } = string.Empty;
        public SortDirection SortDirection { get; set; } = SortDirection.None;
        /// <summary>
        ///     Define javascript function name to render data - mRender function name in JQuery DataTable)
        /// </summary>
        /// <remarks>
        ///     Function follow params: function &lt;your function name&gt;(data, type, row). Param
        ///     row: is a array column data
        /// </remarks>
        public string MRenderFunction { get; set; }
        public ColumnFilterModel ColumnFilter { get; set; }
        public JObject SearchColumns { get; set; }
        public Attribute[] CustomAttributes { get; set; }
        public string Width { get; set; }
        /// <summary>
        ///     Set place holder for the filter column as hint. If null will take default by the logic in _DataTableHTML.cshtml
        /// </summary>
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
    }
}
