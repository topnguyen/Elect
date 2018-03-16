#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectDIOptions.cs </Name>
//         <Created> 16/03/2018 1:51:16 PM </Created>
//         <Key> e4a80a81-561e-459f-874c-c80030bb30cb </Key>
//     </File>
//     <Summary>
//         ElectDIOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Elect.DI.Options
{
    public class ElectDIOptions
    {
        /// <summary>
        ///     Assembly name of system to scan and register to services. 
        /// </summary>
        public string AssemblyName { get; set; } = PlatformServices.Default.Application.ApplicationName;

        /// <summary>
        ///     Assembly folder path, default is application base path. 
        /// </summary>
        public string AssemblyFolderPath { get; set; } = Path.GetFullPath(PlatformServices.Default.Application.ApplicationBasePath);
    }
}