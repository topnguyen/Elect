namespace Elect.Web.Swagger
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectSwagger(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<ElectSwaggerOptions>>().Value;
            if (!options.IsEnable)
            {
                return app;
            }
            app.UseSwagger(c =>
            {
                c.RouteTemplate = options.SwaggerRoutePrefix + "/{documentName}/" + options.SwaggerName;
                c.SerializeAsV2 = options.SerializeAsV2;
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    var host =  httpReq.Host.Host;
                    if (httpReq.Headers.TryGetValue("X-Forwarded-Host", out var forwardedHost))
                    {
                        host = forwardedHost;
                    }
                    var port =  httpReq.Host.Port;
                    if (httpReq.Headers.TryGetValue("X-Forwarded-Port", out var forwardedPort))
                    {
                        if (int.TryParse(forwardedPort, out var forwardedPortInt))
                        {
                            port = forwardedPortInt;
                        }
                    }
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer
                        {
                            Url = $"{httpReq.Scheme}://{host}{(port.HasValue ? $":{port.Value}" : string.Empty)}"
                        }
                    };
                });
            });
            app.UseSwaggerUI(c =>
            {
                // RoutePrefix of Swagger UI must not start by / or ~
                c.RoutePrefix = options.SwaggerRoutePrefix;
                c.SwaggerEndpoint(SwaggerHelper.GetSwaggerEndpoint(options), options.AccessKey);
            });
            // Path and GZip for Statics Content
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Bootstrapper.Instance.WorkingFolder),
                RequestPath = ElectSwaggerConstants.AssetsUrl,
                OnPrepareResponse = context =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        MaxAge = new TimeSpan(365, 0, 0, 0)
                    };
                }
            });
            app.UseMiddleware<ElectSwaggerMiddleware>();
            return app;
        }
        /// <summary>
        ///     Keep swagger access middleware before UseSwagger and UseSwaggerUI to wrap a request 
        /// </summary>
        public class ElectSwaggerMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly ElectSwaggerOptions _options;
            public ElectSwaggerMiddleware(RequestDelegate next, IOptions<ElectSwaggerOptions> configuration)
            {
                _next = next;
                _options = configuration.Value;
            }
            public async Task Invoke(HttpContext context)
            {
                // Check is request to Swagger
                if (!SwaggerHelper.IsAccessSwagger(context, _options))
                {
                    await _next.Invoke(context).ConfigureAwait(true);
                    return;
                }
                // Set cookie if need
                string accessKey = context.Request.Query[ElectSwaggerConstants.AccessKeyName];
                if (!string.IsNullOrWhiteSpace(accessKey) && context.Request.Cookies[ElectSwaggerConstants.AccessKeyName] != accessKey)
                {
                    context.Response.Cookies.Append(ElectSwaggerConstants.CookieAccessKeyName, accessKey, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false // allow transmit via http and https
                    });
                }
                // Check Permission
                bool isCanAccess = SwaggerHelper.IsCanAccessSwagger(context, _options.AccessKey);
                if (!isCanAccess)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync(_options.UnAuthorizeMessage).ConfigureAwait(true);
                    return;
                }
                if (SwaggerHelper.IsAccessJsonViewer(context, _options))
                {
                    var jsonViewerContentResult = SwaggerHelper.GetApiJsonViewerHtml();
                    context.Response.ContentType = jsonViewerContentResult.ContentType;
                    context.Response.StatusCode = jsonViewerContentResult.StatusCode ?? StatusCodes.Status200OK;
                    await context.Response.WriteAsync(jsonViewerContentResult.Content, Encoding.UTF8).ConfigureAwait(true);
                    return;
                }
                if (SwaggerHelper.IsAccessUI(context, _options))
                {
                    var apiDocContentResult = SwaggerHelper.GetApiDocHtml();
                    context.Response.ContentType = apiDocContentResult.ContentType;
                    context.Response.StatusCode = apiDocContentResult.StatusCode ?? StatusCodes.Status200OK;
                    await context.Response.WriteAsync(apiDocContentResult.Content, Encoding.UTF8).ConfigureAwait(true);
                    return;
                }
                // Next middleware is swagger endpoint => generate document by swagger
                await _next.Invoke(context).ConfigureAwait(true);
            }
        }
    }
}
