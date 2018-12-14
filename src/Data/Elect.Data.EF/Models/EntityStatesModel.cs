using System.Collections.Generic;

namespace Elect.Data.EF.Models
{
    public class EntityStatesModel
    {
        public List<object> ListAdded { get; set; } = new List<object>();
        public List<object> ListModified { get; set; } = new List<object>();
        public List<object> ListDeleted { get; set; } = new List<object>();
    }
}