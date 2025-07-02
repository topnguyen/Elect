namespace Elect.Web.DataTable.Models.Column
{
    public class ColumnFilterGlobalConfigModel : Hashtable
    {
        private readonly DataTableModel _model;
        public JObject ColumnBuilders = new JObject();
        public ColumnFilterGlobalConfigModel(DataTableModel model)
        {
            _model = model;
            this[FilterConstants.PlaceHolder] = "head:after";
        }
        public override string ToString()
        {
            this[FilterConstants.UseColVis] = _model.IsEnableColVis;
            this[FilterConstants.Columns] = _model.Columns
                .Select(c => c.IsSearchable && c.ColumnFilter != null ? c.ColumnFilter : null).ToArray();
            return JsonConvert.SerializeObject(this);
        }
    }
}
