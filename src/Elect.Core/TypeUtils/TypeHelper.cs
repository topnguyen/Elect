namespace Elect.Core.TypeUtils
{
    public class TypeHelper
    {
        public static Type GetNotNullableType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }
        public static bool IsNullableType(Type type)
        {
            Type u = Nullable.GetUnderlyingType(type);
            return u != null;
        }
        public static bool IsNumericType(Type type)
        {
            if (type == null || GetNotNullableType(type).IsEnum)
            {
                return false;
            }
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
                case TypeCode.Object:
                    if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return IsNumericType(Nullable.GetUnderlyingType(type));
                    }
                    return false;
            }
            return false;
        }
    }
}
