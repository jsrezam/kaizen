using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
using Kaizen.Core.Services;
using Kaizen.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly KaizenDbContext context;
        private readonly IUserService userService;
        public UsersController(IMapper mapper, UserManager<ApplicationUser> userManager, KaizenDbContext context, IUserService userService
        )
        {
            this.userService = userService;
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("agents")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.RequireAdminRole)]
        public async Task<IActionResult> GetAgents()
        {

            var result = await (from u in context.Users
                                join uc in context.UserClaims on u.Id equals uc.UserId
                                where uc.ClaimType.Equals("role") && uc.ClaimValue.Equals("agent")
                                select new UserResource
                                {
                                    Id = u.Id,
                                    LastName = u.LastName,
                                    FirstName = u.FirstName,
                                    UserName = u.UserName,
                                    Email = u.Email
                                }).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("campaign/{campaignId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.RequireAdminRole)]
        public async Task<IActionResult> GetUserByCampaign(int campaignId)
        {
            var campaign = context.Campaigns.SingleOrDefault(c => c.Id == campaignId);
            if (campaign == null)
                return NotFound();

            var user = await userManager.FindByIdAsync(campaign.UserId);
            var result = mapper.Map<ApplicationUser, ApplicationUserResource>(user);
            return Ok(result);
        }
    }
}