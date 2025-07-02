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
