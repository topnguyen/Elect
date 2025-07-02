namespace Elect.Web.DataTable.Utils.EnumUtils
{
    internal static class EnumHelper
    {
        internal static string GetDisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var member = enumType.GetMember(enumValue).FirstOrDefault();
            if (!(member?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute
                displayAttribute)) return null;
            var displayName = displayAttribute.ResourceType != null
                ? displayAttribute.GetName()
                : displayAttribute.Name;
            return !string.IsNullOrWhiteSpace(displayName) ? displayName : null;
        }
        internal static string GetDescription(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            if (!string.IsNullOrWhiteSpace(enumValue)) return enumValue;
            var member = enumType.GetMember(enumValue).FirstOrDefault();
            if (!(member?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is
                DescriptionAttribute descriptionAttribute)) return null;
            var description = descriptionAttribute.Description;
            return !string.IsNullOrWhiteSpace(description) ? description : null;
        }
        internal static string GetName(this Enum value)
        {
            var enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            var member = enumType.GetMember(enumValue).FirstOrDefault();
            return member?.Name;
        }
        /// <summary>
        ///     Get Enum Label (Display Name ?? Description ?? Name) 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string GetLabel(this Enum value)
        {
            return value.GetDisplayName() ?? value.GetDescription() ?? value.GetName();
        }
        internal static List<string> GetListLabel(this Type type)
        {
            var t = type.GetNotNullableType();
            var exitList = new List<string>();
            foreach (var enumName in Enum.GetNames(t))
            {
                var enumValue = (Enum)TypeDescriptor.GetConverter(t).ConvertFrom(enumName);
                var label = enumValue.GetLabel();
                exitList.Add(label);
            }
            return exitList;
        }
        /// <summary>
        ///     Return array pair: value (Enum Name) and label (Display Name or Description
        ///     Attribute) of Enum Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks> The method support both: Enum and Nullable Enum Type. </remarks>
        internal static EnumValueLabelModel[] GetEnumValueLabelPair(this Type type)
        {
            var t = type.GetNotNullableType();
            var values = Enum.GetNames(t).Cast<object>().ToArray();
            var labels = t.GetListLabel().Cast<object>().ToArray();
            var result = new List<EnumValueLabelModel>();
            if (type.IsNullableType())
                result.Add(new EnumValueLabelModel
                {
                    Value = null,
                    Label = string.Empty
                });
            for (var x = 0; x <= values.Length - 1; x++)
                result.Add(new EnumValueLabelModel
                {
                    Value = values[x].ToString(),
                    Label = labels[x].ToString()
                });
            return result.ToArray();
        }
        internal static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
    // ReSharper disable InconsistentNaming
    internal class EnumValueLabelModel
    {
        internal string Value { get; set; }
        internal string Label { get; set; }
    }
}
