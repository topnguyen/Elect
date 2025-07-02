namespace Elect.Web.DataTable.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class DataTableBaseAttribute : Attribute
    {
        public abstract void ApplyTo(ColumnModel columnModel, PropertyInfo propertyInfo);
    }
}
