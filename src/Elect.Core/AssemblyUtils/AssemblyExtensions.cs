namespace Elect.Core.AssemblyUtils
{
    public static class AssemblyExtensions
    {
        public static string GetDirectoryPath(this Assembly assembly)
        {
            return AssemblyHelper.GetDirectoryPath(assembly);
        }
    }
}
