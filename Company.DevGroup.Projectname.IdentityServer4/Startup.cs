using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Company.DevGroup.Projectname.IdentityServer4
{
    public class Startup
    {
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
            .AddInMemoryApiScopes(Config.ApiScopes)
            //���ò����û�
            .AddTestUsers(Config.Users)
            // in-memory, code config
            //Ԥ��������Դ
            .AddInMemoryIdentityResources(Config.IdentityResources)
            //����API��Դ
            .AddInMemoryApiResources(Config.ApiResources)
            //Ԥ��������֤��Client
            .AddInMemoryClients(Config.Clients)
            //���ÿ�����ǩ�������ڿ���ʱʹ�ã���������������-����Ҫ����Կ���ϴ洢�ڰ�ȫ�ĵط�
            .AddDeveloperSigningCredential(persistKey: false);
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
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}