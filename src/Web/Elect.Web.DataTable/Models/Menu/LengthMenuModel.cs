namespace Elect.Web.DataTable.Models.Menu
{
    public class LengthMenuModel : List<Tuple<string, int>>
    {
        public override string ToString()
        {
            return "[[" + string.Join(", ", this.Select(pair => pair.Item2)) + "],[\"" +
                   string.Join("\", \"", this.Select(pair => pair.Item1.Replace("\"", "\"\""))) + "\"]]";
        }
    }
}
