namespace Elect.Web.Consul.FabioClient
{
    public class ElectFabioClient : IElectFabioClient
    {
        private readonly ElectConsulOptions _consulOptions;
        public ElectFabioClient(IOptions<ElectConsulOptions> consulOptions)
        {
            _consulOptions = consulOptions.Value;
        }
        public Uri GetEndpoint(string serviceName)
        {
            var fabioEndpoint = new Uri($"{_consulOptions.FabioEndpoint}/{serviceName}/");
            return fabioEndpoint;
        }
    }
}
