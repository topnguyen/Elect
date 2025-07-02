namespace Elect.Web.DataTable.Utils.TypeUtils
{
    internal class TransformTypeInfoHelper
    {
        internal static Dictionary<string, object> MergeTransformValuesIntoDictionary<T, TTransform>(
            Func<T, TTransform> transformInput, T input)
        {
            // Get the the properties from the input as a dictionary
            var dict = new DataTableTypeInfoModel<T>().ToDictionary(input);
            // Get the transform object
            var transform = transformInput(input);
            if (transform == null) return dict;
            foreach (var propertyInfo in transform.GetType().GetTypeInfo().GetProperties())
                dict[propertyInfo.Name] = propertyInfo.GetValue(transform, null);
            return dict;
        }
    }
}
