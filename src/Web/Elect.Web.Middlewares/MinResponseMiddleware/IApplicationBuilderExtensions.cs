#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 21/03/2018 8:10:36 PM </Created>
//         <Key> a1e32320-4de5-46fd-81fb-d98fe0139402 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Builder;
using WebMarkupMin.AspNetCore2;

namespace Elect.Web.Middlewares.MinResponseMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseElectMinResponse(this IApplicationBuilder app)
        {
            app.UseWebMarkupMin();

            return app;
        }
    }
}