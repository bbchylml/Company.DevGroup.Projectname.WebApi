using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Company.DevGroup.Projectname.Application;
using Company.DevGroup.Projectname.Application.Dtos;
using Company.DevGroup.Projectname.Models;
using Company.DevGroup.Projectname.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WeihanLi.Common;

namespace Company.DevGroup.Projectname.WebApi.Controllers
{
    /// <summary>
    /// Account
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserApp _userApp;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ILogger _logger;
        private readonly IAuthenticationApp _authenticationApp;

        /// <summary>
        /// AccountController .ctor
        /// </summary>
        /// <param name="userManager">userManager</param>
        /// <param name="signInManager">signInManager</param>
        /// <param name="userApp">userApp</param>
        /// <param name="authenticationApp">authenticationApp</param>
        /// <param name="loggerFactory">loggerFactory</param>
        public AccountController(UserManager<Users> userManager,
            SignInManager<Users> signInManager,
            IUserApp userApp,
            IAuthenticationApp authenticationApp,
            ILoggerFactory loggerFactory)
        {
            _userApp = userApp;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationApp = authenticationApp;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        /// <summary>
        /// SignIn
        /// </summary>
        /// <remarks>
        /// POST /api/v1/Account/SignIn
        /// {
        ///     "Email":"test0001@test.com",
        ///     "Password":"test001.com"
        /// }
        /// </remarks>
        [Route("SignIn")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] LoginRequestDTO loginModel)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var userInfo = new Users()
            {
                Email = loginModel.Email
            };
            var result = new JsonResponseModel<TokenEntityDTO>();
            var signinResult = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, true, lockoutOnFailure: false);
            if (signinResult.Succeeded)
            {
                _logger.LogInformation("User logged in.");

                userInfo = _userApp.FetchAsync(u => u.Email == loginModel.Email);
                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, ObjectIdGenerator.Instance.NewId()),
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                    new Claim(JwtRegisteredClaimNames.NameId, userInfo.Id.ToString()),
                };
                var token = _authenticationApp.GenerateToken(claims);

                var userToken = new UserTokenDTO
                {
                    AccessToken = token.AccessToken,
                    ExpiresIn = token.ExpiresIn,
                    UserEmail = userInfo.Email,
                    UserId = userInfo.Id,
                    UserName = userInfo.UserName
                };
                result = new JsonResponseModel<TokenEntityDTO> { Data = userToken, Msg = "", Status = JsonResponseStatus.Success };
            }
            else
            {
                if (signinResult.IsLockedOut)
                {
                    result = new JsonResponseModel<TokenEntityDTO> { Data = null, Msg = "Account locked out", Status = JsonResponseStatus.RequestError };
                }
                else
                {
                    result = new JsonResponseModel<TokenEntityDTO> { Data = null, Msg = "failed to authenticate", Status = JsonResponseStatus.AuthFail };
                }
            }
            return Json(result);
        }

        /// <summary>
        /// SignUp
        /// </summary>
        /// <remarks>
        /// POST /api/v1/Account/SignUp
        /// {
        ///     "Email":"test0001@test.com",
        ///     "Password":"test001.com"
        /// }
        /// </remarks>
        /// <returns></returns>
        [Route("SignUp")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] RegisterDTO regModel)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var userInfo = new Users()
            {
                UserName = regModel.Email,
                Email = regModel.Email,
                EmailConfirmed = true,
                CreatedTime = DateTime.Now
            };
            var result = new JsonResponseModel<TokenEntityDTO>();
            var signupResult = await _userManager.CreateAsync(userInfo, regModel.Password);
            if (signupResult.Succeeded)
            {
                _logger.LogInformation(3, "User created a new account");
                userInfo = _userApp.FetchAsync(u => u.Email == regModel.Email);

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, ObjectIdGenerator.Instance.NewId()),
                    new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                    new Claim(ClaimTypes.Name, userInfo.UserName),
                };
                var token = _authenticationApp.GenerateToken(claims);

                var userToken = new UserTokenDTO
                {
                    AccessToken = token.AccessToken,
                    ExpiresIn = token.ExpiresIn,
                    UserEmail = userInfo.Email,
                    UserId = userInfo.Id,
                    UserName = userInfo.UserName
                };
                result = new JsonResponseModel<TokenEntityDTO> { Data = userToken, Msg = "", Status = JsonResponseStatus.Success };
            }
            else
            {
                result = new JsonResponseModel<TokenEntityDTO> { Data = null, Msg = "sign up failed," + string.Join(",", signupResult.Errors.Select(e => e.Description).ToArray()), Status = JsonResponseStatus.ProcessFail };
            }
            return Json(result);
        }
    }
}