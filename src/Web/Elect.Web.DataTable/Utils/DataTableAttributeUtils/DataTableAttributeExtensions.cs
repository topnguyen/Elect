namespace Elect.Web.DataTable.Utils.DataTableAttributeUtils
{
    internal static class DataTableAttributeExtensions
    {
        internal static string GetDisplayName(this DataTableAttribute attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute.DisplayName))
            {
                return attribute.DisplayName;
            }
            // Translate by Attribute Resource Type is first Priority
            if (attribute.DisplayNameResourceType != null)
            {
                return ElectDataTableTranslator.Get(attribute.DisplayName, attribute.DisplayNameResourceType);
            }
            // Translate by Shared Resource Type
            return ElectDataTableTranslator.Get(attribute.DisplayName);
        }
    }
}
