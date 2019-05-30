using Elect.Core.Interfaces;

namespace Elect.Face.Kairos.Models
{
    /// <summary>
    ///     Config for the Kairos Client
    /// </summary>
    public class ElectKairosOptions : IElectOptions
    {
        public string AppId { get; set; }
        
        public string AppKey { get; set; }
        
        /// <summary>
        ///     Default gallery if you not set when call service
        /// </summary>
        public string DefaultGallery { get; set; }
    }
}