#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectHttpContext.cs </Name>
//         <Created> 01/04/2018 11:31:22 PM </Created>
//         <Key> 15b18c28-0c1b-461f-9032-8d33d87ae0cb </Key>
//     </File>
//     <Summary>
//         ElectHttpContext.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Web.Middlewares.HttpContextMiddleware
{
    public static class ElectHttpContext
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor _contextAccessor;

        /// <summary>
        ///     Get current request HttpContext 
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor?.HttpContext;

        internal static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}