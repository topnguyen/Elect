namespace Elect.Web.DataTable.Utils.TypeUtils
{
    internal class TypeHelper
    {
        internal static T GetResourceLookup<T>(Type resourceType, string resourceName)
        {
            resourceType = resourceType ?? ElectDataTableOptions.Instance.SharedResourceType;
            if (resourceType == null || resourceName == null) return default;
            var property = resourceType.GetProperty(resourceName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic);
            if (property == null) return default;
            return (T)property.GetValue(null, null);
        }
    }
}
