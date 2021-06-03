using System.Collections.Generic;
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
        [Authorize(Policies.AdminRoleValue)]
        public async Task<IActionResult> CreateCampaign([FromBody] CampaignSaveDto campaignSaveDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = mapper.Map<CampaignSaveDto, Campaign>(campaignSaveDto);
            await campaignService.CreateCampaignAsync(campaign);

            var customers = mapper.Map<IEnumerable<CustomerDto>, IEnumerable<Customer>>(campaignSaveDto.Customers);

            await campaignService.AddCampaignDetailAsync(campaign.Id, customers);
            var result = mapper.Map<Campaign, CampaignSaveDto>(campaign);

            return Ok(result);
        }

        [HttpPost("addCustomers/{campaignId}")]
        public async Task<IActionResult> AddCustomersToCampaign([FromBody] IEnumerable<CustomerDto> customersDto, int campaignId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = await campaignService.GetCampaignAsync(campaignId);

            if (campaign == null)
                return NotFound();

            var customers = mapper.Map<IEnumerable<CustomerDto>, IEnumerable<Customer>>(customersDto);

            await campaignService.AddCampaignDetailAsync(campaign.Id, customers);

            return Ok();
        }

        [HttpGet("agents/{agentId}")]
        [Authorize(Policies.AdminRoleValue)]
        public async Task<IActionResult> GetAgentCampaignsAsync(string agentId, CampaignQueryDto campaignQueryResource)
        {
            var campaignQuery = mapper.Map<CampaignQueryDto, CampaignQuery>(campaignQueryResource);
            var userCampaignsQuery = await campaignService.GetAgentCampaignsAsync(agentId, campaignQuery);
            var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultDto<CampaignDto>>(userCampaignsQuery);

            resultQuery.Items = await campaignService.AddProgressToCampaignsAsync(resultQuery.Items);

            return Ok(resultQuery);
        }

        [HttpGet("{campaignId}")]
        public async Task<IActionResult> GetCampaignAsync(int campaignId)
        {
            var campaign = await campaignService.GetCampaignAsync(campaignId);

            if (campaign == null)
                return NotFound();

            var campaignDto = mapper.Map<Campaign, CampaignDto>(campaign);

            return Ok(campaignDto);
        }

        [HttpGet("agents/valids")]
        public async Task<IActionResult> GetAgentValidCampaignsAsync(CampaignQueryDto campaignQueryDto)
        {
            var loggedAgentEmail = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals("email")).Value;
            var agent = await userService.GetUserByEmailAsync(loggedAgentEmail);

            var campaignQuery = mapper.Map<CampaignQueryDto, CampaignQuery>(campaignQueryDto);
            var userCampaignsQuery = await campaignService.GetAgentValidCampaignsAsync(agent.Id, campaignQuery);
            var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultDto<CampaignDto>>(userCampaignsQuery);

            resultQuery.Items = await campaignService.AddProgressToCampaignsAsync(resultQuery.Items);

            return Ok(resultQuery);
        }

        [HttpPut("{campaignId}")]
        public async Task<IActionResult> UpdateCampaignAsync(int campaignId, [FromBody] CampaignDto campaignDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = await campaignService.GetCampaignAsync(campaignId);

            if (campaign == null)
                return NotFound();

            mapper.Map<CampaignDto, Campaign>(campaignDto, campaign);
            await campaignService.UpdateCampaignAsync(campaign);

            var result = mapper.Map<Campaign, CampaignDto>(campaign);

            return Ok(result);
        }
    }
}