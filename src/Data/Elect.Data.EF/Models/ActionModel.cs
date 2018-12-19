using System;

namespace Elect.Data.EF.Models
{
    public class ActionModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public Action Action { get; set; }

        public ActionModel(Action action)
        {
            Action = action;
        }
    }
}