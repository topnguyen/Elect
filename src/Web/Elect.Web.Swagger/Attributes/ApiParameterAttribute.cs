#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ApiParameterAttribute.cs </Name>
//         <Created> 18/04/2018 9:10:21 AM </Created>
//         <Key> 122a9678-126a-4990-a19d-01322a625b3e </Key>
//     </File>
//     <Summary>
//         ApiParameterAttribute.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.Swagger.Models;
using System;

namespace Elect.Web.Swagger.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApiParameterAttribute : Attribute
    {
        public string Name { get; }

        public ParameterInType In { get; set; } = ParameterInType.Query;

        public string Type { get; set; } = "string";

        public bool IsRequire { get; set; }

        public string Description { get; set; }

        public ApiParameterAttribute(string name)
        {
            Name = name;
        }
    }
}