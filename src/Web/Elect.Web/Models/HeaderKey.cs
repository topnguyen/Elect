namespace Elect.Web.Models
{
    public static class HeaderKey
    {
        #region Request
        public const string Accept = "Accept";
        public const string AcceptCharset = "Accept-Charset";
        public const string AcceptEncoding = "Accept-Encoding";
        public const string AcceptLanguage = "Accept-Language";
        public const string AcceptDatetime = "Accept-Datetime";
        public const string AccessControlRequestMethod = "AcceptAccess-Control-Request-Method";
        public const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        public const string Authorization = "Authorization";
        public const string CacheControl = "Cache-Control";
        public const string Connection = "Connection";
        public const string Cookie = "Cookie";
        public const string ContentLength = "Content-Length";
        public const string ContentMd5 = "Content-MD5";
        public const string ContentType = "Content-Type";
        public const string Date = "Date";
        public const string Expect = "Expect";
        public const string Forwarded = "Forwarded";
        public const string From = "From";
        public const string IfMatch = "If-Match";
        public const string IfModifiedSince = "If-Modified-Since";
        public const string IfNoneMatch = "If-None-Match";
        public const string IfRange = "If-Range";
        public const string IfUnmodifiedSince = "If-Unmodified-Since";
        public const string MaxForwards = "Max-Forwards";
        public const string Origin = "Origin";
        public const string Pragma = "Pragma";
        public const string ProxyAuthorization = "Proxy-Authorization";
        public const string Range = "Range";
        public const string Referer = "Referer";
        public const string Te = "TE";
        public const string UserAgent = "User-Agent";
        public const string Upgrade = "Upgrade";
        public const string Via = "Via";
        public const string Warning = "Warning";
        // Non Standard
        public const string XRequestedWith = "X-Requested-With";
        public const string XCsrfToken = "X-Csrf-Token";
        public const string XRequestId = "XCsrfToken";
        public const string XCorrelationId = "X-Correlation-ID";
        public const string XForwardedFor = "X-Forwarded-For";
        /// <summary>
        ///     Rename the Anti-Forgery HTTP header from RequestVerificationToken to X-XSRF-TOKEN.
        ///     X-XSRF-TOKEN is not a standard but a common name given to this HTTP header
        ///     popularized by Angular.
        /// </summary>
        public const string XAntiforgeryToken = "X-XSRF-TOKEN";
        // Cloudfare
        // Seemore: https://support.cloudflare.com/hc/en-us/articles/200170986-How-does-Cloudflare-handle-HTTP-Request-headers-
        public const string CFIPCountry = "CF-IPCountry";
        public const string CFConnectingIP = "CF-Connecting-IP";
        public const string CFTrueClientIP = "True-Client-IP";
        public const string CFVisitor = "CF-Visitor";
        public const string CFRAY = "CF-RAY";
        public const string CFXForwardedProto = "X-Forwarded-Proto";
        #endregion
        #region Response
        public const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        public const string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";
        public const string AccessControlExposeHeaders = "Access-Control-Expose-Headers";
        public const string AccessControlMaxAge = "Access-Control-Max-Age";
        public const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        public const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
        public const string AcceptPatch = "Accept-Patch";
        public const string AcceptRanges = "Accept-Ranges";
        public const string Age = "Age";
        public const string Allow = "Allow";
        public const string ContentDisposition = "Content-Disposition";
        public const string ContentEncoding = "Content-Encoding";
        public const string ContentLanguage = "Content-Language";
        public const string ContentLocation = "Content-Location";
        public const string ContentMD5 = "Content-MD5";
        public const string ContentRange = "Content-Range";
        public const string ETag = "ETag";
        public const string Expires = "Expires";
        public const string LastModified = "Last-Modified";
        public const string Location = "Location";
        public const string Server = "Server";
        public const string SetCookie = "Set-Cookie";
        public const string Trailer = "Trailer";
        public const string TransferEncoding = "Transfer-Encoding";
        public const string Vary = "Vary";
        // Non Standard
        public const string XFrameOptions = "X-Frame-Options";
        public const string XPoweredBy = "X-Powered-By";
        public const string XContentDuration = "X-Content-Duration";
        public const string XProcessingTimeMilliseconds = "X-Processing-Time-Milliseconds";
        public const string XuaCompatible = "X-UA-Compatible";
        public const string XxssProtection = "X-XSS-Protection";
        // Author
        public const string XAuthorWebsite = "X-Author-Website";
        public const string XAuthorEmail = "X-Author-Email";
        public const string XAuthorName = "X-Author-Name";
        #endregion
    }
}
