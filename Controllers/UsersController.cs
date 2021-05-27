using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Utilities;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
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
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> GetActiveAgentsAsync()
        {
            return Ok(await userService.GetActiveAgentsAsync());
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

            return Ok(user);
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
    }
}