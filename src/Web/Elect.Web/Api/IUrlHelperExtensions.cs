namespace Elect.Web.Api
{
    public static class IUrlHelperExtensions
    {
        public static PagedMetaModel<TRequest, TResponse> GetPagedMeta<TRequest, TResponse>(
            this IUrlHelper urlHelper,
            TRequest pagedRequestModel,
            PagedResponseModel<TResponse> pagedResponseModel,
            HttpMethod method = HttpMethod.GET)
            where TRequest : PagedRequestModel
            where TResponse : class, new()
        {
            PagedMetaModel<TRequest, TResponse> pagedMetaModel = new PagedMetaModel<TRequest, TResponse>(urlHelper, pagedRequestModel, pagedResponseModel, method);
            return pagedMetaModel;
        }
    }
}
