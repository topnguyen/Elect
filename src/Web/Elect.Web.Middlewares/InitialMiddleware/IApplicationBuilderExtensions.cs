#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> IApplicationBuilderExtensions.cs </Name>
//         <Created> 21/03/2018 8:04:13 PM </Created>
//         <Key> 07b75768-5c2b-45b2-9317-263b48f7c615 </Key>
//     </File>
//     <Summary>
//         IApplicationBuilderExtensions.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Elect.Web.Middlewares.InitialMiddleware
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Enable Rewind help to get Request Body content. 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElectInitial(this IApplicationBuilder app, Action<IServiceProvider> action)
        {
            return app.UseMiddleware<ElectInitialMiddleware>(action);
        }
        
        /// <summary>
        ///     Enable Rewind help to get Request Body content. 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseElectInitial(this IApplicationBuilder app, Action action)
        {
            return app.UseMiddleware<ElectInitialMiddleware>(action);
        }

        public class ElectInitialMiddleware
        {
            private readonly RequestDelegate _next;
            
            private Task _initializationTask;

            // Initial Without Service Provider
            
            public ElectInitialMiddleware(RequestDelegate next, IApplicationLifetime lifetime, Action action)
            {
                _next = next;
                
                var startRegistration = default(CancellationTokenRegistration);
                
                startRegistration = lifetime.ApplicationStarted.Register(() =>
                {
                    _initializationTask = InitializeAsync(action, lifetime.ApplicationStopping);
                    
                    startRegistration.Dispose();
                });
            }
            
            private async Task InitializeAsync(Action action, CancellationToken cancellationToken = default)
            {
                var initialTask = Task.Run(() => { action.Invoke(); }, cancellationToken);

                await initialTask;
            }
            
            // Initial With Service Provider
            
            public ElectInitialMiddleware(RequestDelegate next, IServiceProvider serviceProvider, IApplicationLifetime lifetime, Action<IServiceProvider> action)
            {
                _next = next;
                
                var startRegistration = default(CancellationTokenRegistration);
                
                startRegistration = lifetime.ApplicationStarted.Register(() =>
                {
                    _initializationTask = InitializeAsync(serviceProvider, action, lifetime.ApplicationStopping);
                    
                    startRegistration.Dispose();
                });
            }
            
            private async Task InitializeAsync(IServiceProvider serviceProvider, Action<IServiceProvider> action, CancellationToken cancellationToken = default)
            {
                var initialTask = Task.Run(() => { action.Invoke(serviceProvider); }, cancellationToken);

                await initialTask;
            }

            // Invoke
            
            public async Task Invoke(HttpContext context)
            {
                var initializationTask = _initializationTask;

                if (initializationTask != null)
                {
                    await initializationTask;
 
                    _initializationTask = null;
                }
                
                await _next(context);
            }
        }
    }
}