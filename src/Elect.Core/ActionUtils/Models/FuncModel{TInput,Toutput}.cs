namespace Elect.Core.ActionUtils.Models
{
    public class FuncModel<TInput, TOutput> : ElectDisposableModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public Func<TInput, TOutput> Func { get; set; }
        public FuncModel(Func<TInput, TOutput> func)
        {
            Func = func;
        }
    }
}
