namespace Elect.Sample.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = BuildWebHost(args);
            webHost.Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);
            webHostBuilder.UseStartup<Startup>();
            var webHost = webHostBuilder.Build();
            return webHost;
        }
    }
}
