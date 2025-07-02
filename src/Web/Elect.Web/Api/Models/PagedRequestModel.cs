namespace Elect.Web.Api.Models
{
    public class PagedRequestModel : ElectDisposableModel
    {
        public virtual int Skip { get; set; } = 0;
        /// <summary>
        ///     Default is 10 item 
        /// </summary>
        public virtual int Take { get; set; } = 10;
        public virtual string ExcludeIds { get; set; }
    }
}
