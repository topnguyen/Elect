#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IUrlHelperExtensions.cs </Name>
//         <Created> 02/04/2018 9:38:42 AM </Created>
//         <Key> 13300ac9-003a-48b8-916d-2553059b8c5d </Key>
//     </File>
//     <Summary>
//         IUrlHelperExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.Api.Models;
using Elect.Web.Models;
using Microsoft.AspNetCore.Mvc;

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