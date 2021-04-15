using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kaizen.Controllers.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kaizen.Controllers
{
    [Route("/api/accounts")]
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountsController(UserManager<IdentityUser> userManager
        , SignInManager<IdentityUser> signInManager
        , IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsResource userCredentialsResource)
        {
            var user = new IdentityUser
            {
                UserName = userCredentialsResource.Email,
                Email = userCredentialsResource.Email
            };

            var response = await userManager.CreateAsync(user, userCredentialsResource.Password);
            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(await BuildToken(userCredentialsResource));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsResource userCredentialsResource)
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

        private async Task<ResponseAuthenticationResourse> BuildToken(UserCredentialsResource userCredentialsResource)
        {
            var claims = new List<Claim>()
            {
                new Claim("email",userCredentialsResource.Email)
            };

            var user = await userManager.FindByEmailAsync(userCredentialsResource.Email);
            var claimsDB = await userManager.GetClaimsAsync(user);
            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims
            , expires: expiration, signingCredentials: credentials);

            return new ResponseAuthenticationResourse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}