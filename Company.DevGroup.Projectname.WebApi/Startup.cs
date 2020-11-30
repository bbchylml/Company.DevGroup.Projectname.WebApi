<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> add Identity Server4
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Impl;
<<<<<<< HEAD
using Company.DevGroup.Projectname.Models.Jwt;
=======
using Company.DevGroup.Projectname.Data;
using Company.DevGroup.Projectname.Models.Jwt;
using Company.DevGroup.Projectname.WebApi.Configs.IdentityServer4;
using Company.DevGroup.Projectname.WebApi.Extensions;
>>>>>>> add Identity Server4
using Company.DevGroup.Projectname.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> add Identity Server4
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Company.DevGroup.Projectname.WebApi
{
    /// <summary>
<<<<<<< HEAD
    /// ����������
    /// </summary>
    public class Startup
    {
=======
    /// 程序启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup .ctor
        /// </summary>
        /// <param name="configuration"></param>
>>>>>>> add Identity Server4
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

<<<<<<< HEAD
=======
        /// <summary>
        /// 系统配置接口
        /// </summary>
>>>>>>> add Identity Server4
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< HEAD
            //Add MvcFramework ����Json����
=======
            services.AddHttpClient();

            //Add MvcFramework 返回Json内容
>>>>>>> add Identity Server4
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

<<<<<<< HEAD
=======
            // cookie policy to deal with temporary browser incompatibilities
            services.AddSameSiteCookiePolicy();

>>>>>>> add Identity Server4
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });

            services.Configure<TokenParameter>(Configuration.GetSection("tokenParameter"));
            var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();
<<<<<<< HEAD
=======

            #region 配置IdentityServer4认证
            //把配置添加到内存
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddTestUsers(Config.GetUsers())
                .AddInMemoryClients(Config.GetClients())
                .AddDeveloperSigningCredential(persistKey: false);
                //使用固定的证书,防止IdentityServer4服务器重启导致以往的token失效
                //AddDeveloperSigningCredential(true, "tempkey.jwk");

            //配置IdentityServer4认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //IdentityServer4认证服务器地址
                    options.Authority = "http://localhost:5000";
                    //获取或设置元数据地址或颁发机构是否需要HTTPS，默认值为true。只有在开发环境中才应禁用此功能。   
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        //将用于对照令牌的访问群体进行检查的访问群体，就是
                        //scope,对应Config.GetApis()中ApiResource的Name
                        ValidAudiences = new List<string>
                        {
                             "api",
                             "api.scope1", 
                             "api.scope2"
                        }
                    };
                });
            #endregion

            #region 配置JWT认证

            /*
             * 
             * 配置JWT认证
>>>>>>> add Identity Server4
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
<<<<<<< HEAD

            services.AddScoped<IAuthenticationApp, AuthenticationApp>();
            services.AddScoped<IUserApp, UserApp>();

            //���cors ���� ���ÿ�����   
=======
            */
            #endregion

            #region 添加cors 服务 配置跨域处理  

            //添加cors 服务 配置跨域处理   
>>>>>>> add Identity Server4
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader()
<<<<<<< HEAD
                    .AllowAnyOrigin(); //�����κ���Դ����������
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo { 
                    Version = "v2", 
                    Title = "WebApi V2",
                    Description = "API for WebApi",
                    Contact = new OpenApiContact() { 
                        Name = "TracyChan1988", 
                        Email = "bbchylml@126.com" 
                    }
                });
                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Version = "v1", 
                    Title = "WebApi V1",
                    Description = "API for WebApi",
                    Contact = new OpenApiContact() {
=======
                    .AllowAnyOrigin(); //允许任何来源的主机访问
                });
            });

            #endregion

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "WebApi V2",
                    Description = "API for WebApi",
                    Contact = new OpenApiContact()
                    {
                        Name = "TracyChan1988",
                        Email = "bbchylml@126.com"
                    }
                });
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WebApi V1",
                    Description = "API for WebApi",
                    Contact = new OpenApiContact()
                    {
>>>>>>> add Identity Server4
                        Name = "TracyChan1988",
                        Email = "bbchylml@126.com"
                    }
                });

<<<<<<< HEAD
                #region ����swagger��֤���� 5.x �汾
                //���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������һ�¼��ɣ�bearerScheme��
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ���·�����Bearer {token} ���ɣ�ע������֮���пո����磺\"Bearer {token}\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
=======
                #region 启用swagger验证功能 5.x 版本
                //添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称一致即可，bearerScheme。
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer {token} 即可，注意两者之间有空格。例如：\"Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
>>>>>>> add Identity Server4
                    Type = SecuritySchemeType.ApiKey
                };

                options.AddSecurityDefinition("Bearer", bearerScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
<<<<<<< HEAD
                    { 
=======
                    {
>>>>>>> add Identity Server4
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
<<<<<<< HEAD
                        }, Array.Empty<string>() 
=======
                        }, Array.Empty<string>()
>>>>>>> add Identity Server4
                    }
                });

                #endregion

<<<<<<< HEAD
                //����xml�����ļ�,ָ����ȡվ��Ŀ¼�����е�xml�ļ�
=======
                //增加xml配置文件,指定读取站点目录下所有的xml文件
>>>>>>> add Identity Server4
                DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);
                foreach (FileInfo file in dir.EnumerateFiles("*.xml"))
                {
                    options.IncludeXmlComments(file.FullName);
                }

                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var versions = apiDesc.CustomAttributes()
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                options.OperationFilter<RemoveVersionParameterOperationFilter>();
                options.DocumentFilter<SetVersionInPathDocumentFilter>();
            });
<<<<<<< HEAD
=======

            services.AddScoped<IAuthenticationApp, AuthenticationApp>();
            services.AddScoped<IUserApp, UserApp>();

            services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ApiContext"),
                    p => p.MigrationsAssembly("Company.DevGroup.Projectname.Data")
                )
            );
>>>>>>> add Identity Server4
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.DevGroup.Projectname API V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Company.DevGroup.Projectname API V2 Docs");
                options.ShowExtensions();
<<<<<<< HEAD
                options.RoutePrefix = string.Empty; //c.RoutePrefix = "swagger";  //Ĭ��
=======
                options.RoutePrefix = string.Empty; //c.RoutePrefix = "swagger";  //默认
>>>>>>> add Identity Server4
            });

            app.UseRouting();

<<<<<<< HEAD
            //����Jwt��֤
            app.UseAuthentication();

            app.UseAuthorization();

            //����Cors
=======
            //启用身份验证 Jwt和IdentityServer4都需要
            app.UseAuthentication();
            //启用授权 Jwt和IdentityServer4都需要
            app.UseAuthorization();

            //启用IdentityServer4
            app.UseIdentityServer();

            //配置Cors
>>>>>>> add Identity Server4
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
