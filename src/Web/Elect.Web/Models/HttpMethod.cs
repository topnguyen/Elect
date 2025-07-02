// ReSharper disable InconsistentNaming
namespace Elect.Web.Models
{
    public enum HttpMethod
    {
        [Display(Name = "GET")]
        GET,
        [Display(Name = "POST")]
        POST,
        [Display(Name = "PUT")]
        PUT,
        [Display(Name = "PATCH")]
        PATCH,
        [Display(Name = "DELETE")]
        DELETE,
        [Display(Name = "OPTIONS")]
        OPTIONS,
        [Display(Name = "HEAD")]
        HEAD,
        [Display(Name = "TRADE")]
        TRADE,
        [Display(Name = "CONNECT")]
        CONNECT
    }
}
