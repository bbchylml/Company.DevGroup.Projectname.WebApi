using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Company.DevGroup.Projectname.Models.Jwt
{
    public class TokenParameter
    {
        /// <summary>
        /// JWT加密的密钥。现在主流用SHA256加密，需要256位以上的密钥，unicode是16个字符以上，尽量复杂一些。密钥泄露，Token就会被破解
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// 签发人的名称，如果没人注意，你可以把大名写在上面。
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        /// <summary>
        /// "nbf" (Not Before) Claim (default is UTC NOW)
        /// </summary>
        /// <remarks>The "nbf" (not before) claim identifies the time before which the JWT
        ///   MUST NOT be accepted for processing.  The processing of the "nbf"
        ///   claim requires that the current date/time MUST be after or equal to
        ///   the not-before date/time listed in the "nbf" claim.  Implementers MAY
        ///   provide for some small leeway, usually no more than a few minutes, to
        ///   account for clock skew.  Its value MUST be a number containing a
        ///   NumericDate value.  Use of this claim is OPTIONAL.</remarks>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>
        /// "iat" (Issued At) Claim (default is UTC NOW)
        /// </summary>
        /// <remarks>The "iat" (issued at) claim identifies the time at which the JWT was
        ///   issued.  This claim can be used to determine the age of the JWT.  Its
        ///   value MUST be a number containing a NumericDate value.  Use of this
        ///   claim is OPTIONAL.</remarks>
        public DateTime IssuedAt => DateTime.UtcNow;

        /// <summary>
        /// Set the timespan the token will be valid for (default is 2 hour/7200 seconds)
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(2);

        /// <summary>
        /// 访问时间的过期时间，单位(分钟)
        /// </summary>
        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        /// <summary>
        ///  refreshToken的有效分钟数。过了这个时间，用户需要重新登录。
        /// </summary>
        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }

        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        /// <remarks>The "jti" (JWT ID) claim provides a unique identifier for the JWT.
        ///   The identifier value MUST be assigned in a manner that ensures that
        ///   there is a negligible probability that the same value will be
        ///   accidentally assigned to a different data object; if the application
        ///   uses multiple issuers, collisions MUST be prevented among values
        ///   produced by different issuers as well.  The "jti" claim can be used
        ///   to prevent the JWT from being replayed.  The "jti" value is a case-
        ///   sensitive string.  Use of this claim is OPTIONAL.</remarks>
        public Func<Task<string>> JtiGenerator =>
          () => Task.FromResult(Guid.NewGuid().ToString());

        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}