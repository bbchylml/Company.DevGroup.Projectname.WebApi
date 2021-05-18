using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using System;
using System.IO;

namespace Company.DevGroup.Projectname.ApiGateway
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; protected set; }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json", true, true)
                        .AddEnvironmentVariables();
                    })
                    .ConfigureServices((hostingContext, services) =>
                    {
                        var builder = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddJsonFile("ocelot.json", true, true)
                        .AddEnvironmentVariables();

                        Configuration = builder.Build();

                        services.AddCors(options =>
                        {
                            options.AddDefaultPolicy(
                                builder =>
                                {
                                    builder.WithOrigins(Configuration["Origins"].Split(',')).AllowAnyMethod().AllowAnyHeader();
                                }
                            );
                        });

                        #region Identity Server 4
                        var authenticationProviderKey = "apigateway";
                        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddIdentityServerAuthentication(authenticationProviderKey, m =>
                                {
                                    m.Authority = Configuration["IdentityServer"];//Ids的地址，获取公钥
                                    m.RequireHttpsMetadata = false;
                                    m.SupportedTokens = SupportedTokens.Both;
                                });
                        #endregion

                        //注入Ocelot、Consul到容器
                        services.AddOcelot().AddConsul().AddPolly();
                        services.AddHttpContextAccessor();
                        services.AddLogging();
                    })
                    .Configure(app =>
                    {
                        app.UseDefaultFiles();
                        app.UseStaticFiles();
                        app.UseCors();
                        app.UseOcelot().Wait();
                    });
                });
    }
}
