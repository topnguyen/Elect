﻿using System.Collections.Generic;
using Elect.Core.StringUtils;
using Elect.DI;
using Elect.DI.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Elect.Sample.DI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Register Dependency Injection
            services.AddElectDI();
            
//          services.AddElectDI(_ =>
//          {
//              _.ListAssemblyName = new List<string> // default is 1 assembly name: application name - Elect.Sample.DI
//              {
//                  "ExampleAssembly" // will scan ExampleAssembly.dll and ExampleAssembly.*.dll
//              };
//              _.ListAssemblyFolderPath = new List<string> // default is  1 folder: application base folder path
//              {
//                  "C:\\ExampleAssemblyFolderPath" 
//              };
//          });

            // Optional - Print out Registered Service with Information
            services.PrintServiceAddedToConsole();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        public interface ISampleService
        {
            string GetRandomString();
        }

        [ScopedDependency(ServiceType = typeof(ISampleService))]
        public class SampleService : ISampleService
        {
            public string GetRandomString()
            {
                return StringHelper.Generate(10);
            }
        }
    }
}