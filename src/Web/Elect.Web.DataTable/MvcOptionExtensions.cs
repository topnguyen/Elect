namespace Elect.Web.DataTable
{
    public static class MvcOptionExtensions
    {
        public static void AddDataTableModelBinder(this MvcOptions mvcOptions)
        {
            var isProviderAdded =
                mvcOptions.ModelBinderProviders.Any(x => x.GetType() == typeof(DataTablesModelBinderProvider));
            if (isProviderAdded) return;
            mvcOptions.ModelBinderProviders.Insert(0, new DataTablesModelBinderProvider());
        }
    }
}
