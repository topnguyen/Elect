#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectSwaggerConstants.cs </Name>
//         <Created> 17/04/2018 2:54:54 PM </Created>
//         <Key> 76e79d84-e09b-454d-976a-cad297fbf9b9 </Key>
//     </File>
//     <Summary>
//         ElectSwaggerConstants.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Elect.Data.IO.DirectoryUtils;
using System.IO;
using System.Reflection;

namespace Elect.Web.Swagger.Models
{
    public class ElectSwaggerConstants
    {
        internal const string AssetsUrl = "/developers/assets";

        internal const string AccessKeyName = "key";

        internal const string CookieAccessKeyName = "Elect_Swagger_AccessKey";

        internal const string AssetFolderName = "Elect_Swagger";

        internal static readonly string IndexFileFullPath = $"{AssetFolderName}/index.html";

        internal static readonly string JsonViewerFileFullPath = $"{AssetFolderName}/json-viewer.html";

        public static string NugetPackageFolderPath
        {
            get
            {
                var currentWindowUser = DirectoryHelper.SpecialFolder.GetCurrentWindowUserFolder();

                string nugetPackage = Path.Combine(currentWindowUser, $@".nuget\packages\{Assembly.GetExecutingAssembly().GetName()}");

                return nugetPackage;
            }
        }
    }
}