using Company.DevGroup.Projectname.Application.Dtos;
using System;
using System.Security.Claims;

namespace Company.DevGroup.Projectname.Application
{
    public interface IAuthenticationApp
    {
        bool IsAuthenticated(LoginRequestDTO request, out string token);
        bool RefreshToken(RefreshTokenDTO request, out string token);
        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="claims">claims</param>
        /// <returns>token</returns>
        TokenEntityDTO GenerateToken(params Claim[] claims);
    }
}
