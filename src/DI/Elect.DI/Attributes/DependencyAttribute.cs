namespace Elect.DI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class DependencyAttribute : Attribute
    {
        protected DependencyAttribute(ServiceLifetime dependencyType)
        {
            DependencyType = dependencyType;
        }
        public ServiceLifetime DependencyType { get; }
        public Type ServiceType { get; set; }
        public ServiceDescriptor BuildServiceDescriptor(TypeInfo type)
        {
            var serviceType = ServiceType ?? type.AsType();
            return new ServiceDescriptor(serviceType, type.AsType(), DependencyType);
        }
    }
}
