#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ShowInApiDocAttribute.cs </Name>
//         <Created> 01/04/2018 11:36:03 PM </Created>
//         <Key> 8f8e719a-91f9-4944-a779-0d8adccb0f46 </Key>
//     </File>
//     <Summary>
//         ShowInApiDocAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Web.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ShowInApiDocAttribute : Attribute
    {
    }
}