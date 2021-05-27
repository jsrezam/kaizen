using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/campaignDetails")]
    public class CampaignDetailsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICampaignDetailService campaignDetailService;

        public CampaignDetailsController(IMapper mapper, ICampaignDetailService campaignDetailService)
        {
            this.campaignDetailService = campaignDetailService;
            this.mapper = mapper;
        }

        [HttpGet("{campaignId}")]
        public async Task<IActionResult> GetCampaignDetailByCampaignAsync(int campaignId, CampaignDetailQueryDto campaignDetailQueryResource)
        {
            var productQuery = mapper.Map<CampaignDetailQueryDto, CampaignDetailQuery>(campaignDetailQueryResource);
            var queryResult = await campaignDetailService.GetCampaignDetailByCampaignAsync(campaignId, productQuery);
            var resultQuery = mapper.Map<QueryResult<CampaignDetail>, QueryResultDto<CampaignDetailDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpDelete("{campaignDetailItemId}")]
        public async Task<IActionResult> RemoveDetailCampaignItem(int campaignDetailItemId)
        {
            var campaignDetailItem = await campaignDetailService.GetCampaignDetailItemAsync(campaignDetailItemId);

            if (campaignDetailItem == null)
                return NotFound();

            await campaignDetailService.RemoveDetailCampaignItem(campaignDetailItem);

            return Ok();
        }

    }
}