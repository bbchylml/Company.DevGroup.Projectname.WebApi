<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> add Identity Server4
﻿using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
=======
>>>>>>> add Identity Server4

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
<<<<<<< HEAD
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
=======
﻿using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
=======
        /// <summary>
        /// AuthenticationController .ctor
        /// </summary>
        /// <param name="authApp"></param>
>>>>>>> add Identity Server4
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
<<<<<<< HEAD
>>>>>>> 06de2a93f4a5608de4212ff7aa5b5cc6082aafe6
=======
>>>>>>> add Identity Server4
