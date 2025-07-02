namespace Elect.Web.HttpDetection
{
    public static class HttpRequestExtensions
    {
        public static DeviceModel GetDeviceInformation(this HttpRequest request)
        {
            return new DeviceModel(request);
        }
    }
}
