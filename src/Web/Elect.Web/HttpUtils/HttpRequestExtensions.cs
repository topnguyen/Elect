#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> this HttpRequestExtensions.cs </Name>
//         <Created> 21/03/2018 6:22:14 PM </Created>
//         <Key> f141f553-1c38-48b6-91da-3b7e7bc89c7a </Key>
//     </File>
//     <Summary>
//         this HttpRequestExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Elect.Web.HttpUtils
{
    public static class HttpRequestExtensions
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
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return HttpRequestHelper.IsAjaxRequest(request);
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
        public static bool IsLocalRequest(this HttpRequest request)
        {
            return HttpRequestHelper.IsLocalRequest(request);
        }

        public static bool IsRequestFor(this HttpRequest request, string endpoint)
        {
            return HttpRequestHelper.IsRequestFor(request, endpoint);
        }

        /// <summary>
        ///     Endpoint of current request combine schema://host with port/path 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetEndpoint(this HttpRequest request)
        {
            return HttpRequestHelper.GetEndpoint(request);
        }

        /// <summary>
        ///     Endpoint of current request domain schema://host with port 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetDomain(this HttpRequest request)
        {
            return HttpRequestHelper.GetDomain(request);
        }

        public static CultureInfo GetCultureInfo(this HttpRequest request)
        {
            return HttpRequestHelper.GetCultureInfo(request);
        }

        public static object GetBody(this HttpRequest request)
        {
            return HttpRequestHelper.GetBody(request);
        }

        public static T GetBody<T>(this HttpRequest request)
        {
            return HttpRequestHelper.GetBody<T>(request);
        }
    }
}