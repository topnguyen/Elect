#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TypeExtensions.cs </Name>
//         <Created> 15/03/2018 8:33:01 PM </Created>
//         <Key> dd0d698a-a858-4246-a79e-6e59a8f0c1d4 </Key>
//     </File>
//     <Summary>
//         TypeExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Linq;
using System.Reflection;

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