#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TypeHelper.cs </Name>
//         <Created> 15/03/2018 8:27:11 PM </Created>
//         <Key> addf2081-46f4-45ad-8680-e7ef6fd30344 </Key>
//     </File>
//     <Summary>
//         TypeHelper.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Reflection;

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