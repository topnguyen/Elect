namespace Elect.Web.DataTable.Models
{
    public class DataTableModel
    {
        private string _dom;
        public DataTableModel(string id, string ajaxUrl, params ColumnModel[] columns)
        {
            AjaxUrl = ajaxUrl;
            Id = id;
            Columns = columns.ToList();
            ColumnFilterGlobalConfig = new ColumnFilterGlobalConfigModel(this);
        }
        public bool? IsDevelopMode { get; set; }
        public bool? IsAutoWidthColumn { get; set; }
        public bool? IsResponsive { get; set; }
        public bool? IsEnableColVis { get; set; }
        public bool? IsSaveState { get; set; }
        public bool? IsScrollX { get; set; }
        public bool? IsEnableColReorder { get; set; }
        public bool? IsShowFooter { get; set; }
        public bool? IsShowPageSize { get; set; } = true;
        /// <summary>
        ///     Show global search input, default is true. 
        /// </summary>
        public bool? IsShowGlobalSearchInput { get; set; } = true;
        public bool? IsUseTableTools { get; set; }
        public bool? IsHideHeader { get; set; }
        public bool? IsUseColumnFilter { get; set; }
        /// <summary>
        ///     Enable to make the search fast and helpful for UI render, default is true. 
        /// </summary>
        public bool? IsDeferRender { get; set; } = true;
        /// <summary>
        ///     Table class, default is "table table-hover dataTable table-striped". 
        /// </summary>
        public string TableClass { get; set; } = "table table-hover dataTable table-striped";
        public string Id { get; set; }
        public string AjaxUrl { get; set; }
        public List<ColumnModel> Columns { get; set; }
        public IDictionary<string, object> AdditionalOptions { get; } = new Dictionary<string, object>();
        public ColumnFilterGlobalConfigModel ColumnFilterGlobalConfig { get; set; }
        public string Dom
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_dom))
                    return _dom;
                var str = "<\"dt-panelmenu clearfix\"";
                if (IsShowPageSize == true)
                    str += "l";
                if (IsShowGlobalSearchInput == true)
                    str += "f";
                if (IsUseTableTools == true)
                    str += "B";
                return str + ">t<\"dt-panelfooter clearfix\"rip>";
            }
            set => _dom = value;
        }
        public string LanguageCode { get; set; }
        public LengthMenuModel LengthMenu { get; set; } = new LengthMenuModel
        {
            Tuple.Create("5", 5),
            Tuple.Create("10", 10),
            Tuple.Create("25", 25),
            Tuple.Create("50", 50),
            Tuple.Create("100", 100),
            Tuple.Create("250", 250),
            Tuple.Create("500", 500),
            Tuple.Create("1000", 1000)
        };
        public int? PageSize { get; set; }
        public string GlobalJsVariableName { get; set; }
        /// <summary>
        ///     Your javascript function as string with params: jqXHR, textStatus, errorThrown. Ex:
        ///     "function(jqXHR, textStatus, errorThrown){console.log(textStatus)}"
        /// </summary>
        public string AjaxErrorHandler { get; set; }
        /// <summary>
        ///     Function name of Initial Complete callback. DataTable will pass "settings" and "json"
        ///     to the function. Ex: initCompleteHandle(settings, json).
        /// </summary>
        public string InitCompleteFunctionName { get; set; }
        /// <summary>
        ///     Function name of Draw callback. DataTable will pass "settings" to the function. Ex: drawCallBackHandle(oSettings).
        /// </summary>
        public string DrawCallbackFunctionName { get; set; }
        /// <summary>
        ///     Function name of Footercallback. DataTable will pass "row, data, start, end, display"
        ///     to the function. Ex: footerCallbackHandle(row, data, start, end, display).
        /// </summary>
        public string FooterCallbackFunctionName { get; set; }
        /// <summary>
        ///     Function name of Responsive Resize callback. DataTable will pass "e, datatable,
        ///     columns" to the function. Ex: responsiveResizeCallBackHandle(e, datatable, columns).
        /// </summary>
        /// <remarks> see more: https://datatables.net/reference/event/responsive-resize </remarks>
        public string ResponsiveResizeCallbackFunctionName { get; set; }
        /// <summary>
        ///     Function name of before send handler, you can modified data before submit by this
        ///     way. DataTable will pass "list name-value" submit to server as params to the function.
        ///     Ex: beforeSendHandle(aoData).
        /// </summary>
        public string BeforeSendFunctionName { get; set; }
    }
}
