namespace Elect.DI.Attributes
{
    public class ScopedDependencyAttribute : DependencyAttribute
    {
        public ScopedDependencyAttribute() : base(ServiceLifetime.Scoped)
        {
        }
    }
}
