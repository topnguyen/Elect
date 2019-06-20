#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> TransientDependencyAttribute.cs </Name>
//         <Created> 15/03/2018 9:03:18 PM </Created>
//         <Key> 96fdfff8-7982-473f-8a4d-e274ebacccaf </Key>
//     </File>
//     <Summary>
//         TransientDependencyAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.DependencyInjection;

namespace Elect.DI.Attributes
{
    public class TransientDependencyAttribute : DependencyAttribute
    {
        public TransientDependencyAttribute() : base(ServiceLifetime.Transient)
        {
        }
    }
}