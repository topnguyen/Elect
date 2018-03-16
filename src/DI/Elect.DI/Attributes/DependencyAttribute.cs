#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DependencyAttribute.cs </Name>
//         <Created> 15/03/2018 9:00:07 PM </Created>
//         <Key> daad6b2f-c1a3-4425-a714-9cee9757fa85 </Key>
//     </File>
//     <Summary>
//         DependencyAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

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