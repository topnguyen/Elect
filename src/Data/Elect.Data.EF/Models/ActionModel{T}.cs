using System;

namespace Elect.Data.EF.Models
{
    public class ActionModel<T>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        
        public Action<T> Action { get; set; }

        public ActionModel(Action<T> action)
        {
            Action = action;
        }
    }
}