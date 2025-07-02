namespace Elect.Web.Swagger.Models
{
    public class ElectSwaggerOptions
    {
        public bool IsEnable { get; set; } = true;
        public bool SerializeAsV2 { get; set; } = true;
        private string _swaggerRoutePrefix = "developers/api-docs";
        /// <summary>
        ///     Swagger Endpoint, default is "/developers/api-docs". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string SwaggerRoutePrefix
        {
            get => _swaggerRoutePrefix.Trim('~').Trim('/');
            set => _swaggerRoutePrefix = value;
        }
        /// <summary>
        ///     Swagger name, default is "all" 
        /// </summary>
        public string SwaggerName { get; set; } = "all";
        /// <summary>
        ///     Api Document Endpoint, default is "/developers". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string Url { get; set; } = "/developers";
        /// <summary>
        ///     Json Viewer Endpoint, Default is "/developers/json-viewer". 
        /// </summary>
        /// <remarks> Must start with "/" </remarks>
        public string JsonViewerUrl { get; set; } = "/developers/json-viewer";
        /// <summary>
        ///     Api Document Title. Default is "API Document" 
        /// </summary>
        public string Title { get; set; } = "API Document";
        /// <summary>
        ///     Api Document Version. Ex: latest, v1, v2 and so on. Default is "latest" 
        /// </summary>
        public string Version { get; set; } = "latest";
        /// <summary>
        ///     Access Key via uri param "key", default is "" - allow anonymous. 
        /// </summary>
        public string AccessKey { get; set; } = string.Empty;
        /// <summary>
        ///     Un-authorize message when user access api document with not correct key. Default is
        ///     "You don't have permission to view API Document, please contact your administrator."
        /// </summary>
        public string UnAuthorizeMessage { get; set; } = "You don't have permission to view API Document, please contact your administrator.";
        /// <summary>
        ///     Authenticate Token Type, default is "Bearer". 
        /// </summary>
        public string AuthTokenType { get; set; } = "Bearer";
        /// <summary>
        ///     Show full schema for each type in Document 
        /// </summary>
        public bool IsFullSchemaForType { get; set; } = true;
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorWebsite { get; set; }
        public List<OpenApiParameter> GlobalParameters { get; set; } = new List<OpenApiParameter>();
        /// <summary>
        ///     Additional Options if you want to add your customize (Operation Filter, Document
        ///     Filter and so on) after Elect add Swagger Options.
        /// </summary>
        public Action<SwaggerGenOptions, ElectSwaggerOptions> ExtendOptions { get; set; }
    }
}
