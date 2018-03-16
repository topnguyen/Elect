#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectPrintRegisteredToConsoleOptions.cs </Name>
//         <Created> 16/03/2018 2:18:42 PM </Created>
//         <Key> 2b560738-5e88-4a19-88ea-ce354b07c32d </Key>
//     </File>
//     <Summary>
//         ElectPrintRegisteredToConsoleOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Microsoft.Extensions.PlatformAbstractions;

namespace Elect.DI.Options
{
    public class ElectPrintRegisteredToConsoleOptions
    {
        /// <summary>
        ///     Assembly name of system to scan and register to services. 
        /// </summary>
        public string AssemblyName { get; set; } = PlatformServices.Default.Application.ApplicationName;
    }
}