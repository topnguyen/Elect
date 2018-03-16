#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SingletonDependencyAttribute.cs </Name>
//         <Created> 15/03/2018 9:03:45 PM </Created>
//         <Key> 2e2f58d7-e32d-485a-adb1-2f366545f9a0 </Key>
//     </File>
//     <Summary>
//         SingletonDependencyAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.DependencyInjection;

namespace Elect.DI.Attributes
{
    public class SingletonDependencyAttribute : DependencyAttribute
    {
        public SingletonDependencyAttribute() : base(ServiceLifetime.Singleton)
        {
        }
    }
}