namespace Elect.Web.Consul.FabioClient
{
    public interface IElectFabioClient
    {
        Uri GetEndpoint(string serviceName);
    }
}
