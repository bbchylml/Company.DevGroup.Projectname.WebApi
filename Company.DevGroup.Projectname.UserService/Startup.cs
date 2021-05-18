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

            //将身份验证服务添加到DI并配置Bearer为默认方案。
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    //指定授权地址
                    options.Authority = Configuration["IdentityServer"];
                    //获取或设置元数据地址或权限是否需要HTTPS。默认值为true。这应该只在开发环境中禁用。
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //设置验证时间时要应用的时钟偏移，即token多久验证一次，默认为5分钟
                        ClockSkew = TimeSpan.FromMinutes(1),
                        //指示令牌是否必须具有“过期”值
                        RequireExpirationTime = true,
                        ValidateIssuer = true,
                        //将用于对照令牌的访问群体进行检查的访问群体，对应Idp中ApiResource的Name
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

            app.UseDefaultFiles();      //新增
            app.UseStaticFiles();       //新增，显示index.html
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();     
            app.UseAuthentication();    //启用身份验证
            app.UseAuthorization();     //启用授权，必须在UseCors后，不然Cors不生效

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
