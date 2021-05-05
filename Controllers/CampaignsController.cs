using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/campaigns")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CampaignsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public CampaignsController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> CreateCampaign([FromBody] CampaignSaveResource CampaignSaveResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = mapper.Map<CampaignSaveResource, Campaign>(CampaignSaveResource);
            await unitOfWork.CampaignRepository.AddAsync(campaign);
            await unitOfWork.SaveChangesAsync();

            if (campaign.Id <= 0)
                return BadRequest();

            var campaignDetail = AddCampaignDetail(campaign.Id, CampaignSaveResource.Customers);

            await unitOfWork.CampaignDetailRepository.AddRangeAsync(campaignDetail);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<Campaign, CampaignSaveResource>(campaign);
            return Ok(result);
        }

        // [HttpGet]
        // public async Task<IActionResult> GetCampaigns(CampaignQueryResource campaignQueryResource)
        // {
        //     var campaignQuery = mapper.Map<CampaignQueryResource, CampaignQuery>(campaignQueryResource);
        //     var queryResult = await unitOfWork.CampaignRepository.GetCampaignsAsync(campaignQuery);
        //     var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultResource<CampaignResource>>(queryResult);
        //     return Ok(resultQuery);
        // }

        [HttpGet("user/{userId}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> GetUserCampaigns(string userId, CampaignQueryResource campaignQueryResource)
        {
            var campaignQuery = mapper.Map<CampaignQueryResource, CampaignQuery>(campaignQueryResource);
            var userCampaignsQuery = await unitOfWork.CampaignRepository.GetUserCampaignsAsync(userId, campaignQuery);
            var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultResource<CampaignResource>>(userCampaignsQuery);
            resultQuery.Items = await AddProgressToCampaignsAsync(resultQuery.Items);

            return Ok(resultQuery);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserCampaigns(CampaignQueryResource campaignQueryResource)
        {
            var userEmail = HttpContext.User.Claims
            .FirstOrDefault(x => x.Type.Equals("email")).Value;
            var user = await userManager.FindByEmailAsync(userEmail);

            var campaignQuery = mapper.Map<CampaignQueryResource, CampaignQuery>(campaignQueryResource);
            var userCampaignsQuery = await unitOfWork.CampaignRepository._GetUserCampaignsAsync(user.Id, campaignQuery);
            var resultQuery = mapper.Map<QueryResult<Campaign>, QueryResultResource<CampaignResource>>(userCampaignsQuery);
            resultQuery.Items = await AddProgressToCampaignsAsync(resultQuery.Items);

            return Ok(resultQuery);
        }

        // [HttpGet("{campaignId}")]
        // public async Task<IActionResult> GetUserCampaign(int campaignId)
        // {
        //     var campaign = await unitOfWork.CampaignRepository.GetCampaignAsync(campaignId);
        //     if (campaign == null)
        //         return NotFound();

        //     var campaignResource = mapper.Map<Campaign, CampaignResource>(campaign);
        //     return Ok(campaignResource);
        // }

        [HttpPut("{id}")]
        [Authorize(Policies.RequireAdminRole)]
        public async Task<IActionResult> InactivateCampaign(int id, [FromBody] CampaignResource campaignResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var campaign = await unitOfWork.CampaignRepository.GetByIdAsync(id);

            if (campaign == null)
                return NotFound();

            mapper.Map<CampaignResource, Campaign>(campaignResource, campaign);
            unitOfWork.CampaignRepository.Update(campaign);
            await unitOfWork.SaveChangesAsync();

            var result = mapper.Map<Campaign, CampaignResource>(campaign);
            return Ok(result);
        }

        private IEnumerable<CampaignDetail> AddCampaignDetail(int CampaignId, IEnumerable<CustomerResource> customersResource)
        {
            var resp = new List<CampaignDetail>();

            foreach (var customer in customersResource)
            {
                resp.Add(new CampaignDetail
                {
                    CustomerId = customer.Id,
                    CampaignId = CampaignId,
                    Status = CampaignStatus.NotCalled.ToString()
                });
            }

            return resp;
        }

        private async Task<IEnumerable<CampaignResource>> AddProgressToCampaignsAsync(IEnumerable<CampaignResource> userCampaignsResource)
        {
            foreach (var userCampaign in userCampaignsResource)
            {
                userCampaign.Progress = decimal.Round((
                    await unitOfWork.CampaignDetailRepository
                    .GetCalledCustomerNumberInCampaignAsync(userCampaign.Id)
                * 100) / await unitOfWork.CampaignDetailRepository
                .GetCustomersInCampaignNumberAsync(userCampaign.Id), 2);
            }

            return userCampaignsResource;
        }
    }
}