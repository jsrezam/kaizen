using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Controllers;
using Kaizen.Controllers.Utilities;
using Kaizen.Core.Models;

namespace Kaizen.Core.Services
{
    public class CallLogService : ICallLogService
    {
        private readonly IUnitOfWork unitOfWork;

        public CallLogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task SynchronizeTodayCalls(string userId, IEnumerable<CallLog> callLogs)
        {
            var queryResult = await unitOfWork.CampaignRepository.GetAgentValidCampaignsAsync(userId, new CampaignQuery());

            if (queryResult.TotalItems == 0)
                return;

            foreach (var userCampaign in queryResult.Items)
            {
                foreach (var campaignDetail in userCampaign.CampaignDetails)
                {
                    var matchCall = callLogs.FirstOrDefault(cd => cd.CallNumberFormatted.Equals(campaignDetail.Customer.CellPhone));
                    if (matchCall != null)
                    {
                        campaignDetail.CallDuration = matchCall.CallDurationFormat;
                        campaignDetail.CallTimes += matchCall.CallTimes;
                        campaignDetail.Status = CampaignStatus.Called.ToString();
                        unitOfWork.CampaignDetailRepository.Update(campaignDetail);
                    }
                }
            }

            await unitOfWork.SaveChangesAsync();

        }
    }
}