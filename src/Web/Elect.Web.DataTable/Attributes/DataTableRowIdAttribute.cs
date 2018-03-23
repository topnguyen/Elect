#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableRowIdAttribute.cs </Name>
//         <Created> 23/03/2018 4:44:18 PM </Created>
//         <Key> 686ebf35-42a4-44c9-9fe0-c59544182104 </Key>
//     </File>
//     <Summary>
//         DataTableRowIdAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Column;
using System.Reflection;

namespace Elect.Web.DataTable.Attributes
{
    public class DataTableRowIdAttribute : DataTableBaseAttribute
    {
        public bool EmitAsColumnName { get; set; }

        public override void ApplyTo(ColumnModel columnModel, PropertyInfo propertyInfo)
        {
            // This attribute does not affect rendering
        }

        public DataTableRowIdAttribute()
        {
            EmitAsColumnName = true;
        }
    }
}