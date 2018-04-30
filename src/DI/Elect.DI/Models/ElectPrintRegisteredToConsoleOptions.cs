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

using Elect.Core.Interfaces;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;

namespace Elect.DI.Models
{
    public class ElectPrintRegisteredToConsoleOptions : IElectOptions
    {
        /// <summary>
        ///     List assembly name to scan, default is application name. 
        /// </summary>
        public List<string> ListAssemblyName { get; set; } = new List<string> { PlatformServices.Default.Application.ApplicationName };

        /// <summary>
        ///     Print with minimal display format, default is true. 
        /// </summary>
        public bool IsMinimalDisplay { get; set; } = true;

        /// <summary>
        ///     Primary Text Color 
        /// </summary>
        public ConsoleColor PrimaryColor { get; set; } = ConsoleColor.Cyan;

        /// <summary>
        ///     Secondary Text Color 
        /// </summary>
        public ConsoleColor SecondaryColor { get; set; } = ConsoleColor.DarkGray;

        /// <summary>
        ///     Sort Ascending By 
        /// </summary>
        public ElectDIPrintSortBy SortAscBy { get; set; } = ElectDIPrintSortBy.Service;
    }
}