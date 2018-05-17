#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IHtmlHelperExtensions.cs </Name>
//         <Created> 24/03/2018 2:19:14 PM </Created>
//         <Key> ae7ff9dc-19a7-4bb9-a282-188eb0588a8a </Key>
//     </File>
//     <Summary>
//         IHtmlHelperExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using System.Linq;
using System.Linq.Expressions;
using Elect.Web.DataTable.Models;
using Elect.Web.DataTable.Models.Column;
using Elect.Web.DataTable.Utils.ExpressionUtils;
using Elect.Web.DataTable.Utils.TypeUtils;
using Elect.Web.IUrlHelperUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Elect.Web.DataTable
{
    public static class IHtmlHelperExtensions
    {
        public static DataTableModel DataTableModel<TController, T>(this IHtmlHelper html, string id,
            Expression<Func<TController, DataTableActionResult<T>>> exp, params ColumnModel[] columns)
            where T : class, new()
        {
            if (columns?.Any() != true) columns = typeof(T).GetColumns();

            var methodInfo = exp.MethodInfo();

            var controllerName = typeof(TController).Name;

            controllerName = controllerName.Substring(0,
                controllerName.LastIndexOf(nameof(Controller), StringComparison.CurrentCultureIgnoreCase));

            var urlHelper = new UrlHelper(html.ViewContext);

            var ajaxUrl = urlHelper.AbsoluteAction(methodInfo.Name, controllerName);

            return new DataTableModel(id, ajaxUrl, columns);
        }

        public static DataTableModel DataTableModel(this IHtmlHelper html, Type t, string id, string uri)
        {
            return new DataTableModel(id, uri, t.GetColumns());
        }

        public static DataTableModel DataTableModel<T>(string id, string uri)
        {
            return new DataTableModel(id, uri, typeof(T).GetColumns());
        }

        public static DataTableModel DataTableModel<TResult>(this IHtmlHelper html, string id, string uri)
        {
            return DataTableModel(html, typeof(TResult), id, uri);
        }

        public static DataTableModel DataTableModel(this IHtmlHelper html, string id, string ajaxUrl,
            params string[] columns)
        {
            var listColumnDef = columns.Select(c => new ColumnModel(c, typeof(string))).ToArray();

            return new DataTableModel(id, ajaxUrl, listColumnDef);
        }
    }
}