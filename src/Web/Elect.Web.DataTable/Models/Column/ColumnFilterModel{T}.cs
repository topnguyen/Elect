#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ColumnFilterModel_T_.cs </Name>
//         <Created> 23/03/2018 4:32:28 PM </Created>
//         <Key> 0d07ef53-5bf6-4de9-a84b-6ad004a84493 </Key>
//     </File>
//     <Summary>
//         ColumnFilterModel_T_.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.DataTable.Models.Constants;
using Elect.Web.DataTable.Utils.EnumUtils;
using System.Linq;
using System.Reflection;

namespace Elect.Web.DataTable.Models.Column
{
    public class ColumnFilterModel<TTarget>
    {
        private readonly TTarget _target;

        private readonly ColumnModel _columnModel;

        public ColumnFilterModel(TTarget target, ColumnModel columnModel)
        {
            _target = target;
            _columnModel = columnModel;
        }

        public TTarget Text()
        {
            _columnModel.ColumnFilter.FilterType = FilterConstants.Text;
            return _target;
        }

        public TTarget CheckBoxes(params string[] options)
        {
            _columnModel.ColumnFilter.FilterType = FilterConstants.Checkbox;
            _columnModel.ColumnFilter.FilterValues = options.Cast<object>().ToArray();
            if (_columnModel.Type.GetTypeInfo().IsEnum)
            {
                _columnModel.ColumnFilter.FilterValues = _columnModel.Type.GetEnumValueLabelPair().Select(x => new
                {
                    value = string.IsNullOrWhiteSpace(x.Value) ? DataConstants.Null : x.Value,
                    label = x.Label
                }).ToArray<object>();
            }
            return _target;
        }

        public TTarget Select(params string[] options)
        {
            _columnModel.ColumnFilter.FilterType = FilterConstants.Select;
            _columnModel.ColumnFilter.FilterValues = options.Cast<object>().ToArray();
            if (_columnModel.Type.GetTypeInfo().IsEnum)
            {
                _columnModel.ColumnFilter.FilterValues = _columnModel.Type.GetEnumValueLabelPair().Select(x => new
                {
                    value = string.IsNullOrWhiteSpace(x.Value) ? DataConstants.Null : x.Value,
                    label = x.Label
                }).ToArray<object>();
            }
            return _target;
        }

        public TTarget Number()
        {
            _columnModel.ColumnFilter.FilterType = FilterConstants.Number;
            return _target;
        }

        public TTarget NumberRange()
        {
            _columnModel.ColumnFilter.FilterType = FilterConstants.NumberRange;
            return _target;
        }

        public TTarget DateRange()
        {
            _columnModel.ColumnFilter.FilterType = FilterConstants.DateRange;
            return _target;
        }

        public TTarget None()
        {
            _columnModel.ColumnFilter = null;
            return _target;
        }
    }
}