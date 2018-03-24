#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HideInApiDocAttribute.cs </Name>
//         <Created> 24/03/2018 11:47:03 PM </Created>
//         <Key> 3121f335-b605-4024-89a3-ed0704181cb1 </Key>
//     </File>
//     <Summary>
//         HideInApiDocAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Web.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class HideInApiDocAttribute : Attribute
    {
    }
}