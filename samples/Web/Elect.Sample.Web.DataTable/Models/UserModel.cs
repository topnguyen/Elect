using Elect.Web.DataTable.Attributes;
using System;
using Elect.Web.DataTable.Models.Constants;

namespace Elect.Sample.Web.DataTable.Models
{
    public class UserModel
    {
        [DataTable(IsVisible = false, Order = 1)]
        public int Id { get; set; }

        [DataTableFilter(FilterType.Text, Selector = "filter-col-DisplayName")]
        [DataTable(DisplayName = "Name", Order = 3)]
        public string FullName { get; set; }

        [DataTable(DisplayName = "Created At", Order = 2)]
        public DateTimeOffset CreatedTime { get; set; }

        [DataTable(DisplayName = "Actived", Order = 4)]
        public bool IsActive { get; set; }
    }
}