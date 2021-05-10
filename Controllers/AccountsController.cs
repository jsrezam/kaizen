using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Core.DTOs;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kaizen.Controllers
{
    [Route("/api/accounts")]
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountsController(UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        , IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsDto userCredentialsResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = userCredentialsResource.Email,
                Email = userCredentialsResource.Email,
                LastName = userCredentialsResource.LastName,
                FirstName = userCredentialsResource.FirstName,
                PhoneNumber = userCredentialsResource.PhoneNumber,
                IdentificationCard = userCredentialsResource.IdentificationCard

            };

            var response = await userManager.CreateAsync(user, userCredentialsResource.Password);
            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(await BuildToken(userCredentialsResource, true));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto userCredentialsResource)
        {
            var response = await signInManager
            .PasswordSignInAsync(userCredentialsResource.Email
            , userCredentialsResource.Password
            , isPersistent: false
            , lockoutOnFailure: false
            );

            if (!response.Succeeded)
                return BadRequest("Invalid username and/or password.");

            return Ok(await BuildToken(userCredentialsResource));
        }

        private async Task<ResponseAuthenticationDto> BuildToken(UserCredentialsDto userCredentialsResource, bool IsNew = false)
        {
            var user = await userManager.FindByEmailAsync(userCredentialsResource.Email);

            if (IsNew)
            {
                var claims = new List<Claim>()
                {
                    new Claim("email",userCredentialsResource.Email),
                    new Claim("role","agent")
                };

                await userManager.AddClaimsAsync(user, claims);
            }

            var claimsDB = await userManager.GetClaimsAsync(user);
            //claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claimsDB
            , expires: expiration, signingCredentials: credentials);

            return new ResponseAuthenticationDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}