namespace Elect.Web.DataTable.Models
{
    public class DataTablePropertyInfoModel
    {
        public DataTablePropertyInfoModel(PropertyInfo propertyInfo, DataTableBaseAttribute[] attributes)
        {
            PropertyInfo = propertyInfo;
            Attributes = attributes;
        }
        public PropertyInfo PropertyInfo { get; }
        public DataTableBaseAttribute[] Attributes { get; }
        public Type Type => PropertyInfo.PropertyType;
        public int Order { get; set; } = ConfigConstants.DefaultOrder;
    }
}
