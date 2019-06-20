#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> SiteMapValidator.cs </Name>
//         <Created> 21/03/2018 2:45:19 PM </Created>
//         <Key> eb509905-3dc9-4646-9df6-62c9ee43caa1 </Key>
//     </File>
//     <Summary>
//         SiteMapValidator.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace Elect.Web.SiteMap.Services
{
    public class SiteMapValidator
    {
        /// <summary>
        ///     The maximum number of site map a site map index file can contain. 
        /// </summary>
        public const int MaximumSiteMapCount = 50000;

        /// <summary>
        ///     The maximum number of site maps nodes allowed in a site maps file. The absolute
        ///     maximum allowed is 50,000 according to the specification. See
        ///     http://www.sitemaps.org/protocol.html but the file size must also be less than 10MB.
        ///     After some experimentation, a maximum of 25,000 nodes keeps the file size below 10MB.
        /// </summary>
        public const int MaximumSiteMapIndexCount = 25000;

        /// <summary>
        ///     The maximum size of a site maps file in bytes (10MB). 
        /// </summary>
        public const int MaximumSiteMapSizeInBytes = 10485760;

        /// <summary>
        ///     Checks the size of the XML site maps document. If it is over 10MB, logs an error. 
        /// </summary>
        /// <param name="siteMapXml"> The site maps XML document. </param>
        public static void CheckDocumentSize(string siteMapXml)
        {
            if (siteMapXml.Length >= MaximumSiteMapSizeInBytes)
            {
                throw new NotSupportedException($"SiteMap exceeds the maximum size of 10MB. This is because you have unusually long URL's. Size:<{siteMapXml.Length}>.");
            }
        }

        /// <summary>
        ///     Checks the count of the number of site maps. If it is over 50,000, logs an error. 
        /// </summary>
        /// <param name="siteMapCount"> The site maps count. </param>
        public static void CheckSiteMapCount(int siteMapCount)
        {
            if (siteMapCount > MaximumSiteMapCount)
            {
                throw new NotSupportedException($"SiteMap index file exceeds the maximum number of allowed sitemaps of 50,000. Count:<{siteMapCount}>");
            }
        }
    }
}