namespace Elect.Web.DataTable.Models.Column
{
    public class ColumnFilterModel : Hashtable
    {
        private static readonly List<Type> DateTypes = new List<Type>
        {
            typeof(DateTime),
            typeof(DateTime?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?)
        };
        public ColumnFilterModel(Type t)
        {
            SetDefaultValuesAccordingToColumnType(t);
        }
        internal object[] FilterValues
        {
            set => this[FilterConstants.Values] = value;
        }
        internal string FilterType
        {
            set => this[FilterConstants.Type] = value;
        }
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
                        label = ElectDataTableTranslator.Get(ElectDataTableOptions.Instance.DefaultDisplayText.Yes)
                    },
                    new
                    {
                        value = DataConstants.False,
                        label = ElectDataTableTranslator.Get(ElectDataTableOptions.Instance.DefaultDisplayText.No)
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
