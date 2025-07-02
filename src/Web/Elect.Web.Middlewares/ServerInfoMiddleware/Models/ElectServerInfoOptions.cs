namespace Elect.Web.Middlewares.ServerInfoMiddleware.Models
{
    public class ElectServerInfoOptions : IElectOptions
    {
        /// <summary>
        ///     Default is "cloudflare-nginx". 
        /// </summary>
        public string ServerName { get; set; } = "cloudflare-nginx";
        /// <summary>
        ///     Power by a technology, default is "PHP/5.6.30". 
        /// </summary>
        public string PoweredBy { get; set; } = "PHP/5.6.30";
        /// <summary>
        ///     Author Full Name 
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        ///     Author of Website URL 
        /// </summary>
        public string AuthorWebsite { get; set; }
        /// <summary>
        ///     Author Email 
        /// </summary>
        public string AuthorEmail { get; set; }
    }
}
