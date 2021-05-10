using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Controllers.Resources;
using Kaizen.Core;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/campaignDetails")]
    public class CampaignDetailsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CampaignDetailsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("{campaignId}")]
        public async Task<IActionResult> GetCampaignDetail(int campaignId, CampaignDetailQueryResource campaignDetailQueryResource)
        {
            var productQuery = mapper.Map<CampaignDetailQueryResource, CampaignDetailQuery>(campaignDetailQueryResource);
            var queryResult = await unitOfWork.CampaignDetailRepository.GetCampaignDetailAsync(campaignId, productQuery);
            var resultQuery = mapper.Map<QueryResult<CampaignDetail>, QueryResultResource<CampaignDetailResource>>(queryResult);
            return Ok(resultQuery);
        }

    }
}