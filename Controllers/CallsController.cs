using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/calls")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CallsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICallLogService callLogService;
        private readonly IUserService userService;

        public CallsController(IMapper mapper, ICallLogService callLogService, IUserService userService)
        {
            this.userService = userService;
            this.callLogService = callLogService;
            this.mapper = mapper;
        }

        [HttpPost("synchronize")]
        public async Task<IActionResult> SynchronizeTodayCalls([FromBody] IEnumerable<CallLogDto> callLogResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loggedAgentEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("email")).Value;
            var agent = await userService.GetUserByEmailAsync(loggedAgentEmail);

            var calls = mapper.Map<IEnumerable<CallLogDto>, IEnumerable<CallLog>>(callLogResources);

            await callLogService.SynchronizeTodayCalls(agent.Id, calls);

            return Ok();
        }
    }
}