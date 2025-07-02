namespace Elect.Web.DataTable.Processing.Request
{
    public class DataTableModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;
            // Depend on "iColumns" property is have or not, we will known this is legacy model or
            // latest style model. Binding the value to model by the legacy or new style mapping.
            var columns = GetValue<int>(valueProvider, PropertyConstants.Columns);
            var dataTableRequestModel =
                columns <= 0 ? BindModel(valueProvider) : BindLegacyModel(valueProvider, columns);
            // Keep all data to Data Property
            dataTableRequestModel.Data = GetDataDictionary(bindingContext);
            // Bind data to result
            bindingContext.Result = ModelBindingResult.Success(dataTableRequestModel);
            return Task.CompletedTask;
        }
        private static DataTableRequestModel BindModel(IValueProvider valueProvider)
        {
            var obj = new DataTableRequestModel
            {
                DisplayStart = GetValue<int>(valueProvider, "start"),
                DisplayLength = GetValue<int>(valueProvider, "length"),
                Search = GetValue<string>(valueProvider, "search[value]"),
                EscapeRegex = GetValue<bool>(valueProvider, "search[regex]"),
                Echo = GetValue<int>(valueProvider, "draw"),
                ColReorderIndexs = GetValueArray<int>(valueProvider, PropertyConstants.ColReorderIndexs)
            };
            var colIdx = 0;
            while (true)
            {
                var colPrefix = $"columns[{colIdx}]";
                var colName = GetValue<string>(valueProvider, $"{colPrefix}[data]");
                if (string.IsNullOrWhiteSpace(colName)) break;
                obj.ColumnNames.Add(colName);
                obj.ListIsSortable.Add(GetValue<bool>(valueProvider, $"{colPrefix}[orderable]"));
                obj.ListIsSearchable.Add(GetValue<bool>(valueProvider, $"{colPrefix}[searchable]"));
                obj.SearchValues.Add(GetValue<string>(valueProvider, $"{colPrefix}[search][value]"));
                obj.ListIsEscapeRegexColumn.Add(GetValue<bool>(valueProvider, $"{colPrefix}[searchable][regex]"));
                colIdx++;
            }
            obj.Columns = colIdx;
            colIdx = 0;
            while (true)
            {
                var colPrefix = $"order[{colIdx}]";
                var orderColumn = GetValue<int?>(valueProvider, $"{colPrefix}[column]");
                if (orderColumn.HasValue)
                {
                    obj.SortCol.Add(orderColumn.Value);
                    obj.SortDir.Add(GetValue<string>(valueProvider, $"{colPrefix}[dir]"));
                    colIdx++;
                }
                else
                {
                    break;
                }
            }
            obj.SortingCols = colIdx;
            return obj;
        }
        private static DataTableRequestModel BindLegacyModel(IValueProvider valueProvider, int columns)
        {
            var obj = new DataTableRequestModel(columns)
            {
                DisplayStart = GetValue<int>(valueProvider, PropertyConstants.DisplayStart),
                DisplayLength = GetValue<int>(valueProvider, PropertyConstants.DisplayLength),
                Search = GetValue<string>(valueProvider, PropertyConstants.Search),
                EscapeRegex = GetValue<bool>(valueProvider, PropertyConstants.EscapeRegex),
                SortingCols = GetValue<int>(valueProvider, PropertyConstants.SortingCols),
                Echo = GetValue<int>(valueProvider, PropertyConstants.Echo),
                ColReorderIndexs = GetValueArray<int>(valueProvider, PropertyConstants.ColReorderIndexs)
            };
            for (var i = 0; i < obj.Columns; i++)
            {
                obj.ListIsSortable.Add(GetValue<bool>(valueProvider, $"{PropertyConstants.Sortable}_{i}"));
                obj.ListIsSearchable.Add(GetValue<bool>(valueProvider, $"{PropertyConstants.Searchable}_{i}"));
                // Important Legacy DataTable bind sSearch for sSearchValues
                obj.SearchValues.Add(GetValue<string>(valueProvider, $"{PropertyConstants.Search}_{i}"));
                obj.ListIsEscapeRegexColumn.Add(GetValue<bool>(valueProvider, $"{PropertyConstants.EscapeRegex}_{i}"));
                obj.SortCol.Add(GetValue<int>(valueProvider, $"{PropertyConstants.SortCol}_{i}"));
                obj.SortDir.Add(GetValue<string>(valueProvider, $"{PropertyConstants.SortDir}_{i}"));
            }
            return obj;
        }
        private static Dictionary<string, object> GetDataDictionary(ModelBindingContext bindingContext)
        {
            var data = new Dictionary<string, object>();
            try
            {
                // Submit Form Request
                if (bindingContext.HttpContext.Request.ContentType?.Contains(ContentType.FormUrlEncoded) == true)
                {
                    var form = bindingContext.HttpContext.Request.Form;
                    var valueProvider = bindingContext.ValueProvider;
                    foreach (var key in form.Keys) data.Add(key, valueProvider.GetValue(key));
                }
                else
                {
                    var submitData = bindingContext.HttpContext.Request.GetBody();
                    data = submitData?.ToDictionary() ?? new Dictionary<string, object>();
                }
            }
            catch
            {
                data = new Dictionary<string, object>();
            }
            return data;
        }
        private static T GetValue<T>(IValueProvider valueProvider, string key)
        {
            var valueResult = valueProvider.GetValue(key);
            var result = valueResult.FirstValue.ConvertTo<T>();
            return result;
        }
        private static List<T> GetValueArray<T>(IValueProvider valueProvider, string key)
        {
            var valueResult = valueProvider.GetValue(key);
            var value = valueResult.FirstValue;
            if (string.IsNullOrWhiteSpace(value)) return new List<T>();
            var result = value.Split(',').Select(x => x.ConvertTo<T>()).ToList();
            return result;
        }
    }
    public class DataTablesModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var isSameType = context.Metadata.ModelType == typeof(DataTableRequestModel);
            return isSameType ? new DataTableModelBinder() : null;
        }
    }
}
