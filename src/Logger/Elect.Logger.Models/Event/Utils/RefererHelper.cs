using System;
using Elect.Web.Models;
using Microsoft.AspNetCore.Http;

namespace Elect.Logger.Models.Event.Utils
{
    internal static class RefererHelper
    {
        public static RefererModel Get(HttpRequest httpRequest)
        {
            var refererUrl = httpRequest.Headers[HeaderKey.Referer];

            if (string.IsNullOrWhiteSpace(refererUrl))
            {
                return null;
            }

            var referer = new RefererModel
            {
                Url = refererUrl
            };

            if (!string.IsNullOrWhiteSpace(referer.Url))
            {
                referer.Domain = new Uri(referer.Url).Host;
            }

            return referer;
        }
    }
}