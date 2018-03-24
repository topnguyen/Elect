#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableModel.cs </Name>
//         <Created> 23/03/2018 4:19:51 PM </Created>
//         <Key> cb2d204c-abc9-400f-b050-768ff686b062 </Key>
//     </File>
//     <Summary>
//         DataTableModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Column;
using Elect.Web.DataTable.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Web.DataTable.Models
{
    public class DataTableModel
    {
        public bool IsDevelopMode { get; set; } = false;

        public bool IsAutoWidthColumn { get; set; } = false;

        public bool IsResponsive { get; set; } = true;

        public bool IsEnableColVis { get; set; } = true;

        public bool IsShowPageSize { get; set; } = true;

        /// <summary>
        ///     Show global search input, default is true. 
        /// </summary>
        public bool IsShowGlobalSearchInput { get; set; } = true;

        public bool IsUseTableTools { get; set; } = true;

        public bool IsHideHeader { get; set; } = false;

        public bool IsUseColumnFilter { get; set; } = false;

        /// <summary>
        ///     Enable to make the search fast and helpful for UI render, default is true. 
        /// </summary>
        public bool IsDeferRender { get; set; } = true;

        /// <summary>
        ///     Table class, default is "table table-hover dataTable table-striped". 
        /// </summary>
        public string TableClass { get; set; } = "table table-hover dataTable table-striped";

        public string Id { get; set; }

        public string AjaxUrl { get; set; }

        public List<ColumnModel> Columns { get; set; }

        public IDictionary<string, object> AdditionalOptions { get; } = new Dictionary<string, object>();

        public ColumnFilterGlobalConfigModel ColumnFilterGlobalConfig { get; set; }

        private string _dom;

        public string Dom
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_dom))
                    return _dom;

                string str = "<\"dt-panelmenu clearfix\"";
                if (IsShowPageSize)
                    str += "l";
                if (IsShowGlobalSearchInput)
                    str += "f";
                if (IsUseTableTools)
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
            Tuple.Create("All", -1)
        };

        public int? PageSize { get; set; }

        public string GlobalJsVariableName { get; set; }

        /// <summary>
        ///     Your javascript function as string with params: jqXHR, textStatus, errorThrown. Ex:
        ///     "function(jqXHR, textStatus, errorThrown){console.log(textStatus)}"
        /// </summary>
        public string AjaxErrorHandler { get; set; }

        /// <summary>
        ///     Function name of Draw Call Back. DataTable will pass "setting" to the function. Ex: drawCallBackHandle(oSettings).
        /// </summary>
        public string DrawCallbackFunctionName { get; set; }

        /// <summary>
        ///     Function name of Responsive Resize Call Back. DataTable will pass "e, datatable,
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

        public DataTableModel(string id, string ajaxUrl, params ColumnModel[] columns)
        {
            AjaxUrl = ajaxUrl;
            Id = id;
            Columns = columns.ToList();
            ColumnFilterGlobalConfig = new ColumnFilterGlobalConfigModel(this);
        }
    }
}