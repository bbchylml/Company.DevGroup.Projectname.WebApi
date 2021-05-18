using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace Company.DevGroup.Projectname.UserService
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
            services.AddCors(options => {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins(Configuration["Origins"].Split(',')).AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });

            //�������֤������ӵ�DI������BearerΪĬ�Ϸ�����
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //ָ����Ȩ��ַ
                    options.Authority = Configuration["IdentityServer"];
                    //��ȡ������Ԫ���ݵ�ַ��Ȩ���Ƿ���ҪHTTPS��Ĭ��ֵΪtrue����Ӧ��ֻ�ڿ��������н��á�
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //������֤ʱ��ʱҪӦ�õ�ʱ��ƫ�ƣ���token�����֤һ�Σ�Ĭ��Ϊ5����
                        ClockSkew = TimeSpan.FromMinutes(1),
                        //ָʾ�����Ƿ������С����ڡ�ֵ
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        //�����ڶ������Ƶķ���Ⱥ����м��ķ���Ⱥ�壬��ӦIdp��ApiResource��Name
                        ValidAudiences = new List<string>
                        {
                            "UserService"
                        }
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();      //����
            app.UseStaticFiles();       //��������ʾindex.html
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();     
            app.UseAuthentication();    //���������֤
            app.UseAuthorization();     //������Ȩ��������UseCors�󣬲�ȻCors����Ч

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
