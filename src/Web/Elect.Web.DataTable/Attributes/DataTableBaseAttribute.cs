#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DataTableBaseAttribute.cs </Name>
//         <Created> 23/03/2018 4:43:31 PM </Created>
//         <Key> 3ea70f2e-c72c-437e-ba23-86ffe7949cbe </Key>
//     </File>
//     <Summary>
//         DataTableBaseAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Column;
using System;
using System.Reflection;

namespace Elect.Web.DataTable.Attributes
{
    public abstract class DataTableBaseAttribute : Attribute
    {
        public abstract void ApplyTo(ColumnModel columnModel, PropertyInfo propertyInfo);
    }
}