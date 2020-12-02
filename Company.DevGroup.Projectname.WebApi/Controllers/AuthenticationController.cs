using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.DevGroup.Projectname.WebApi.Controllers
{
    /// <summary>
    /// api 验证控制器
    /// </summary>
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationApp _authApp;

        /// <summary>
        /// AuthenticationController .ctor
        /// </summary>
        /// <param name="authApp"></param>
        public AuthenticationController(IAuthenticationApp authApp)
        {
            this._authApp = authApp;
        }

        /// <summary>
        /// 请求AccessToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("requestToken")]
        public ActionResult RequestToken([FromBody] LoginRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (_authApp.IsAuthenticated(request, out token))
            {
                return Ok(token);
            }
            return BadRequest("Invalid Request");
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("refreshToken")]
        public ActionResult RefreshToken([FromBody] RefreshTokenDTO request)
        {
            if (request.Token == null && request.refreshToken == null)
                return BadRequest("Invalid Request");

            //这儿是验证Token的代码
            string refreshToken;
            if (_authApp.RefreshToken(request, out refreshToken))
            {
                return Ok(new[] { request.Token, refreshToken });
            }

            return BadRequest("Invalid Request");
        }
    }
}