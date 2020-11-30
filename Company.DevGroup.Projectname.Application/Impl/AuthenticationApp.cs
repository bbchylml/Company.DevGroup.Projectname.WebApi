<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> add Identity Server4
﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Company.DevGroup.Projectname.Application.Impl
{
    public class AuthenticationApp : IAuthenticationApp
    {
        private readonly IUserApp _userApp;
        private readonly TokenParameter _tokenParameter;
        public AuthenticationApp(IUserApp userApp, IOptions<TokenParameter> tokenParameter)
        {
            _userApp = userApp;
            _tokenParameter = tokenParameter.Value;
        }
        public bool IsAuthenticated(LoginRequestDTO request, out string token)
        {
            token = string.Empty;
            if (!_userApp.IsValid(request))
                return false;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenParameter.Issuer, _tokenParameter.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration), signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }

        public bool RefreshToken(RefreshTokenDTO request, out string refreshToken)
        {
            refreshToken = string.Empty;

            var handler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal claim = handler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenParameter.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                }, out SecurityToken securityToken);

                var username = claim.Identity.Name;

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwtToken = new JwtSecurityToken(_tokenParameter.Issuer, _tokenParameter.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration), signingCredentials: credentials);
                refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TokenEntityDTO GenerateToken(params Claim[] claims)
        {
            var now = DateTime.UtcNow;
            var claimList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };
            if (claims != null)
            {
                claimList.AddRange(claims);
            }

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _tokenParameter.Issuer,
                audience: _tokenParameter.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_tokenParameter.ValidFor),
                signingCredentials: _tokenParameter.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenEntityDTO
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)_tokenParameter.ValidFor.TotalSeconds
            };
            return response;
        }

        /// <summary>
        /// ToUnixEpochDate
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns></returns>
        public static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
<<<<<<< HEAD
=======
﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Company.DevGroup.Projectname.Application.Impl
{
    public class AuthenticationApp : IAuthenticationApp
    {
        private readonly IUserApp _userApp;
        private readonly TokenParameter _tokenParameter;
        public AuthenticationApp(IUserApp userApp, IOptions<TokenParameter> tokenParameter)
        {
            _userApp = userApp;
            _tokenParameter = tokenParameter.Value;
        }
        public bool IsAuthenticated(LoginRequestDTO request, out string token)
        {
            token = string.Empty;
            if (!_userApp.IsValid(request))
                return false;
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenParameter.Issuer, _tokenParameter.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration), signingCredentials: credentials);
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }

        public bool RefreshToken(RefreshTokenDTO request, out string refreshToken)
        {
            refreshToken = string.Empty;

            var handler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal claim = handler.ValidateToken(request.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenParameter.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                }, out SecurityToken securityToken);

                var username = claim.Identity.Name;

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenParameter.Secret));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var jwtToken = new JwtSecurityToken(_tokenParameter.Issuer, _tokenParameter.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenParameter.AccessExpiration), signingCredentials: credentials);
                refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TokenEntityDTO GenerateToken(params Claim[] claims)
        {
            var now = DateTime.UtcNow;
            var claimList = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };
            if (claims != null)
            {
                claimList.AddRange(claims);
            }

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _tokenParameter.Issuer,
                audience: _tokenParameter.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_tokenParameter.ValidFor),
                signingCredentials: _tokenParameter.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenEntityDTO
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)_tokenParameter.ValidFor.TotalSeconds
            };
            return response;
        }

        /// <summary>
        /// ToUnixEpochDate
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns></returns>
        public static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6
=======
>>>>>>> add Identity Server4
}