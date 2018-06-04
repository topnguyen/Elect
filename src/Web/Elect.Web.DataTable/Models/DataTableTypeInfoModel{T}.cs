#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableTypeInfoModel_T_.cs </Name>
//         <Created> 23/03/2018 5:23:00 PM </Created>
//         <Key> ab5c66b3-097f-4842-ad38-bf9f32dc44ca </Key>
//     </File>
//     <Summary>
//         DataTableTypeInfoModel_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Utils.DataTableTypeInfoModelUtils;
using System;
using System.Collections.Generic;

namespace Elect.Web.DataTable.Models
{
    public class DataTableTypeInfoModel<T>
    {
        public DataTablePropertyInfoModel[] Properties => DataTableTypeInfoModelHelper.GetProperties(typeof(T));

        public Dictionary<string, object> ToDictionary(T value)
        {
            var dictionary = new Dictionary<string, object>();

            foreach (var pi in Properties) dictionary[pi.PropertyInfo.Name] = pi.PropertyInfo.GetValue(value, null);

            return dictionary;
        }

        public Func<T, Dictionary<string, object>> ToFuncDictionary()
        {
            return ToDictionary;
        }
    }
}