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

        /// <summary>
        ///     In is a place the parameter will pass to server, in formData by default
        /// </summary>
        /// <remarks>You can use <see cref="ParameterIn"/> to prevent spell issue</remarks>
        public string In { get; set; } = ParameterIn.FormData;

        /// <summary>
        ///     Type can be "string", "file", "number" and so on, see more at https://swagger.io/docs/specification/describing-parameters/
        /// </summary>
        /// <remarks>You can use <see cref="ParameterType"/> to prevent spell issue</remarks>
        public string Type { get; set; } = ParameterType.File;

        public bool IsRequire { get; set; }

        public string Description { get; set; }

        public ApiParameterAttribute(string name)
        {
            Name = name;
        }
    }
}