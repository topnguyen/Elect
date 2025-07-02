namespace Elect.Core.TypeUtils
{
    public static class TypeExtensions
    {
        public static Type GetNotNullableType(this Type type)
        {
            return TypeHelper.GetNotNullableType(type);
        }
        public static bool IsNullableType(this Type type)
        {
            return TypeHelper.IsNullableType(type);
        }
        public static bool IsNumericType(this Type type)
        {
            return TypeHelper.IsNumericType(type);
        }
        public static bool IsGenericType(this Type type, Type genericType) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
        public static bool IsImplementGenericInterface(this Type type, Type interfaceType) => type.IsGenericType(interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => @interface.IsGenericType(interfaceType));
    }
}
