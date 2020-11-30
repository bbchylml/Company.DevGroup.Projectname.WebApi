using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Company.DevGroup.Projectname.IdentityServer4
{
    /// <summary>
    /// IdentityServer资源和客户端配置文件
    /// </summary>
    public static class Config
    {
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

        /// <summary>
        /// API资源集合
        ///     如果您将在生产环境中使用此功能，那么给您的API取一个逻辑名称就很重要。
        ///     开发人员将使用它通过身份服务器连接到您的api。
        ///     它应该以简单的方式向开发人员和用户描述您的api。
        /// </summary>
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource> { new ApiResource("api1", "My API") };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            // backward compat
            new ApiScope("api"),
                
            // more formal
            new ApiScope("api1"),
            new ApiScope("api2"),
            new ApiScope("api3")
        };

    /// <summary>
    /// 客户端集合
    /// </summary>
    public static IEnumerable<Client> Clients =>
            new Client[]
            {
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
                    AllowedScopes = { "api1" }
                },
                /// 资源所有者密码凭证（ResourceOwnerPassword）
                ///     Resource Owner其实就是User，所以可以直译为用户名密码模式。
                ///     密码模式相较于客户端凭证模式，多了一个参与者，就是User。
                ///     通过User的用户名和密码向Identity Server申请访问令牌。
                new Client
                {
                    ClientId = "client2",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "api2" }
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
                    AccessTokenLifetime = 60,
                    AllowedScopes =
                    {
                        "api3",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Phone
                    }
                }
            };

        /// <summary>
        /// 用户集合
        /// </summary>
        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser{SubjectId = "818727", Username = "alice", Password = "alice",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json)
                    }
                },
                new TestUser{SubjectId = "88421113", Username = "bob", Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json),
                        new Claim("location", "somewhere")
                    }
                }
            };
    }
}