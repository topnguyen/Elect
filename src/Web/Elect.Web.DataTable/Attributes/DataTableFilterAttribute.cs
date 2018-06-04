#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableFilterAttribute.cs </Name>
//         <Created> 23/03/2018 4:45:06 PM </Created>
//         <Key> 37a6b85b-097d-4a2a-8540-07c4e1e8bef0 </Key>
//     </File>
//     <Summary>
//         DataTableFilterAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Models.Column;
using Elect.Web.DataTable.Models.Constants;
using EnumsNET;
using System.Reflection;

namespace Elect.Web.DataTable.Attributes
{
    public class DataTableFilterAttribute : DataTableBaseAttribute
    {
        private readonly string _filterType;

        public DataTableFilterAttribute()
        {
        }

        public DataTableFilterAttribute(string filterType) : this()
        {
            _filterType = filterType;
        }

        public DataTableFilterAttribute(FilterType filterType) : this(GetFilterTypeString(filterType))
        {
        }

        /// <summary>
        ///     Sets sSelector on the column (i.e. selector for custom positioning) 
        /// </summary>
        public string Selector { get; set; }

        private static string GetFilterTypeString(FilterType filterType)
        {
            return filterType.AsString(EnumFormat.DisplayName);
        }

        public override void ApplyTo(ColumnModel columnModel, PropertyInfo propertyInfo)
        {
            columnModel.ColumnFilter = new ColumnFilterModel(propertyInfo.PropertyType);

            if (Selector != null) columnModel.ColumnFilter[PropertyConstants.Selector] = Selector;
            if (_filterType == FilterConstants.None)
            {
                columnModel.ColumnFilter = null;
            }
            else
            {
                if (_filterType != null) columnModel.ColumnFilter.FilterType = _filterType;
            }
        }
    }
}