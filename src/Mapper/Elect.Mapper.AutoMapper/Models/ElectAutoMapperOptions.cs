#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> ElectAutoMapperOptions.cs </Name>
//         <Created> 16/03/2018 11:11:54 PM </Created>
//         <Key> fe472d2b-f305-48b8-afca-ff6b5b48e81f </Key>
//     </File>
//     <Summary>
//         ElectAutoMapperOptions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using AutoMapper;
using Elect.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;

namespace Elect.Mapper.AutoMapper.Models
{
    public class ElectAutoMapperOptions : IElectOptions
    {
        /// <summary>
        ///     List assembly folder to scan, default is application base path. 
        /// </summary>
        public List<string> ListAssemblyFolderPath { get; set; } = new List<string> { PlatformServices.Default.Application.ApplicationBasePath };

        /// <summary>
        ///     List assembly name to scan, default is application name. 
        /// </summary>
        /// <remarks>Default is root assembly name, e.g: Elect.Mapper.AutoMapper.dll => Scan Elect.dll and Elect.*.dll </remarks>
        public List<string> ListAssemblyName { get; set; } = new List<string> { PlatformServices.Default.Application.ApplicationName };

        /// <summary>
        ///     Lifetime of IMapper, default is Scoped. 
        /// </summary>
        public ServiceLifetime IMapperLifeTime { get; set; } = ServiceLifetime.Scoped;

        /// <summary>
        ///     Assert the mapper profile is valid after finish configuration, default is true. 
        /// </summary>
        public bool IsAssertConfigurationIsValid { get; set; } = true;

        /// <summary>
        ///     Compile mapping after configuration to boost map speed, default is true. 
        /// </summary>
        public bool IsCompileMappings { get; set; } = true;

        public Action<IMapperConfigurationExpression> AdditionalInitial { get; set; } = _ => { };
    }
}