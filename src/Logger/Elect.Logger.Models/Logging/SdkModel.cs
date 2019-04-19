using Elect.Core.ObjUtils;

namespace Elect.Logger.Models.Logging
{
    public class SdkModel : ElectDisposableModel
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}