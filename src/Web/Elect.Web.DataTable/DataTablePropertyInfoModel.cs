#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTablePropertyInfoModel.cs </Name>
//         <Created> 23/03/2018 4:41:32 PM </Created>
//         <Key> c900c821-dfe6-451c-93c6-07a489198ba2 </Key>
//     </File>
//     <Summary>
//         DataTablePropertyInfoModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Attributes;
using System;

namespace Elect.Web.DataTable
{
    public class DataTablePropertyInfoModel
    {
        public DataTablePropertyInfoModel(System.Reflection.PropertyInfo propertyInfo, DataTableBaseAttribute[] attributes)
        {
            PropertyInfo = propertyInfo;

            Attributes = attributes;
        }

        public System.Reflection.PropertyInfo PropertyInfo { get; }

        public DataTableBaseAttribute[] Attributes { get; }

        public Type Type => PropertyInfo.PropertyType;
    }
}