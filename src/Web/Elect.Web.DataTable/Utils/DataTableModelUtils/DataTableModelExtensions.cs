namespace Elect.Web.DataTable.Utils.DataTableModelUtils
{
    public static class DataTableModelExtensions
    {
        public static string GetColumnDefine(this DataTableModel model)
        {
            return GetColumnDefine(model.Columns.ToArray());
        }
        public static string GetColumnSortDefine(this DataTableModel model)
        {
            return GetColumnSortDefine(model.Columns.ToArray());
        }
        public static JToken GetColumnSearchableDefine(this DataTableModel model)
        {
            var initialSearches = model.Columns
                .Select(c => c.IsSearchable & (c.SearchColumns != null) ? c.SearchColumns : null as object).ToArray();
            return new JArray(initialSearches);
        }
        public static ColumnFilterModel<DataTableModel> FilterOn(this DataTableModel model, string columnName,
            object jsOptions)
        {
            IDictionary<string, object> optionsDict = jsOptions.ToDictionary();
            return model.FilterOn(columnName, optionsDict);
        }
        public static ColumnFilterModel<DataTableModel> FilterOn(this DataTableModel model, string columnName,
            IDictionary<string, object> filterOptions)
        {
            return model.FilterOn(columnName, filterOptions, null);
        }
        public static ColumnFilterModel<DataTableModel> FilterOn(this DataTableModel model, string columnName,
            object jsOptions, object jsInitialSearchColumns)
        {
            IDictionary<string, object> optionsDict = jsOptions.ToDictionary();
            IDictionary<string, object> initialSearchColsDict = jsInitialSearchColumns.ToDictionary();
            return model.FilterOn(columnName, optionsDict, initialSearchColsDict);
        }
        public static ColumnFilterModel<DataTableModel> FilterOn(this DataTableModel model, string columnName,
            IDictionary<string, object> filterOptions, IDictionary<string, object> jsInitialSearchColumns)
        {
            var columns = model.Columns.Single(c => c.Name == columnName);
            if (filterOptions != null)
                foreach (var jsOption in filterOptions)
                    columns.ColumnFilter[jsOption.Key] = jsOption.Value;
            if (jsInitialSearchColumns != null)
            {
                columns.SearchColumns = new JObject();
                foreach (var jsInitialSearchCol in jsInitialSearchColumns)
                    columns.SearchColumns[jsInitialSearchCol.Key] = new JValue(jsInitialSearchCol.Value);
            }
            return new ColumnFilterModel<DataTableModel>(model, columns);
        }
        private static string GetColumnDefine(params ColumnModel[] columns)
        {
            bool IsFalse(bool x)
            {
                return x == false;
            }
            bool IsNonEmptyString(string x)
            {
                return !string.IsNullOrEmpty(x);
            }
            var columnsJsonObject = new List<dynamic>();
            columnsJsonObject.AddRange(ConvertColumnToTargetedProperty(PropertyConstants.Sortable,
                column => column.IsSortable, IsFalse, columns));
            columnsJsonObject.AddRange(ConvertColumnToTargetedProperty(PropertyConstants.Visible,
                column => column.IsVisible, IsFalse, columns));
            columnsJsonObject.AddRange(ConvertColumnToTargetedProperty(PropertyConstants.Searchable,
                column => column.IsSearchable, IsFalse, columns));
            columnsJsonObject.AddRange(ConvertColumnToTargetedProperty(PropertyConstants.Render,
                column => column.MRenderFunction, propertyConverter: x => new JRaw(x),
                propertyPredicate: IsNonEmptyString, columns: columns));
            columnsJsonObject.AddRange(ConvertColumnToTargetedProperty(PropertyConstants.ClassName,
                column => column.CssClass, IsNonEmptyString, columns));
            columnsJsonObject.AddRange(ConvertColumnToTargetedProperty(PropertyConstants.Width, column => column.Width,
                IsNonEmptyString, columns));
            return columnsJsonObject.Any() ? JsonConvert.SerializeObject(columnsJsonObject) : DataConstants.EmptyArray;
        }
        private static string GetColumnSortDefine(params ColumnModel[] columns)
        {
            var sortList = columns
                .Select((c, idx) => c.SortDirection == SortDirection.None
                    ? new dynamic[] { -1, string.Empty }
                    : (c.SortDirection == SortDirection.Ascending
                        ? new dynamic[] { idx, SortDirectionConstants.Ascending }
                        : new dynamic[] { idx, SortDirectionConstants.Descending })).Where(x => x[0] > -1).ToArray();
            return sortList.Any() ? JsonConvert.SerializeObject(sortList) : DataConstants.EmptyArray;
        }
        private static IEnumerable<JObject> ConvertColumnToTargetedProperty<TProperty>(string jsonPropertyName,
            Func<ColumnModel, TProperty> propertySelector, Func<TProperty, bool> propertyPredicate,
            params ColumnModel[] columns)
        {
            return ConvertColumnToTargetedProperty(jsonPropertyName, propertySelector, propertyPredicate, x => x,
                columns);
        }
        private static IEnumerable<JObject> ConvertColumnToTargetedProperty<TProperty, TResult>(string jsonPropertyName,
            Func<ColumnModel, TProperty> propertySelector, Func<TProperty, bool> propertyPredicate,
            Func<TProperty, TResult> propertyConverter, params ColumnModel[] columns)
        {
            return
                columns
                    .Select((x, idx) => new { rawPropertyValue = propertySelector(x), idx })
                    .Where(x => propertyPredicate(x.rawPropertyValue))
                    .GroupBy(
                        x => x.rawPropertyValue,
                        (rawPropertyValue, groupedItems) =>
                            new
                            {
                                rawPropertyValue,
                                indices = groupedItems.Select(x => x.idx)
                            })
                    .Select(x => new JObject(
                        new JProperty(jsonPropertyName, propertyConverter(x.rawPropertyValue)),
                        new JProperty(PropertyConstants.Targets, new JArray(x.indices))
                    ));
        }
    }
}
