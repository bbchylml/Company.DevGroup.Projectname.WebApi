using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;
using ClaimValueTypes = System.Security.Claims.ClaimValueTypes;

namespace Company.DevGroup.Projectname.IdentityServer4
{
    /// <summary>
    /// IdentityServer资源和客户端配置文件
    /// </summary>
    public sealed class Config
    {
        /// <summary>
        /// API资源集合
        ///     如果您将在生产环境中使用此功能，那么给您的API取一个逻辑名称就很重要。
        ///     开发人员将使用它通过身份服务器连接到您的api。
        ///     它应该以简单的方式向开发人员和用户描述您的api。
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource> {
            new ApiResource("secretapi1", "secretapi1 API"),
            new ApiResource("secretapi2", "secretapi2 API"),
            new ApiResource("secretapi3", "secretapi3 API"),
            new ApiResource("UserService", "UserService API"){
                //!!!重要
                Scopes = { "UserServiceScope" }
            },
            new ApiResource("UploadService", "UploadService API"){
                Scopes = { "UploadServiceScope" }
            }
        };

        /// <summary>
        /// 客户端集合
        /// </summary>
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                #region 示例

                /// 客户端模式（Client Credentials）
                ///     可以将ClientId和ClientSecret视为应用程序本身的登录名和密码。
                ///     它将您的应用程序标识到身份服务器，以便它知道哪个应用程序正在尝试与其连接。
                new Client
                { 
                    //客户端标识
                    ClientId = "client1",
                    //没有交互用户，使用clientid/secret进行身份验证，适用于和用户无关，机器与机器之间直接交互访问资源的场景。
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //认证密钥
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    //客户端有权访问的作用域
                    AllowedScopes = { "secretapi1Scope" }
                },
                /// 资源所有者密码凭证（ResourceOwnerPassword）
                ///     Resource Owner其实就是User，所以可以直译为用户名密码模式。
                ///     密码模式相较于客户端凭证模式，多了一个参与者，就是User。
                ///     通过User的用户名和密码向Identity Server申请访问令牌。
                new Client
                {
                    ClientId = "apiClientPassword",
                    ClientSecrets = { new Secret("apiSecret".Sha256()) },
                    AccessTokenLifetime = 1800,//设置AccessToken过期时间
                    AllowedGrantTypes =GrantTypes.ResourceOwnerPassword,
                    //RefreshTokenExpiration = TokenExpiration.Absolute,//刷新令牌将在固定时间点到期
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600,//以秒为单位滑动刷新令牌的生命周期。
                    //按照现有的设置，如果3600内没有使用RefreshToken，那么RefreshToken将失效。即便是在3600内一直有使用RefreshToken，RefreshToken的总生命周期不会超过30天。所有的时间都可以按实际需求调整。
                    AllowOfflineAccess = true,//如果要获取refresh_tokens ,必须把AllowOfflineAccess设置为true
                    AllowedScopes = new List<string>
                    {
                        "secretapi2Scope",
                        StandardScopes.OfflineAccess, //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        StandardScopes.OpenId,//如果要获取id_token,必须在scopes中加上OpenId和Profile，id_token需要通过refresh_tokens获取AccessToken的时候才能拿到（还未找到原因）
                        StandardScopes.Profile//如果要获取id_token,必须在scopes中加上OpenId和Profile
                    }
                },           
                /// 授权码模式（Code）
                ///     适用于保密客户端（Confidential Client），比如ASP.NET MVC等服务器端渲染的Web应用
                new Client
                {
                    ClientId = "mvc client",
                    ClientName = "ASP.NET Core MVC Client",

                    AllowedGrantTypes = GrantTypes.Code, //authorization_code
                    ClientSecrets = { new Secret("mvc secret".Sha256()) },

                    RedirectUris = { "http://localhost:6001/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:6001/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:6001/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    //访问令牌的生存期（秒）（默认为3600秒/1小时）
                    AccessTokenLifetime = 1800,
                    AllowedScopes =
                    {
                        "secretapi3Scope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone
                    }
                },

                #endregion

                new Client
                {
                    ClientId = "UserServiceClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("UserServiceSecret".Sha256()) },
                    AllowedScopes = { "UserServiceScope" },
                    AccessTokenLifetime = 60* 60 * 1
                },
                new Client
                {
                    ClientId = "UploadServiceClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("UploadServiceSecret".Sha256()) },
                    AllowedScopes = { "UploadServiceScope" },
                    AccessTokenLifetime = 60* 60 * 1
                }
            };

        /// <summary>
        /// 用户集合
        /// </summary>
        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser{SubjectId = "131648", Username = "TracyChan1988", Password = "TracyChan1988",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Tracy Chan"),
                        new Claim(JwtClaimTypes.GivenName, "Tracy"),
                        new Claim(JwtClaimTypes.FamilyName, "Chan"),
                        new Claim(JwtClaimTypes.Email, "bbchylml@126.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bbchylml.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'Xingangdong Road, Haizhu District', 'locality': 'Guangzhou', 'postal_code': 510000, 'country': 'China' }", IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("secretapi1Scope"),
            new ApiScope("secretapi2Scope"),
            new ApiScope("secretapi3Scope"),
            new ApiScope("UserServiceScope"),
            new ApiScope("UploadServiceScope")
        };

        /// <summary>
        /// 身份资源集合
        /// </summary>
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResources.Phone()
            };
    }
}