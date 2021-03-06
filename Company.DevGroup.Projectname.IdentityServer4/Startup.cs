using Company.DevGroup.Projectname.IdentityServer4.Context;
using Company.DevGroup.Projectname.IdentityServer4.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Company.DevGroup.Projectname.IdentityServer4
{
    public class Startup
    {
        /// <summary>
        /// Startup构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //注册IdentityServer中间件
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            //启用开发者签名，仅在开发时使用，不建议用于生产-你需要将密钥材料存储在安全的地方
            //.AddDeveloperSigningCredential(persistKey: false);
            .AddDeveloperSigningCredential(true, "tempkey.rsa")//解决Keyset is missing 错误
            //主要是user这里替换我们自己的服务
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            //配置测试用户
            //.AddTestUsers(Config.Users)
            .AddInMemoryApiScopes(Config.ApiScopes)
            // in-memory, code config
            //预置身份资源
            .AddInMemoryIdentityResources(Config.IdentityResources)
            //配置API资源
            .AddInMemoryApiResources(Config.ApiResources)
            //预置允许验证的Client
            .AddInMemoryClients(Config.Clients);

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<UserDbContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], 
                new MySqlServerVersion(new Version(5, 7, 29)), // ServerVersion for MariaDB
                p => p.CommandTimeout(60))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            //使用IdentityServer中间件，必须放到 UseRouting 与 UseEndpoints 之间。
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("IdentityServer4 Server Started!");
                });
            });
        }
    }
}
