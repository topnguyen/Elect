namespace Elect.Sample.DI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = BuildWebHost(args);
            OnAppStart(webHost);
            webHost.Run();
        }
        public static IWebHost BuildWebHost(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);
            webHostBuilder.UseStartup<Startup>();
            var webHost = webHostBuilder.Build();
            return webHost;
        }
        private static void OnAppStart(IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var sampleService = serviceProvider.GetService<Startup.ISampleService>();
                var token = sampleService.GetRandomString();
                Console.WriteLine(token);
            }
        }
    }
}
