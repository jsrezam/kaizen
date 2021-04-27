using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core.Models;
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
        public UsersController(IMapper mapper, UserManager<ApplicationUser> userManager, KaizenDbContext context
        )
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet("customersByUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCustomersByUser()
        {
            var email = HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Equals("email")).Value;

            var user = await userManager.FindByEmailAsync(email);

            return Ok(null);
        }

        [HttpGet("agents")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var result = mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserResource>>(users);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = mapper.Map<ApplicationUser, ApplicationUserResource>(user);
            return Ok(result);
        }

        [HttpGet("campaign/{campaignId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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