using System;
using Elect.Web.DataTable.Attributes;

namespace Elect.Sample.Web.DataTable.Models
{
    public class UserModel
    {
        [DataTable(IsVisible = false)]
        public int Id { get; set; }

        [DataTable(DisplayName = "Name")]
        public string FullName { get; set; }

        [DataTable(DisplayName = "Created At")]
        public DateTimeOffset CreatedTime { get; set; }

        [DataTable(DisplayName = "Actived")]
        public bool IsActive { get; set; }
    }
}