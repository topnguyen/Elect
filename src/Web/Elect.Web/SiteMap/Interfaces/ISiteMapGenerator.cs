#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ISiteMapGenerator.cs </Name>
//         <Created> 21/03/2018 2:22:13 PM </Created>
//         <Key> 13c27e44-e3d1-4e9e-b38c-876d3760d8c1 </Key>
//     </File>
//     <Summary>
//         ISiteMapGenerator.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.AspNetCore.Mvc;

namespace Elect.Web.SiteMap.Interfaces
{
    public interface ISiteMapGenerator<in T> where T : class, ISiteMapItem
    {
        string GenerateXmlString(params T[] items);

        ContentResult GenerateContentResult(params T[] items);
    }
}