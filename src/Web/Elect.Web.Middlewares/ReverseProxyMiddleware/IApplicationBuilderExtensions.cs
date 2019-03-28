#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 28/03/2019 10:54:00 AM </Created>
//         <Key> 35f29248-f5c2-4f62-a326-71ffb7cee679 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Elect.Web.Middlewares.ReverseProxyMiddleware.Models;
using Elect.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Elect.Web.Middlewares.ReverseProxyMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectReserveProxy(this IApplicationBuilder app)
        {
            app.UseMiddleware<ReverseProxyMiddleware>();

            return app;
        }
    }

    public class ReverseProxyMiddleware
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        private readonly RequestDelegate _nextMiddleware;
        private readonly ElectReserveProxyOptions _option;

        public ReverseProxyMiddleware(RequestDelegate nextMiddleware, IOptions<ElectReserveProxyOptions> configuration)
        {
            _nextMiddleware = nextMiddleware;
            _option = configuration.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            // Execute Before Reserve Proxy if have

            if (_option.BeforeReserveProxy != null)
            {
                var isContinueReserveProxy = _option.BeforeReserveProxy.Invoke(context);

                if (!isContinueReserveProxy)
                {
                    return;
                }
            }
            
            var targetUri = BuildTargetUri(context.Request);

            if (_option.IsEnableTargetUrlConsoleLog)
            {
                Console.WriteLine($"[Elect Reserve Proxy] Target URL: {targetUri}");
            }

            if (targetUri != null)
            {
                var targetRequestMessage = CreateTargetMessage(context, targetUri);

                using (var responseMessage = await HttpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted).ConfigureAwait(true))
                {
                    context.Response.StatusCode = (int) responseMessage.StatusCode;

                    CopyFromTargetResponseHeaders(context, responseMessage);

                    await responseMessage.Content.CopyToAsync(context.Response.Body).ConfigureAwait(true);
                }
            }
            else
            {
                await _nextMiddleware(context).ConfigureAwait(true);
            }
            
            // Execute After Reserve Proxy if have
            _option.AfterReserveProxy?.Invoke(context);
        }

        private static HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();

            CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;

            requestMessage.Headers.Host = targetUri.Host;

            requestMessage.Method = GetMethod(context.Request.Method);

            return requestMessage;
        }

        private static void CopyFromOriginalRequestContentAndHeaders(HttpContext context,
            HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) &&
                !HttpMethods.IsHead(requestMethod) &&
                !HttpMethods.IsDelete(requestMethod) &&
                !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);

                requestMessage.Content = streamContent;
            }

            foreach (var header in context.Request.Headers)
            {
                requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }

        private static void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in responseMessage.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            context.Response.Headers.Remove(HeaderKey.TransferEncoding);
        }

        private static HttpMethod GetMethod(string method)
        {
            if (HttpMethods.IsDelete(method))
            {
                return HttpMethod.Delete;
            }

            if (HttpMethods.IsGet(method))
            {
                return HttpMethod.Get;
            }

            if (HttpMethods.IsHead(method))
            {
                return HttpMethod.Head;
            }

            if (HttpMethods.IsOptions(method))
            {
                return HttpMethod.Options;
            }

            if (HttpMethods.IsPost(method))
            {
                return HttpMethod.Post;
            }

            if (HttpMethods.IsPut(method))
            {
                return HttpMethod.Put;
            }

            return HttpMethods.IsTrace(method) ? HttpMethod.Trace : new HttpMethod(method);
        }

        private Uri BuildTargetUri(HttpRequest request)
        {
            var serviceRootEndpoint = _option.ServiceRootUrl.Trim('/');

            var serviceEndpoint = $"{serviceRootEndpoint}/{request.Path.Value?.Trim('/')}".Trim('/');

            if (!string.IsNullOrWhiteSpace(request.QueryString.Value))
            {
                serviceEndpoint += $"?{request.QueryString.Value.Trim('?')}".Trim('/');
            }

            var targetUri = new Uri(serviceEndpoint);

            return targetUri;
        }
    }
}