using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Common;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Kaizen.Core.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> SignUp([FromBody] UserCredentialsDto userCredentialsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = mapper.Map<UserCredentialsDto, ApplicationUser>(userCredentialsDto);

            var response = await accountService.SignUpAsync(user, userCredentialsDto.Password);

            if (!response.Succeeded)
                return BadRequest(response.Errors);

            var newUser = await userService.GetUserByEmailAsync(user.Email);

            return Ok(await accountService.BuildToken(newUser, true));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto userCredentialsDto)
        {
            var response = await accountService
            .PasswordSignInAsync(userCredentialsDto.Email
            , userCredentialsDto.Password
            , isPersistent: false
            , lockoutOnFailure: false
            );

            if (!response.Succeeded) return BadRequest("Invalid username and/or password.");

            var userLogged = await userService.GetUserByEmailAsync(userCredentialsDto.Email);

            if (!userLogged.IsActive) return BadRequest("Your username is inactive. Validate this problem with your system administrator.");

            return Ok(await accountService.BuildToken(userLogged));
        }

        [HttpPost("makeAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.AdminRoleValue)]
        public async Task<IActionResult> MakeAdminAsync([FromBody] UserViewModelDto userDto)
        {
            var userView = await userService.GetUserViewAsync(userDto.Id);

            if (userView == null) return NotFound();

            if (userView.Role.Equals(Policies.AdminRoleValue))
                return BadRequest("This user is already in the administrator role.");

            var user = await userService.GetUserByIdAsync(userView.Id);

            await accountService.RemoveRoleAsync(user, Policies.AgentRoleValue);
            await accountService.AddRoleAsync(user, Policies.AdminRoleValue);

            var adminUser = await userService.GetUserViewAsync(user.Id);

            mapper.Map<UserViewModel, UserViewModelDto>(adminUser);

            return Ok(adminUser);
        }

        [HttpPost("makeAgent")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.AdminRoleValue)]
        public async Task<IActionResult> MakeAgentAsync([FromBody] UserViewModelDto userDto)
        {
            var userView = await userService.GetUserViewAsync(userDto.Id);

            if (userView == null) return NotFound();

            if (userView.Role.Equals(Policies.AgentRoleValue))
                return BadRequest("This user is already in the agent role.");

            var user = await userService.GetUserByIdAsync(userView.Id);

            await accountService.RemoveRoleAsync(user, Policies.AdminRoleValue);
            await accountService.AddRoleAsync(user, Policies.AgentRoleValue);

            var agentUser = await userService.GetUserViewAsync(user.Id);

            mapper.Map<UserViewModel, UserViewModelDto>(agentUser);

            return Ok(agentUser);
        }

    }
}