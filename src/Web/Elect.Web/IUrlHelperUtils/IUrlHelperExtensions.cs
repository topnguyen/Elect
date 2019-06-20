﻿#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IUrlHelperExtensions.cs </Name>
//         <Created> 21/03/2018 3:17:54 PM </Created>
//         <Key> 7d492aa2-a5ff-4544-861c-5ecc78bb4cb1 </Key>
//     </File>
//     <Summary>
//         IUrlHelperExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Mvc;
using System;

namespace Elect.Web.IUrlHelperUtils
{
    /// <summary>
    ///     <see cref="IUrlHelper" /> extension methods. 
    /// </summary>
    public static class IUrlHelperExtensions
    {
        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action
        ///     name, controller name and route values.
        /// </summary>
        /// <param name="url">            The URL helper. </param>
        /// <param name="actionName">     The name of the action method. </param>
        /// <param name="controllerName"> The name of the controller. </param>
        /// <param name="routeValues">    The route values. </param>
        /// <returns> The absolute URL. </returns>
        public static string AbsoluteAction(this IUrlHelper url, string actionName, string controllerName, object routeValues = null)
        {
            return url.Action(actionName, controllerName, routeValues, url.ActionContext.HttpContext.Request.Scheme);
        }

        /// <summary>
        ///     Generates a fully qualified URL to the specified content by using the specified
        ///     content path. Converts a virtual (relative) path to an application absolute path.
        /// </summary>
        /// <param name="url">         The URL helper. </param>
        /// <param name="contentPath"> The content path. </param>
        /// <returns> The absolute URL. </returns>
        public static string AbsoluteContent(this IUrlHelper url, string contentPath)
        {
            var request = url.ActionContext.HttpContext.Request;

            return new Uri(new Uri(request.Scheme + "://" + request.Host.Value), url.Content(contentPath)).ToString();
        }

        /// <summary>
        ///     Generates a fully qualified URL to the specified route by using the route name and
        ///     route values.
        /// </summary>
        /// <param name="url">         The URL helper. </param>
        /// <param name="routeName">   Name of the route. </param>
        /// <param name="routeValues"> The route values. </param>
        /// <returns> The absolute URL. </returns>
        public static string AbsoluteRouteUrl(this IUrlHelper url, string routeName, object routeValues = null)
        {
            return url.RouteUrl(routeName, routeValues, url.ActionContext.HttpContext.Request.Scheme);
        }
    }
}