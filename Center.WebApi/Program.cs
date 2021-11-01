using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Center.ApplicationServices.Center.Services;
using Center.ApplicationServices.CommonServices.GeneralVariableServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Center.WebApi
{
    public class Program
    {
        public static   Task Main(string[] args)
        {
            //Set deafult proxy 
            //WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Initialize Main");
                IHost webHost = CreateHostBuilder(args).Build();

                CachingGeneralVariables(webHost);

              return     webHost.RunAsync();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }

        private static void CachingGeneralVariables(IHost webHost)
        {
            using (IServiceScope serviceScope = webHost.Services.CreateScope())
            {
                var provider = serviceScope.ServiceProvider;
                var gvServices = provider.GetRequiredService<IGeneralVariableServices>();
                gvServices.FillStandardVariables();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
              Host.CreateDefaultBuilder(args)
                .ConfigureLogging(options => options.ClearProviders())
                .UseNLog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();
                });
    }

}
