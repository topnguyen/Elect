#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ApiGlobalParameterModel.cs </Name>
//         <Created> 18/04/2018 11:30:48 AM </Created>
//         <Key> c14b41ab-08ba-4281-a94c-22b99f2d9997 </Key>
//     </File>
//     <Summary>
//         ApiGlobalParameterModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

namespace Elect.Web.Swagger.Models
{
    public class ApiGlobalParameterModel
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

        public ApiGlobalParameterModel(string name)
        {
            Name = name;
        }
    }
}