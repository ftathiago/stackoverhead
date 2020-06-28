// <copyright file="AuthController.cs" company="BlogDoFT">
// Copyright (c) BlogDoFT. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using StackOverHead.Auth.App.Services;
using StackOverHead.Web.Models;
using StackOverHead.Web.Services;

namespace StackOverHead.Web.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [Route("api/[Controller]")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authenticationService">AuthenticationService from Auth.App.</param>
        /// <param name="tokenService">Token Service.</param>
        public AuthController(
            IAuthenticationService authenticationService,
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Authenticate a user by his credentials.
        /// </summary>
        /// <param name="userLogin">Dto with username and password.</param>
        /// <returns>Returns 200 status code when authentications is successfull.</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserLogin userLogin)
        {
            var user = _authenticationService.AuthenticateBy(userLogin.Email, userLogin.Password);
            if (user == null)
            {
                const string errorMessage = "User or password is invalid";
                return BadRequest(new { errorMessage });
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                user,
                token,
            });
        }
    }
}
