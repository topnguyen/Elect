#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2019 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> this HttpResponseExtensions.cs </Name>
//         <Created> 08/01/2019 10:47:14 AM </Created>
//         <Key> e8afa8dd-894e-4876-8157-6d03691d9b1d </Key>
//     </File>
//     <Summary>
//         this HttpResponseExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elect.Web.HttpUtils
{
    public static class HttpResponseExtensions
    {
        public static Task WriteAsync<T>(this HttpResponse response, T actionResult, CancellationToken cancellationToken = default) where T : IActionResult
        {
            return response.HttpContext.WriteAsync(actionResult, cancellationToken);
        }
    }
}