namespace Elect.DI.Attributes
{
    public class SingletonDependencyAttribute : DependencyAttribute
    {
        public SingletonDependencyAttribute() : base(ServiceLifetime.Singleton)
        {
        }
    }
}
