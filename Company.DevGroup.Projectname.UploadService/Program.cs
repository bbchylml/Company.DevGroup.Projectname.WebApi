using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Company.DevGroup.Projectname.UploadService
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
                    var configuration = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
                                               .AddJsonFile("appsettings.json")
                                               .Build();

                    var url = configuration["urls"];
                    webBuilder.UseUrls(url);
                    webBuilder.UseStartup<Startup>();
                });
    }
}