using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/calls")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CallsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICallLogService callLogService;
        private readonly UserManager<ApplicationUser> userManager;

        public CallsController(IMapper mapper, ICallLogService callLogService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.callLogService = callLogService;
            this.mapper = mapper;
        }

        [HttpPost("synchronize")]
        public async Task<IActionResult> SynchronizeTodayCalls([FromBody] IEnumerable<CallLogResource> callLogResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("email")).Value;
            var user = await userManager.FindByEmailAsync(userEmail);

            var calls = mapper.Map<IEnumerable<CallLogResource>, IEnumerable<CallLog>>(callLogResources);

            await callLogService.SynchronizeTodayCalls(user.Id, calls);

            return Ok();
        }
    }
}