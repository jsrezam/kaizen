using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Controllers.Utilities;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/users")]
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

        [HttpGet("agents")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.RequireAdminRole)]
        public async Task<IActionResult> GetAgentUsersAsync()
        {
            return Ok(await userService.GetAgentUsersAsync());
        }

        [HttpGet("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("campaign/{campaignId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.RequireAdminRole)]
        public async Task<IActionResult> GetUserByCampaignAsync(int campaignId)
        {
            var user = await userService.GetUserByCampaignAsync(campaignId);
            if (user == null)
                return NotFound();

            var result = mapper.Map<ApplicationUser, ApplicationUserResource>(user);
            return Ok(result);
        }
    }
}