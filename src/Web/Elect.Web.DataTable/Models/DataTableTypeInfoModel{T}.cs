namespace Elect.Web.DataTable.Models
{
    public class DataTableTypeInfoModel<T>
    {
        public DataTablePropertyInfoModel[] Properties => DataTableTypeInfoModelHelper.GetProperties(typeof(T));
        public Dictionary<string, object> ToDictionary(T value)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var pi in Properties)
            {
                dictionary[pi.PropertyInfo.Name] = pi.PropertyInfo.GetValue(value, null);
            }
            return dictionary;
        }
        public Func<T, Dictionary<string, object>> ToFuncDictionary()
        {
            return ToDictionary;
        }
    }
}
