#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> AssemblyExtensions.cs </Name>
//         <Created> 15/03/2018 8:42:11 PM </Created>
//         <Key> 644bf06e-71e2-46c7-8bd3-e86c17f3736e </Key>
//     </File>
//     <Summary>
//         AssemblyExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.Reflection;

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