using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Impl;
using Company.DevGroup.Projectname.Data;
using Company.DevGroup.Projectname.Models.Jwt;
using Company.DevGroup.Projectname.WebApi.Configs.IdentityServer4;
using Company.DevGroup.Projectname.WebApi.Extensions;
using Company.DevGroup.Projectname.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

using Microsoft.EntityFrameworkCore;
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
    /// ����������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup .ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ϵͳ���ýӿ�
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //ע��HttpClient����
            services.AddHttpClient();

            //Add MvcFramework ����Json����
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            // cookie policy to deal with temporary browser incompatibilities
            services.AddSameSiteCookiePolicy();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });

            services.Configure<TokenParameter>(Configuration.GetSection("tokenParameter"));
            var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();

            #region ����JWT
            /*
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
            */

            #endregion

            #region ����IdentityServer4
            //��������ӵ��ڴ�
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
            //ʹ�ù̶���֤��,��ֹIdentityServer4��������������������tokenʧЧ
            //.AddDeveloperSigningCredential(true, "tempkey.jwk");

            //����IdentityServer4��֤
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //IdentityServer4��֤��������ַ
                options.Authority = "http://localhost:5000";
                //��ȡ������Ԫ���ݵ�ַ��䷢�����Ƿ���ҪHTTPS��Ĭ��ֵΪtrue��ֻ���ڿ��������в�Ӧ���ô˹��ܡ�   
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    //�����ڶ������Ƶķ���Ⱥ����м��ķ���Ⱥ�壬����
                    //scope,��ӦConfig.GetApis()��ApiResource��Name
                    ValidAudiences = new List<string>
                    {
                            "api",
                            "api.scope1", 
                            "api.scope2"
                    }
                };
            });

            #endregion

            #region ���cors ���� ���ÿ����� 

            //���cors ���� ���ÿ�����   
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader()
                    .AllowAnyOrigin(); //�����κ���Դ����������
                });
            });

            #endregion

            #region ��Swagger���ϰ汾

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
                        Name = "TracyChan1988",
                        Email = "bbchylml@126.com"
                    }
                });

                #region ����swagger��֤���� 5.x �汾
                //���һ�������ȫ�ְ�ȫ��Ϣ����AddSecurityDefinition����ָ���ķ�������һ�¼��ɣ�bearerScheme��
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ���·�����Bearer {token} ���ɣ�ע������֮���пո����磺\"Bearer {token}\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                };

                options.AddSecurityDefinition("Bearer", bearerScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, Array.Empty<string>()
                    }
                });

                #endregion

                //����xml�����ļ�,ָ����ȡվ��Ŀ¼�����е�xml�ļ�
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

            #endregion

            services.AddScoped<IAuthenticationApp, AuthenticationApp>();
            services.AddScoped<IUserApp, UserApp>();

            services.AddDbContext<ApiContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ApiContext"),
                    p => p.MigrationsAssembly("Company.DevGroup.Projectname.Data")
                )
            );
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

            //����SwaggerUI
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Company.DevGroup.Projectname API V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Company.DevGroup.Projectname API V2 Docs");
                options.ShowExtensions();
                options.RoutePrefix = string.Empty; //c.RoutePrefix = "swagger";  //Ĭ��
            });

            app.UseRouting();

            //���������֤
            app.UseAuthentication();
            //������Ȩ
            app.UseAuthorization();
            //����IdentityServer4
            app.UseIdentityServer();

            //����Cors
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}