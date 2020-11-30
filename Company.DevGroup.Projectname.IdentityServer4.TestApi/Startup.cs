using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace Company.DevGroup.Projectname.IdentityServer4.TestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //�������֤������ӵ�DI������BearerΪĬ�Ϸ�����
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //ָ����Ȩ��ַ
                    options.Authority = "http://localhost:5000";
                    //��ȡ������Ԫ���ݵ�ַ��Ȩ���Ƿ���ҪHTTPS��Ĭ��ֵΪtrue����Ӧ��ֻ�ڿ��������н��á�
                    options.RequireHttpsMetadata = false;
                    //��ȡ�������κν��յ���OpenIdConnect���Ƶķ���Ⱥ�塣
                    //options.Audience = "api1";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //������֤ʱ��ʱҪӦ�õ�ʱ��ƫ�ƣ���token�����֤һ�Σ�Ĭ��Ϊ5����
                        ClockSkew = TimeSpan.FromMinutes(1),
                        //ָʾ�����Ƿ������С����ڡ�ֵ
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        //�����ڶ������Ƶķ���Ⱥ����м��ķ���Ⱥ�壬����
                        //scope,��ӦConfig.GetApis()��ApiResource��Name
                        ValidAudiences = new List<string>
                        {
                             "api1",
                             "api2"
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
