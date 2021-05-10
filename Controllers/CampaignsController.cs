using System.Linq;
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
    [Route("/api/campaigns")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CampaignsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICampaignService campaignService;
        private readonly IUserService userService;

        public CampaignsController(IMapper mapper, ICampaignService campaignService, IUserService userService)
        {
            this.userService = userService;
            this.campaignService = campaignService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> CreateCampaign([FromBody] CampaignSaveResource CampaignSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = mapper.Map<CampaignSaveResource, Campaign>(CampaignSaveResource);
            await campaignService.CreateCampaignAsync(campaign);

            await campaignService.AddCampaignDetailAsync(campaign.Id, CampaignSaveResource.Customers);
            var result = mapper.Map<Campaign, CampaignSaveResource>(campaign);

            return Ok(result);
        }

        [HttpGet("agents/{agentId}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> GetAgentCampaignsAsync(string agentId, CampaignQueryResource campaignQueryResource)
        {
            var campaignQuery = mapper.Map<CampaignQueryResource, CampaignQuery>(campaignQueryResource);
            var userCampaignsQuery = await campaignService.GetAgentCampaignsAsync(agentId, campaignQuery);
            var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultResource<CampaignResource>>(userCampaignsQuery);

            resultQuery.Items = await campaignService.AddProgressToCampaignsAsync(resultQuery.Items);

            return Ok(resultQuery);
        }

        [HttpGet("agents/valids")]
        public async Task<IActionResult> GetAgentValidCampaignsAsync(CampaignQueryResource campaignQueryResource)
        {
            var loggedAgentEmail = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals("email")).Value;
            var agent = await userService.GetUserByEmailAsync(loggedAgentEmail);

            var campaignQuery = mapper.Map<CampaignQueryResource, CampaignQuery>(campaignQueryResource);
            var userCampaignsQuery = await campaignService.GetAgentValidCampaignsAsync(agent.Id, campaignQuery);
            var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultResource<CampaignResource>>(userCampaignsQuery);

            resultQuery.Items = await campaignService.AddProgressToCampaignsAsync(resultQuery.Items);

            return Ok(resultQuery);
        }

        [HttpPut("{campaignId}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> InactivateCampaign(int campaignId, [FromBody] CampaignResource campaignResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = await campaignService.GetCampaignAsync(campaignId);

            if (campaign == null)
                return NotFound();

            mapper.Map<CampaignResource, Campaign>(campaignResource, campaign);
            await campaignService.UpdateCampaignAsync(campaign);
            var result = mapper.Map<Campaign, CampaignResource>(campaign);

            return Ok(result);
        }
    }
}