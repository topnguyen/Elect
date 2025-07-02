namespace Elect.Core.ActionUtils.Models
{
    public class ActionModel : ElectDisposableModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public Action Action { get; set; }
        public ActionModel(Action action)
        {
            Action = action;
        }
    }
}
