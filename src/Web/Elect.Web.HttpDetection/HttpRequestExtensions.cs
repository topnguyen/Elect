#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> HttpRequestExtensions.cs </Name>
//         <Created> 21/03/2018 8:49:03 PM </Created>
//         <Key> 73c2b5db-d782-4006-a08a-7736890ac7db </Key>
//     </File>
//     <Summary>
//         HttpRequestExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Web.HttpDetection.Models;
using Microsoft.AspNetCore.Http;

namespace Elect.Web.HttpDetection
{
    public static class HttpRequestExtensions
    {
        public static DeviceModel GetDeviceInformation(this HttpRequest request)
        {
            return new DeviceModel(request);
        }
    }
}