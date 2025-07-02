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
