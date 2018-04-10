#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ApiDocGroupAttribute.cs </Name>
//         <Created> 10/04/2018 4:04:03 PM </Created>
//         <Key> a2fc9e80-7686-4aa0-ad2a-f845cee797d0 </Key>
//     </File>
//     <Summary>
//         ApiDocGroupAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Web.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ApiDocGroupAttribute : Attribute
    {
        public string GroupName { get; }

        public ApiDocGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}