using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace Company.DevGroup.Projectname.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("ocelot.json", true, true)
                        .AddEnvironmentVariables();
                    })
                    .ConfigureServices(services =>
                    {
                        //×¢ÈëOcelot¡¢Consulµ½ÈÝÆ÷
                        services.AddOcelot().AddConsul().AddPolly();
                        services.AddHttpContextAccessor();
                        services.AddLogging();
                    })
                    .Configure(app =>
                    {
                        app.UseOcelot().Wait();
                    });
                });
    }
}
