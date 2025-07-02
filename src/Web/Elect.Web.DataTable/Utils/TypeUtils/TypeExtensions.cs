namespace Elect.Web.DataTable.Utils.TypeUtils
{
    internal static class TypeExtensions
    {
        internal static ColumnModel[] GetColumns(this Type t)
        {
            var listPropertyInfo = DataTableTypeInfoModelHelper.GetProperties(t);
            var columnList = new List<ColumnModel>();
            foreach (var propertyInfo in listPropertyInfo)
            {
                var colDef = new ColumnModel(propertyInfo.PropertyInfo.Name, propertyInfo.PropertyInfo.PropertyType);
                foreach (var att in propertyInfo.Attributes)
                {
                    att.ApplyTo(colDef, propertyInfo.PropertyInfo);
                }
                columnList.Add(colDef);
            }
            return columnList.ToArray();
        }
        internal static ColumnModel[] GetColumns<TResult>()
        {
            return GetColumns(typeof(TResult));
        }
    }
}
