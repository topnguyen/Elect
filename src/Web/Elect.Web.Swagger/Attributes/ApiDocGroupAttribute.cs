namespace Elect.Web.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ApiDocGroupAttribute : Attribute
    {
        public string GroupName { get; }
        public ApiDocGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}
