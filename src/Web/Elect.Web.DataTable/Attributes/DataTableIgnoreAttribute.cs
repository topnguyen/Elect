namespace Elect.Web.DataTable.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Prevent a public property from being included as a column in a DataTable row model 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DataTableIgnoreAttribute : Attribute
    {
    }
}
