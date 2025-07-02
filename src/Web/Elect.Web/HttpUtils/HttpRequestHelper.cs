namespace Elect.Web.HttpUtils
{
    public class HttpRequestHelper
    {
        /// <summary>
        ///     Determines whether the specified HTTP request is an AJAX request. 
        /// </summary>
        /// <param name="request"> The HTTP request. </param>
        /// <returns>
        ///     <c> true </c> if the specified HTTP request is an AJAX request; otherwise, <c> false </c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="request" /> parameter is <c> null </c>.
        /// </exception>
        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Headers != null)
            {
                if (request.Headers.TryGetValue(HeaderKey.XRequestedWith, out var value))
                {
                    return string.Equals(value, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
                }
            }
            return false;
        }
        /// <summary>
        ///     Determines whether the specified HTTP request is a local request where the IP address
        ///     of the request originator was 127.0.0.1.
        /// </summary>
        /// <param name="request"> The HTTP request. </param>
        /// <returns>
        ///     <c> true </c> if the specified HTTP request is a local request; otherwise, <c> false </c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="request" /> parameter is <c> null </c>.
        /// </exception>
        public static bool IsLocalRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var connection = request.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                if (connection.LocalIpAddress != null)
                {
                    return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
                }
                return IPAddress.IsLoopback(connection.RemoteIpAddress);
            }
            // for in memory TestServer or when dealing with default connection info
            if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
            {
                return true;
            }
            return false;
        }
        public static bool IsRequestFor(HttpRequest request, string endpoint)
        {
            // get path query with out query param string
            var pathQuery = request.Path.Value?.Trim('/').ToLower() ?? string.Empty;
            // ?
            var iPathQueryWithoutParam = pathQuery.IndexOf('?');
            pathQuery = iPathQueryWithoutParam > 0 ? pathQuery.Substring(iPathQueryWithoutParam) : pathQuery;
            // #
            var iPathQueryWithoutTag = pathQuery.IndexOf('#');
            pathQuery = iPathQueryWithoutTag > 0 ? pathQuery.Substring(iPathQueryWithoutTag) : pathQuery;
            pathQuery = pathQuery.ToLowerInvariant();
            // get endpoint without query param string
            endpoint = endpoint.Trim('/');
            // ?
            var iEndpointWithoutParam = endpoint.IndexOf('?');
            endpoint = iEndpointWithoutParam > 0 ? endpoint.Substring(0, iEndpointWithoutParam) : endpoint;
            endpoint = endpoint.ToLowerInvariant();
            // Compare
            var isRequestTheEndpoint = pathQuery == endpoint;
            return isRequestTheEndpoint;
        }
        /// <summary>
        ///     Endpoint of current request combine schema://host with port/path 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetEndpoint(HttpRequest request)
        {
            return $"{GetDomain(request)}{request.Path.Value}";
        }
        /// <summary>
        ///     Endpoint of current request domain schema://host with port 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetDomain(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host.Value}";
        }
        public static CultureInfo GetCultureInfo(HttpRequest request)
        {
            return request.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
        }
        public static object GetBody(HttpRequest request)
        {
            return GetBody<object>(request);
        }
        public static T GetBody<T>(HttpRequest request)
        {
            try
            {
                T requestBodyObj;
                // Reset Body to Original Position
                request.Body.Position = 0;
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                {
                    string requestBody = reader.ReadToEnd();
                    // Reformat to have beautiful json string
                    requestBodyObj = JsonConvert.DeserializeObject<T>(requestBody);
                }
                // Reset Body to Original Position
                request.Body.Position = 0;
                return requestBodyObj;
            }
            catch (Exception)
            {
                return default;
            }
        }
        public static string GetIpAddress(HttpRequest request)
        {
            var ipAddress = string.Empty;
            // Priority to Proxy Server
            if (request.Headers.TryGetValue(HeaderKey.CFConnectingIP, out var cloudFareConnectingIp))
            {
                ipAddress = cloudFareConnectingIp;
                return ipAddress;
            }
            if (request.Headers.TryGetValue(HeaderKey.CFTrueClientIP, out var cloudFareTrueClientIp))
            {
                ipAddress = cloudFareTrueClientIp;
                return ipAddress;
            }
            // Look for the X-Forwarded-For (XFF) HTTP header field it's used for identifying the
            // originating IP address of a client connecting to a web server through an HTTP proxy or
            // load balancer.
            string xff = request.Headers?
                .Where(x => HeaderKey.XForwardedFor.Equals(x.Key, StringComparison.OrdinalIgnoreCase))
                .Select(k => request.Headers[k.Key]).FirstOrDefault();
            // If you want to exclude private IP addresses, then see http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
            if (!string.IsNullOrWhiteSpace(xff))
            {
                var lastIp = xff.Split(',').FirstOrDefault();
                ipAddress = lastIp;
            }
            if (string.IsNullOrWhiteSpace(ipAddress) || ipAddress == "::1" || ipAddress == "127.0.0.1")
            {
                ipAddress = request.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return null;
            }
            // Standardize
            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1";
            }
            // Remove port
            int index = ipAddress.IndexOf(":", StringComparison.OrdinalIgnoreCase);
            if (index > 0)
            {
                ipAddress = ipAddress.Substring(0, index);
            }
            return ipAddress;
        }
    }
}
