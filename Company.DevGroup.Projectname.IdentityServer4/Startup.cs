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
        /// Startup���캯��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ϵͳ����
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //ע��IdentityServer�м��
            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            //���ÿ�����ǩ�������ڿ���ʱʹ�ã���������������-����Ҫ����Կ���ϴ洢�ڰ�ȫ�ĵط�
            //.AddDeveloperSigningCredential(persistKey: false);
            .AddDeveloperSigningCredential(true, "tempkey.rsa")//���Keyset is missing ����
            //��Ҫ��user�����滻�����Լ��ķ���
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            //���ò����û�
            //.AddTestUsers(Config.Users)
            .AddInMemoryApiScopes(Config.ApiScopes)
            // in-memory, code config
            //Ԥ�������Դ
            .AddInMemoryIdentityResources(Config.IdentityResources)
            //����API��Դ
            .AddInMemoryApiResources(Config.ApiResources)
            //Ԥ��������֤��Client
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

            //ʹ��IdentityServer�м��������ŵ� UseRouting �� UseEndpoints ֮�䡣
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
