using Elect.Web.DataTable.Attributes;
using System;
namespace Elect.Sample.Web.DataTable.Models
{
    public class UserModel: IUserData
    {
        [DataTable(IsVisible = true, Order = 1)]
        public int Id { get; set; }
        
        public string FullName { get; set; }
        
        [DataTable(DisplayName = "Created At", Order = 2)]
        public DateTimeOffset CreatedTime { get; set; }

        [DataTable(DisplayName = "Actived", Order = 4)]
        public bool IsActive { get; set; }
    }

    public interface IUserData
    {
        [DataTable(IsVisible = false, Order = 1)]
        public int Id { get; set; }

        [DataTableIgnore]
        [DataTable(DisplayName = "Name", Order = 3)]
        public string FullName { get; set; }
        
        public bool IsActive { get; set; }
    }
}