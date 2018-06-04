#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TypeExtensions.cs </Name>
//         <Created> 23/03/2018 5:19:27 PM </Created>
//         <Key> 469bec6a-f671-4fd2-87a3-0c17ef327a45 </Key>
//     </File>
//     <Summary>
//         TypeExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Models.Column;
using Elect.Web.DataTable.Utils.DataTableTypeInfoModelUtils;
using System;
using System.Collections.Generic;

namespace Elect.Web.DataTable.Utils.TypeUtils
{
    internal static class TypeExtensions
    {
        internal static ColumnModel[] GetColumns(this Type t)
        {
            var listPropertyInfo = DataTableTypeInfoModelHelper.GetProperties(t);

            var columnList = new List<ColumnModel>();

            foreach (var propertyInfo in listPropertyInfo)
            {
                var colDef = new ColumnModel(propertyInfo.PropertyInfo.Name, propertyInfo.PropertyInfo.PropertyType);

                foreach (var att in propertyInfo.Attributes) att.ApplyTo(colDef, propertyInfo.PropertyInfo);

                columnList.Add(colDef);
            }

            return columnList.ToArray();
        }

        internal static ColumnModel[] GetColumns<TResult>()
        {
            return GetColumns(typeof(TResult));
        }
    }
}