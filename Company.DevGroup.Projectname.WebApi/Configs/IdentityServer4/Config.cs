using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using static IdentityModel.OidcConstants;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace Company.DevGroup.Projectname.WebApi.Configs.IdentityServer4
{
    /// <summary>
    /// IdentityServer 资源配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 定义API资源
        /// </summary>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "我的第一个API")
            };
        }

        /// <summary>
        /// 定义身份资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            //定义自定义身份资源
            var customProfile = new IdentityResource(
                name: "custom.profile",
                displayName: "Custom profile",
                userClaims: new[] { "name", "email", "status" }
            );

            return new List<IdentityResource>
            {
                //定义规范中的所有作用域(scope)（OpenId，Profile，Email，Phone，Address）
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResources.Address(),
                customProfile
            };
        }

        /// <summary>
        /// 定义API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Demo API")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    //对应的是GetApiScopes()
                    Scopes = { "api", "api.scope1", "api.scope2" } 
                },
                new ApiResource
                {
                    Name = "api2",
                    // secret for using introspection endpoint
                    ApiSecrets = { new Secret("secret".Sha256()) },
                    // include the following using claims in access token (in addition to subject id)
                    UserClaims =  { JwtClaimTypes.Name, JwtClaimTypes.Email },
                    // this API defines two scopes
                    Scopes ={ "api2" }
                },
                new ApiResource("secretapi", "secretapi")
                {
                    ApiSecrets = { new Secret("secret".Sha256()) }
                }
            };
        }

        /// <summary>
        /// GetApiScopes
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                // backward compat
                new ApiScope("api"),
                
                // more formal
                new ApiScope("api.scope1"),
                new ApiScope("api.scope2"),
                
                // scope without a resource
                new ApiScope("secretapi"),

            };
        }

        /// <summary>
        /// 定义API客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //走客户端模式
                new Client
                {
                    ClientId = "client1",
                    ClientName = "Client Credentials Client",
                    //认证秘钥，用于验证的secret
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    ////授权方式为用户密码模式授权，类型可参考GrantTypes枚举
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //过期时间
                    AccessTokenLifetime=100000000,
                    // 允许的范围
                    AllowedScopes = { "api", "api.scope1", "api.scope2" }
                },
                //走账号密码模式
                new Client()
                {
                    ClientId = "client2",
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
                        "secretapi",
                        //必须要添加，否则报forbidden错误
                        StandardScopes.OfflineAccess, //如果要获取refresh_tokens ,必须在scopes中加上OfflineAccess
                        StandardScopes.OpenId,//如果要获取id_token,必须在scopes中加上OpenId和Profile，id_token需要通过refresh_tokens获取AccessToken的时候才能拿到（还未找到原因）
                        StandardScopes.Profile//如果要获取id_token,必须在scopes中加上OpenId和Profile
                    }
                }
            };
        }

        /// <summary>
        /// 获取IdentityServer4.Test.TestUser用户，真实场景中TestUser一般使用Asp.NetCore.Identity的用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "bbchylml",
                    Password = "123456"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "tracy",
                    Password = "654321",

                     Claims=new List<Claim>(){
                        new Claim(ClaimTypes.Role, "admin")
                     }
                }
            };
        }

    }
}