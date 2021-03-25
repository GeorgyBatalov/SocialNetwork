using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SocialNetworkSample.App
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Configure(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder Configure(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webHostBuilder =>
                       {
                           webHostBuilder.ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
                           {
                               configurationBuilder.AddJsonFile(configName, optional: false, reloadOnChange: false);
                               configurationBuilder.Build();
                           })
                           .UseKestrel()
                           .PreferHostingUrls(true)
                           .UseUrls(serviceOptions.WebAddress)
                           .ConfigureServices(x => x.AddAutofac())
                           //.UseContentRoot(Directory.GetCurrentDirectory())
                           .UseStartup<Startup>();
                       });
        }
    }
}
