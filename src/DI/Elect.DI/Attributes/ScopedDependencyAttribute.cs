#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ScopedDependencyAttribute.cs </Name>
//         <Created> 15/03/2018 9:01:35 PM </Created>
//         <Key> ff972aff-d242-4be5-a173-4b93dea5004c </Key>
//     </File>
//     <Summary>
//         ScopedDependencyAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.DependencyInjection;

namespace Elect.DI.Attributes
{
    public class ScopedDependencyAttribute : DependencyAttribute
    {
        public ScopedDependencyAttribute() : base(ServiceLifetime.Scoped)
        {
        }
    }
}