using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
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
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        public UsersController(IMapper mapper, UserManager<IdentityUser> userManager)
        {
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var result = mapper.Map<IEnumerable<IdentityUser>, IEnumerable<IdentityUserResource>>(users);
            return Ok(result);
        }
    }
}