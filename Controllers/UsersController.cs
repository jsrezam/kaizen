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
    [Route("/api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        public UsersController(IMapper mapper, IUserService userService
        )
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet("agents/actives")]
        [Authorize(Policies.AdminRoleValue)]
        public async Task<IActionResult> GetActiveAgentsAsync()
        {
            return Ok(await userService.GetActiveAgentsAsync());
        }

        [HttpGet("campaign/{campaignId}")]
        public async Task<IActionResult> GetAgentByCampaignAsync(int campaignId)
        {
            var user = await userService.GetAgentByCampaignAsync(campaignId);
            if (user == null)
                return NotFound();

            var result = mapper.Map<ApplicationUser, ApplicationUserDto>(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersViewAsync(ApplicationUserQueryDto applicationUserQueryDto)
        {
            var userQuery = mapper.Map<ApplicationUserQueryDto, ApplicationUserQuery>(applicationUserQueryDto);
            var queryResult = await userService.GetUsersViewAsync(userQuery);
            var resultQuery = mapper.Map<QueryResult<UserViewModel>, QueryResultDto<UserViewModelDto>>(queryResult);

            return Ok(resultQuery);
        }

        [HttpPost("changeState")]
        [Authorize(Policies.AdminRoleValue)]
        public async Task<IActionResult> ChangeUserState([FromBody] UserViewModelDto userDto)
        {
            var user = await userService.GetUserByIdAsync(userDto.Id);

            if (user == null) return NotFound();

            await userService.ChangeUserState(user);

            var userUpdated = await userService.GetUserViewAsync(user.Id);

            mapper.Map<UserViewModel, UserViewModelDto>(userUpdated);

            return Ok(userUpdated);
        }
    }
}