<<<<<<< HEAD
using System;
=======
ï»¿using System;
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
    /// ³ÌĞòÆô¶¯Àà
    /// </summary>
    public class Startup
    {
=======
    /// ç¨‹åºå¯åŠ¨ç±»
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
        /// ç³»ç»Ÿé…ç½®æ¥å£
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
            //Add MvcFramework ·µ»ØJsonÄÚÈİ
=======
            services.AddHttpClient();

            //Add MvcFramework è¿”å›Jsonå†…å®¹
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

            #region é…ç½®IdentityServer4è®¤è¯
            //æŠŠé…ç½®æ·»åŠ åˆ°å†…å­˜
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
                //ä½¿ç”¨å›ºå®šçš„è¯ä¹¦,é˜²æ­¢IdentityServer4æœåŠ¡å™¨é‡å¯å¯¼è‡´ä»¥å¾€çš„tokenå¤±æ•ˆ
                //AddDeveloperSigningCredential(true, "tempkey.jwk");

            //é…ç½®IdentityServer4è®¤è¯
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //IdentityServer4è®¤è¯æœåŠ¡å™¨åœ°å€
                    options.Authority = "http://localhost:5000";
                    //è·å–æˆ–è®¾ç½®å…ƒæ•°æ®åœ°å€æˆ–é¢å‘æœºæ„æ˜¯å¦éœ€è¦HTTPSï¼Œé»˜è®¤å€¼ä¸ºtrueã€‚åªæœ‰åœ¨å¼€å‘ç¯å¢ƒä¸­æ‰åº”ç¦ç”¨æ­¤åŠŸèƒ½ã€‚   
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        //å°†ç”¨äºå¯¹ç…§ä»¤ç‰Œçš„è®¿é—®ç¾¤ä½“è¿›è¡Œæ£€æŸ¥çš„è®¿é—®ç¾¤ä½“ï¼Œå°±æ˜¯
                        //scope,å¯¹åº”Config.GetApis()ä¸­ApiResourceçš„Name
                        ValidAudiences = new List<string>
                        {
                             "api",
                             "api.scope1", 
                             "api.scope2"
                        }
                    };
                });
            #endregion

            #region é…ç½®JWTè®¤è¯

            /*
             * 
             * é…ç½®JWTè®¤è¯
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

            //Ìí¼Ócors ·şÎñ ÅäÖÃ¿çÓò´¦Àí   
=======
            */
            #endregion

            #region æ·»åŠ cors æœåŠ¡ é…ç½®è·¨åŸŸå¤„ç†  

            //æ·»åŠ cors æœåŠ¡ é…ç½®è·¨åŸŸå¤„ç†   
>>>>>>> add Identity Server4
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader()
<<<<<<< HEAD
                    .AllowAnyOrigin(); //ÔÊĞíÈÎºÎÀ´Ô´µÄÖ÷»ú·ÃÎÊ
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
                    .AllowAnyOrigin(); //å…è®¸ä»»ä½•æ¥æºçš„ä¸»æœºè®¿é—®
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
                #region ÆôÓÃswaggerÑéÖ¤¹¦ÄÜ 5.x °æ±¾
                //Ìí¼ÓÒ»¸ö±ØĞëµÄÈ«¾Ö°²È«ĞÅÏ¢£¬ºÍAddSecurityDefinition·½·¨Ö¸¶¨µÄ·½°¸Ãû³ÆÒ»ÖÂ¼´¿É£¬bearerScheme¡£
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "JWTÊÚÈ¨(Êı¾İ½«ÔÚÇëÇóÍ·ÖĞ½øĞĞ´«Êä) ÔÚÏÂ·½ÊäÈëBearer {token} ¼´¿É£¬×¢ÒâÁ½ÕßÖ®¼äÓĞ¿Õ¸ñ¡£ÀıÈç£º\"Bearer {token}\"",
                    Name = "Authorization",//jwtÄ¬ÈÏµÄ²ÎÊıÃû³Æ
                    In = ParameterLocation.Header,//jwtÄ¬ÈÏ´æ·ÅAuthorizationĞÅÏ¢µÄÎ»ÖÃ(ÇëÇóÍ·ÖĞ)
=======
                #region å¯ç”¨swaggeréªŒè¯åŠŸèƒ½ 5.x ç‰ˆæœ¬
                //æ·»åŠ ä¸€ä¸ªå¿…é¡»çš„å…¨å±€å®‰å…¨ä¿¡æ¯ï¼Œå’ŒAddSecurityDefinitionæ–¹æ³•æŒ‡å®šçš„æ–¹æ¡ˆåç§°ä¸€è‡´å³å¯ï¼ŒbearerSchemeã€‚
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Description = "JWTæˆæƒ(æ•°æ®å°†åœ¨è¯·æ±‚å¤´ä¸­è¿›è¡Œä¼ è¾“) åœ¨ä¸‹æ–¹è¾“å…¥Bearer {token} å³å¯ï¼Œæ³¨æ„ä¸¤è€…ä¹‹é—´æœ‰ç©ºæ ¼ã€‚ä¾‹å¦‚ï¼š\"Bearer {token}\"",
                    Name = "Authorization",//jwté»˜è®¤çš„å‚æ•°åç§°
                    In = ParameterLocation.Header,//jwté»˜è®¤å­˜æ”¾Authorizationä¿¡æ¯çš„ä½ç½®(è¯·æ±‚å¤´ä¸­)
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
                //Ôö¼ÓxmlÅäÖÃÎÄ¼ş,Ö¸¶¨¶ÁÈ¡Õ¾µãÄ¿Â¼ÏÂËùÓĞµÄxmlÎÄ¼ş
=======
                //å¢åŠ xmlé…ç½®æ–‡ä»¶,æŒ‡å®šè¯»å–ç«™ç‚¹ç›®å½•ä¸‹æ‰€æœ‰çš„xmlæ–‡ä»¶
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
                options.RoutePrefix = string.Empty; //c.RoutePrefix = "swagger";  //Ä¬ÈÏ
=======
                options.RoutePrefix = string.Empty; //c.RoutePrefix = "swagger";  //é»˜è®¤
>>>>>>> add Identity Server4
            });

            app.UseRouting();

<<<<<<< HEAD
            //ÆôÓÃJwtÑéÖ¤
            app.UseAuthentication();

            app.UseAuthorization();

            //ÅäÖÃCors
=======
            //å¯ç”¨èº«ä»½éªŒè¯ Jwtå’ŒIdentityServer4éƒ½éœ€è¦
            app.UseAuthentication();
            //å¯ç”¨æˆæƒ Jwtå’ŒIdentityServer4éƒ½éœ€è¦
            app.UseAuthorization();

            //å¯ç”¨IdentityServer4
            app.UseIdentityServer();

            //é…ç½®Cors
>>>>>>> add Identity Server4
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
