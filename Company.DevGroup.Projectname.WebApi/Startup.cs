using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Impl;
using Company.DevGroup.Projectname.Models.Jwt;
using Company.DevGroup.Projectname.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Add MvcFramework ����Json����
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
            });

            services.Configure<TokenParameter>(Configuration.GetSection("tokenParameter"));
            var token = Configuration.GetSection("tokenParameter").Get<TokenParameter>();
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

            services.AddScoped<IAuthenticationApp, AuthenticationApp>();
            services.AddScoped<IUserApp, UserApp>();

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
                options.RoutePrefix = string.Empty; //c.RoutePrefix = "swagger";  //Ĭ��
            });

            app.UseRouting();

            //����Jwt��֤
            app.UseAuthentication();

            app.UseAuthorization();

            //����Cors
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
