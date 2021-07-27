using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.API.Models;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        internal readonly UserManager<ApplicationUser> userManager;
        internal readonly SignInManager<ApplicationUser> signInManager;
        internal readonly IJwtConfiguration jwtConfiguration;
        internal readonly IDateTimeService dateTimeService;
        internal readonly ILogger<AuthController> logger;

        public AuthController(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IJwtConfiguration jwtConfiguration
            , IDateTimeService dateTimeService
            , ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.jwtConfiguration = jwtConfiguration;
            this.dateTimeService = dateTimeService;
            this.logger = logger;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>JWT Token</returns>
        [HttpPost("login", Order = 1)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager
                        .FindByNameAsync(model.UserName)
                        .ConfigureAwait(false);

                    if (user != null && await userManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(false))
                    {
                        var userRoles = await userManager
                            .GetRolesAsync(user)
                            .ConfigureAwait(false);

                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, dateTimeService.Now.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim("Name", user.Name),
                            new Claim("Surname", user.Surname),
                            new Claim("Email", user.Email)
                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret));

                        var token = new JwtSecurityToken(
                            issuer: null,
                            audience: null,
                            expires: dateTimeService.Now.AddHours(8),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                        //set value indicating whether session is persisted and the time at which the authentication was issued
                        var authenticationProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe,
                            IssuedUtc = dateTimeService.Now,
                            ExpiresUtc = dateTimeService.Now.AddHours(8) //expire time
                        };

                        await signInManager
                            .SignInAsync(user, authenticationProperties)
                            .ConfigureAwait(false);


                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            userName = user.UserName,
                            name = user.Name,
                            lastName = user.Surname,
                            roles = string.Join(',', userRoles),
                            userId = user.Id
                        });
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
