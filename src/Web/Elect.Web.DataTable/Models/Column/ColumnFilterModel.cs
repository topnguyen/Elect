#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ColumnFilter.cs </Name>
//         <Created> 23/03/2018 4:21:49 PM </Created>
//         <Key> 0a5da6bd-ae34-48fb-b4b5-59a1e2fd95ea </Key>
//     </File>
//     <Summary>
//         ColumnFilter.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Core.TypeUtils;
using Elect.Web.DataTable.Models.Constants;
using Elect.Web.DataTable.Utils.EnumUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Elect.Web.DataTable.Models.Column
{
    public class ColumnFilterModel : Hashtable
    {
        internal object[] FilterValues { set => this[FilterConstants.Values] = value; }

        internal string FilterType { set => this[FilterConstants.Type] = value; }

        public ColumnFilterModel(Type t)
        {
            SetDefaultValuesAccordingToColumnType(t);
        }

        private static readonly List<Type> DateTypes = new List<Type> { typeof(DateTime), typeof(DateTime?), typeof(DateTimeOffset), typeof(DateTimeOffset?) };

        private void SetDefaultValuesAccordingToColumnType(Type type)
        {
            if (type == null)
            {
                Remove(FilterConstants.Type);
            }
            else if (DateTypes.Contains(type))
            {
                FilterType = FilterConstants.Text;
            }
            else if (type == typeof(bool))
            {
                FilterType = FilterConstants.Select;
                FilterValues = new object[]
                {
                    new
                    {
                        value = DataConstants.True,
                        label = DataConstants.Yes
                    },
                    new
                    {
                        value = DataConstants.False,
                        label = DataConstants.No
                    }
                };
            }
            else if (type == typeof(bool?))
            {
                FilterType = FilterConstants.Select;
                FilterValues = new object[]
                {
                    DataConstants.Null, DataConstants.True, DataConstants.False
                };
            }
            else if (type.GetNotNullableType().IsEnum)
            {
                FilterType = FilterConstants.Select;
                FilterValues = type.GetEnumValueLabelPair().Select(x => new
                {
                    value = string.IsNullOrWhiteSpace(x.Value) ? DataConstants.Null : x.Value,
                    label = x.Label
                }).ToArray<object>();
            }
            else
            {
                FilterType = FilterConstants.Text;
            }
        }
    }
}