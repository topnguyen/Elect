using System;
using System.IO;
using Elect.Logger.Logging;
using Elect.Logger.Models.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddElectLog(_ =>
            {
                _.JsonFilePath =
                    "Logs" + Path.DirectorySeparatorChar +
                    "{Type}" + Path.DirectorySeparatorChar +
                    "{Type}_{yyyy-MM-dd}.json";
            });

            var provider = services.BuildServiceProvider();

            using (var scoped = provider.CreateScope())
            {
                var electLog = scoped.ServiceProvider.GetService<IElectLog>();

                electLog.BeforeLog = (logInfo) =>
                {
                    var log = logInfo;
                    return logInfo;
                };

                electLog.Capture("message credit cart is 378282246310005", LogType.Fatal);
            }

            Console.ReadKey();
        }
    }
}