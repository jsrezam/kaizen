using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IAccountService accountService;

        public AccountsController(IMapper mapper, IUserService userService, IAccountService accountService)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsDto userCredentialsResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUser = mapper.Map<UserCredentialsDto, ApplicationUser>(userCredentialsResource);

            var response = await accountService.SignUpAsync(newUser, userCredentialsResource.Password);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            return Ok(await accountService.BuildToken(userCredentialsResource, true));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto userCredentialsResource)
        {
            var response = await accountService
            .PasswordSignInAsync(userCredentialsResource.Email
            , userCredentialsResource.Password
            , isPersistent: false
            , lockoutOnFailure: false
            );

            if (!response.Succeeded)
                return BadRequest("Invalid username and/or password.");

            return Ok(await accountService.BuildToken(userCredentialsResource));
        }

    }
}