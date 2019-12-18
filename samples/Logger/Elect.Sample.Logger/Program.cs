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

                _.IsEnableLogToFile = true;
            });

            var provider = services.BuildServiceProvider();

            using (var scoped = provider.CreateScope())
            {
                var electLog = scoped.ServiceProvider.GetService<IElectLog>();

                electLog.Capture("Credit cart is 378282246310005", LogType.Debug);
                electLog.Capture("Credit cart is 378282246310005", LogType.Info);
                electLog.Capture("Credit cart is 378282246310005", LogType.Warning);
                electLog.Capture("Credit cart is 378282246310005", LogType.Error);
                electLog.Capture("Not credit cart is 123456789", LogType.Fatal);

                try
                {
                    throw new Exception("Exception sample");
                }
                catch (Exception e)
                {
                    electLog.Capture(e, LogType.Debug);
                    electLog.Capture(e, LogType.Info);
                    electLog.Capture(e, LogType.Warning);
                    electLog.Capture(e, LogType.Error);
                    electLog.Capture(e, LogType.Fatal);
                }
            }

            Console.ReadKey();
        }
    }
}