using System;
using Elect.Logger.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Test.Logger.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddElectLog();

            var provider = services.BuildServiceProvider();

            using (var scoped = provider.CreateScope())
            {
                var electLog = scoped.ServiceProvider.GetService<IElectLog>();
                
                electLog.BeforeLog = (logInfo) =>
                {
                    var log = logInfo;
                    return logInfo;
                };
            
                electLog.Capture("message credit cart is 378282246310005");
            }

            Console.ReadKey();
        }
    }
}