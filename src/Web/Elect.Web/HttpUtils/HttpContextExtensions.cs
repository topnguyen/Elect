#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2019 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> this HttpContextExtensions.cs </Name>
//         <Created> 03/01/2019 2:01:00 PM </Created>
//         <Key> 0a057f4d-6b2b-4402-bc6f-6ae0d216b2b0 </Key>
//     </File>
//     <Summary>
//         this HttpContextExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License


using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Web.HttpUtils
{
    public static class HttpContextExtensions
    {
        private static readonly RouteData EmptyRouteData = new RouteData();

        private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();

        public static Task WriteAsync<T>(this HttpContext context, T actionResult, CancellationToken cancellationToken = default) where T : IActionResult
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var executor = context.RequestServices.GetService<IActionResultExecutor<T>>();

            if (executor == null)
            {
                throw new InvalidOperationException($"No result executor for '{typeof(T).FullName}' has been registered.");
            }

            var routeData = context.GetRouteData() ?? EmptyRouteData;

            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);

            return cancellationToken.IsCancellationRequested
                ? Task.CompletedTask
                : executor.ExecuteAsync(actionContext, actionResult);
        }
    }
}