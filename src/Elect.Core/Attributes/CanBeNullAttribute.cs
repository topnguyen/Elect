namespace Elect.Core.Attributes
{
    /// <summary>
    ///     Indicates that the value of the marked element could be <c> null </c> sometimes, so the
    ///     check for <c> null </c> is necessary before its usage
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
    public sealed class CanBeNullAttribute : Attribute { }
}
